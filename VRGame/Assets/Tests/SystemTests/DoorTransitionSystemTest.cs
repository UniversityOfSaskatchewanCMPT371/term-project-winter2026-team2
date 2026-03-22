using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Collections;

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

    /// <summary>
    /// The component that is being tested.
    /// TODO : Replace the type to the class you are testing.
    /// </summary>
    MonoBehaviour comp;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [Setup]
    public void Setup()
    {
        go = new GameObject();

        // NOTE: adding component in play mode will automatically call Awake() & Start().
        // If necessary, you may move this directly in the test function instead
        comp = go.AddComponent<>(); // TODO : Replace generic with component you are testing
    }

    /// <summary>
    /// Called after each tests. Handles the clean up
    /// of game object.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator Instantiation()
    {
        
    }
}
