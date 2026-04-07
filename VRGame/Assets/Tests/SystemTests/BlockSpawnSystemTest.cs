using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Collections;
using UnityEditor;
using System.Text.RegularExpressions;
using UnityEngine.XR.Interaction.Toolkit;

public class BlockSpawnSystemTest
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;

    /// <summary>
    /// The block spawner controller being tested.
    /// </summary>
    BlockSpawnerController blockSpawnerController;

    /// <summary>
    /// A test prefab to be instantiated when the spawn button is clicked.
    /// </summary>
    GameObject testPrefab;

    /// <summary>
    /// The spawn button to interact with using the fake XR controller.
    /// </summary>
    XRSimpleInteractable interactable;

    /// <summary>
    /// A fake XRDirectInteractor to simulate the interaction from an XR controller.
    /// </summary>
    XRDirectInteractor fakeInteractor;

    public void SetUpFakeInteractor()
    {
        fakeInteractor = go.AddComponent<XRDirectInteractor>();
    }

    /// <summary>
    /// Sets up the block spawner components on the game object.
    /// </summary>
    public void SetUpBlockSpawner()
    {
        // attach the block spawner components
        BlockSpawnerModel blockSpawnerModel = go.AddComponent<BlockSpawnerModel>();
        go.AddComponent<BlockSpawnerView>();
        blockSpawnerController = go.AddComponent<BlockSpawnerController>();
    
        // must add a test prefab to instantiate when the spawn button is clicked, otherwise it will throw an error
        testPrefab = new GameObject("TestBlock");
        blockSpawnerModel.BlockPrefabs = new GameObject[1] { testPrefab };
    }

    /// <summary>
    /// Sets up the spawn button components on the game object and its
    /// dependencies.
    /// </summary>
    public void SetUpSpawnButton()
    {
        // attach the spawn button components
        go.AddComponent<SpawnButtonView>();

        // attaching spawn button controller component will trigger some errors/warnings
        // because the private serialized field `bloackSpawnerController` is not set YET.
        SerializedObject serializedObject = new SerializedObject(go.AddComponent<SpawnButtonController>());

        // set the reference to block spawner controller to avoid errors/warnings during the actual test
        serializedObject.FindProperty("blockSpawnerController").objectReferenceValue = blockSpawnerController;
        serializedObject.ApplyModifiedProperties();

        interactable = go.AddComponent<XRSimpleInteractable>();
    }

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [UnitySetUp]
    public IEnumerator Setup()
    {
        go = new GameObject();

        // set up the test environment by attaching the necessary components
        // to the game objects
        SetUpBlockSpawner();
        SetUpSpawnButton();

        yield return null;
    }

    /// <summary>
    /// Called after each tests. Handles the clean up
    /// of game object.
    /// </summary>
    [UnityTearDown]
    public IEnumerator TearDown()
    {
        UnityEngine.Object.DestroyImmediate(go);
        UnityEngine.Object.DestroyImmediate(testPrefab);
        yield return null;
    }

    [UnityTest]
    public IEnumerator XRControllerClicksSpawnButton()
    {
        var args = new SelectEnterEventArgs
        {
            interactorObject = fakeInteractor,
            interactableObject = interactable  
        };

        // emulate an XR controller clicking the spawn button
        interactable.selectEntered.Invoke(args);
        
        // find the test block in the scene to verify if it has been instantiated
        Assert.IsTrue(GameObject.Find("TestBlock") != null, "The block prefab should be instantiated when the spawn button is clicked.");

        yield return null;
    }
}