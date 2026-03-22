using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Collections;
using NSubstitute;
using UnityEngine.SceneManagement;

/// <summary>
/// System test for the door/scene transition
/// </summary>
public class DoorTransitionSystemTest
{
    /// <summary>
    /// All necessary objects to conduct the System Test
    /// </summary>
    private GameObject playerRig;
    private GameObject doorObject;
    private GameObject sceneChangerObject;
    private DoorModel sourceDoor;
    private DoorModel targetDoor;
    private SceneChangerController sceneChanger;
    private DoorView doorV;
    private Collider playerCollider;


    /// <summary>
    /// This constructs the hierarchy that DoorView expects
    /// (Mirrors the XR Rig structure of the scenes)
    /// </summary>
    private Collider SetupPlayer()
    {
        playerRig = new GameObject("PlayerRig");
        playerRig.AddComponent<PlayerModel>();
        playerRig.AddComponent<PlayerView>();
        playerRig.AddComponent<PlayerController>();

        GameObject mainCamera = new GameObject("MainCamera");
        mainCamera.tag = "MainCamera";
        mainCamera.transform.SetParent(playerRig.transform);
        BoxCollider collider = mainCamera.AddComponent<BoxCollider>();

        UnityEngine.Object.DontDestroyOnLoad(playerRig);
        return collider;
    }

    public void SetupDoor()
    {
        doorObject = new GameObject("Door");
        UnityEngine.Object.DontDestroyOnLoad(doorObject);

        // Creates the initial door
        sourceDoor = doorObject.AddComponent<DoorModel>();
        sourceDoor.ResetDoorLookup();
        sourceDoor.DoorId = 1;
        sourceDoor.TargetDoorId = 2;
        sourceDoor.DestinationSceneId = 1; // Frontal Lobe

        // Creates a target for the door
        targetDoor = doorObject.AddComponent<DoorModel>();
        targetDoor.DoorId = 2;
        targetDoor.TargetDoorId = 1;

        sourceDoor.Init();
        targetDoor.Init();

        DoorController doorC = doorObject.AddComponent<DoorController>();
        doorC.DoorModel = sourceDoor;
        doorC.SceneChangerController = sceneChanger;

        doorV = doorObject.AddComponent<DoorView>();
        doorV.DoorController = doorC;
    }

    public void SetupSceneChanger()
    {
        sceneChangerObject = new GameObject("SceneChanger");
        UnityEngine.Object.DontDestroyOnLoad(sceneChangerObject);

        sceneChanger = sceneChangerObject.AddComponent<SceneChangerController>();
        sceneChanger.SceneManagerWrapper = new SceneManagerWrapper();
    }

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [UnitySetUp]
    public IEnumerator Setup()
    {
    SetupSceneChanger();
    yield return null;

    playerCollider = SetupPlayer();
    SetupDoor();

    yield return null;
    }

    /// <summary>
    /// Called after each tests. Handles the clean up
    /// of game object.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        sourceDoor.ResetDoorLookup();
        sceneChanger.ResetInstance();
        UnityEngine.Object.Destroy(playerRig);
        UnityEngine.Object.Destroy(doorObject);
        UnityEngine.Object.Destroy(sceneChangerObject);
    }

    [UnityTest]
    public IEnumerator PlayerEntersDoorToNewScene()
    {
        doorV.OnTriggerEnter(playerCollider);

        DoorController doorController = doorObject.GetComponent<DoorController>();
        float timeout = Time.time + 10f;
        while (doorController.TriggerDebounce)
        {
            Assert.IsTrue(Time.time < timeout, "Scene load timed out");
            yield return null;
        }

        Assert.AreEqual((int)SceneEnum.PracticeRoom, SceneManager.GetActiveScene().buildIndex,
        "Active scene should be FrontalLobe after door transition");
    }
}
