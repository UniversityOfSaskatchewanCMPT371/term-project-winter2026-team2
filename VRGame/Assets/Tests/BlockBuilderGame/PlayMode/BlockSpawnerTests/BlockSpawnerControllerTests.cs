using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Collections;

public class BlockSpawnerControllerTests
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;

    /// <summary>
    /// The component that is being tested.
    /// </summary>
    BlockSpawnerController controller;
    BlockSpawnerModel model;
    BlockSpawnerView view;
    GameObject prefabA;
    GameObject prefabB;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        go = new GameObject();
        model = go.AddComponent<BlockSpawnerModel>();
        view = go.AddComponent<BlockSpawnerView>();
        controller = go.AddComponent<BlockSpawnerController>();
        prefabA = GameObject.CreatePrimitive(PrimitiveType.Cube);
        prefabB = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }

    /// <summary>
    /// Called after each tests. Handles the clean up
    /// of game object.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        if (model != null && model.LastSpawnedBlock != null)
        {
            UnityEngine.Object.DestroyImmediate(model.LastSpawnedBlock);
        }

        if (prefabA != null)
        {
            UnityEngine.Object.DestroyImmediate(prefabA);
        }

        if (prefabB != null)
        {
            UnityEngine.Object.DestroyImmediate(prefabB);
        }

        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator Instantiation()
    {
        // checks the controller component is attached
        Assert.IsNotNull(controller);

        // let Start() run
        yield return null;
    }

    [UnityTest]
    public IEnumerator SpawnBlock_UpdatesModelState()
    {
        // checks spawn updates block reference index and scale
        model.BlockPrefabs = new[] { prefabA, prefabB };
        model.BlockScale = 2.0f;
        model.CurrentBlockIndex = 0;

        // let Start() run
        yield return null;

        // spawn block through controller
        controller.SpawnBlock();

        yield return null;

        Assert.IsNotNull(model.LastSpawnedBlock);
        Assert.AreEqual(1, model.CurrentBlockIndex);
        Assert.AreEqual(Vector3.one * 2.0f, model.LastSpawnedBlock.transform.localScale);
    }

    [UnityTest]
    public IEnumerator SpawnBlock_CyclesIndexBackToZero()
    {
        // checks index wraps back to zero after last prefab
        model.BlockPrefabs = new[] { prefabA, prefabB };
        model.BlockScale = 1.0f;
        model.CurrentBlockIndex = 1;

        // let Start() run
        yield return null;

        // spawn block through controller
        controller.SpawnBlock();

        yield return null;

        // index should cycle back to zero after spawning prefabB at index 1
        Assert.AreEqual(0, model.CurrentBlockIndex);
        Assert.IsNotNull(model.LastSpawnedBlock);
    }

    [UnityTest]
    public IEnumerator SpawnBlock_OutOfBoundsIndex_ThrowsAssertion()
    {
        // checks out of bounds index throws assertion
        yield return null;

        // set up valid state then break index
        model.BlockPrefabs = new[] { prefabA };
        model.BlockScale = 1.0f;
        model.CurrentBlockIndex = 2;

        Assert.Throws<UnityEngine.Assertions.AssertionException>(() => controller.SpawnBlock());
    }

}