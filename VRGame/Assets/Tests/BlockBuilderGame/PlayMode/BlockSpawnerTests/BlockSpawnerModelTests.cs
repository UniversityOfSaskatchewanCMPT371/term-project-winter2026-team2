using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Text.RegularExpressions;

public class BlockSpawnerModelTests
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;

    /// <summary>
    /// The component that is being tested.
    /// </summary>
    BlockSpawnerModel model;
    BlockSpawnerController controller;
    BlockSpawnerView view;
    GameObject prefab;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        // create the shared test object
        go = new GameObject();

        // add the model under test
        model = go.AddComponent<BlockSpawnerModel>();

        // create a simple source object used as prefab data
        prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    /// <summary>
    /// Called after each tests. Handles the clean up
    /// of game object.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        // destroy the spawned block if a test created one
        if (model != null && model.LastSpawnedBlock != null)
        {
            UnityEngine.Object.DestroyImmediate(model.LastSpawnedBlock);
        }

        // destroy the temporary prefab source object
        if (prefab != null)
        {
            UnityEngine.Object.DestroyImmediate(prefab);
        }

        // destroy the main test object
        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator Instantiation()
    {
        // allow Start() to invoke Model.Init()
        yield return null;

        // verify Init() default for all variables
        Assert.AreEqual(0, model.CurrentBlockIndex);
        Assert.AreEqual(1.0f, model.BlockScale);
        Assert.IsNull(model.LastSpawnedBlock);
    }

    [UnityTest]
    public IEnumerator ModelData_DrivesControllerSpawn()
    {
        // Expect an exception because Init() in view layer is in Awake() call
        LogAssert.Expect(LogType.Exception, new Regex(".*'viewInstance' field cannot be null.*"));

        // add controller and view to form a minimal MVC setup
        controller = go.AddComponent<BlockSpawnerController>();
        view = go.AddComponent<BlockSpawnerView>();

        // set model state that controller should consume
        model.BlockPrefabs = new[] { prefab };
        model.BlockScale = 2.0f;

        // wire layers through test hooks
        controller.ModelMock = model;
        controller.ViewMock = view;
        view.ControllerMock = controller;

        // let Start() run
        yield return null;

        // trigger cross-layer behavior through controller
        controller.SpawnBlock();

        // Wait one frame for instantiate/update calls
        yield return null;

        // confirm model was updated with spawned instance
        Assert.IsNotNull(model.LastSpawnedBlock);
        Assert.AreEqual(Vector3.one * 2.0f, model.LastSpawnedBlock.transform.localScale);
        Assert.AreEqual(0, model.CurrentBlockIndex);
    }
}