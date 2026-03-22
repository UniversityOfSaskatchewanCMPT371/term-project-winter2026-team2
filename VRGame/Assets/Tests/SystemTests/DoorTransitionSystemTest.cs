using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Collections;
using NSubstitute;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

/// <summary>
/// System test for the door/scene transition
/// An end-to-end pipling of the player collider entering a door,
/// the door controller initiates a scene from the scene changer controller,
/// then the correct scene becomes active
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

    /// <summary>
    /// Creates a door GameObject with a source and target door.
    /// The source door is configured to transition to the Occipital Lobe
    /// </summary>
    private void SetupDoor()
    {
        doorObject = new GameObject("Door");
        UnityEngine.Object.DontDestroyOnLoad(doorObject);

        // Creates the initial door
        sourceDoor = doorObject.AddComponent<DoorModel>();
        sourceDoor.ResetDoorLookup();
        sourceDoor.DoorId = 1;
        sourceDoor.TargetDoorId = 2;
        sourceDoor.DestinationSceneId = 4; // Occipital Lobe

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

    /// <summary>
    /// Creates a SceneChangerController with a SceneManagerWrapper
    /// Needs to be called first, since other components depend on this
    /// </summary>
    private void SetupSceneChanger()
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

    /// <summary>
    /// Simulates a player entering a door and verifies that the scene transitions
    /// to the expected destination scene (in this case, the Occipital Lobe)
    /// Also has a 10 second timeout to prevent any hanging
    /// </summary>
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

        Assert.AreEqual((int)SceneEnum.OccipitalLobe, SceneManager.GetActiveScene().buildIndex,
        "Active scene should be OccipitalLobe after door transition");
    }
}
