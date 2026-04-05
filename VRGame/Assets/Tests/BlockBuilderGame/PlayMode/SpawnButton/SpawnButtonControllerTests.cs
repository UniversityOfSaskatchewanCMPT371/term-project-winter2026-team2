using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class SpawnButtonControllerTests
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;

    /// <summary>
    /// The component that is being tested.
    /// </summary>
    SpawnButtonController comp;

    /// <summary>
    /// Other components/objects required to test the functionality
    /// </summary>
    SpawnButtonView view;
    BlockSpawnerController blockSpawnerController;
    BlockSpawnerModel blockSpawnerModel;
    BlockSpawnerView blockSpawnerView;
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

        view = go.AddComponent<SpawnButtonView>();
        blockSpawnerModel = go.AddComponent<BlockSpawnerModel>();
        blockSpawnerView = go.AddComponent<BlockSpawnerView>();
        blockSpawnerController = go.AddComponent<BlockSpawnerController>();
        comp = go.AddComponent<SpawnButtonController>();

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
        if (blockSpawnerModel != null && blockSpawnerModel.LastSpawnedBlock != null)
        {
            UnityEngine.Object.DestroyImmediate(blockSpawnerModel.LastSpawnedBlock);
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
        Assert.IsNotNull(comp);

        // let Start() run
        yield return null;
    }

    [UnityTest]
    public IEnumerator OnButtonPressed_SpawnsBlockThroughBlockSpawner()
    {
        // checks button press triggers block spawner
        blockSpawnerModel.BlockPrefabs = new[] { prefabA, prefabB };
        blockSpawnerModel.BlockScale = 1.5f;
        blockSpawnerModel.CurrentBlockIndex = 0;

        // let Start() run
        yield return null;

        // no way to trigger a button press so we call it manually
        comp.OnButtonPressed();

        yield return null;

        // verify a block was spawned and model state updated
        Assert.IsNotNull(blockSpawnerModel.LastSpawnedBlock);
        Assert.AreEqual(1, blockSpawnerModel.CurrentBlockIndex);
        Assert.AreEqual(Vector3.one * 1.5f, blockSpawnerModel.LastSpawnedBlock.transform.localScale);
    }

    [UnityTest]
    public IEnumerator OnButtonPressed_WhenSpawnerIndexInvalid_ThrowsAssertion()
    {
        // checks button press surfaces spawner assertion for invalid index
        yield return null;

        // set up valid data then break the index
        blockSpawnerModel.BlockPrefabs = new[] { prefabA };
        blockSpawnerModel.BlockScale = 1.0f;
        blockSpawnerModel.CurrentBlockIndex = 2;

        // no way to trigger a button press so we call it manually
        Assert.Throws<UnityEngine.Assertions.AssertionException>(() => comp.OnButtonPressed());

        yield return null;
    }
}