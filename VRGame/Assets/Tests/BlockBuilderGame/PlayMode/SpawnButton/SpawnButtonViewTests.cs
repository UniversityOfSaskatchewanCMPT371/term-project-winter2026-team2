using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnButtonViewTests
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;

    /// <summary>
    /// The component that is being tested.
    /// </summary>
    SpawnButtonView comp;

    /// <summary>
    /// Other components/objects required to test the functionality
    /// </summary>
    SpawnButtonController controller;
    BlockSpawnerController blockSpawnerController;
    BlockSpawnerModel blockSpawnerModel;
    BlockSpawnerView blockSpawnerView;
    GameObject prefab;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        go = new GameObject();

        blockSpawnerModel = go.AddComponent<BlockSpawnerModel>();
        blockSpawnerView = go.AddComponent<BlockSpawnerView>();
        blockSpawnerController = go.AddComponent<BlockSpawnerController>();
        comp = go.AddComponent<SpawnButtonView>();
        controller = go.AddComponent<SpawnButtonController>();

        prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
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

        if (prefab != null)
        {
            UnityEngine.Object.DestroyImmediate(prefab);
        }

        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator Instantiation()
    {
        // checks the view component is attached
        Assert.IsNotNull(comp);

        // let Start() run
        yield return null;
    }

    [UnityTest]
    public IEnumerator SetupXREventsWithNoInteractablesDoesNotThrow()
    {
        LogAssert.Expect(LogType.Warning, "Cannot get components, none exist");

        // setup xr events with no interactables
        Assert.DoesNotThrow(() => comp.SetupXREvents());

        yield return null;
    }

    [UnityTest]
    public IEnumerator SetupXREventsSelectEnteredInvokesController()
    {
        // let Start() run
        yield return null;

        // create interactable and trigger select event
        GameObject interactableGo = new GameObject();
        interactableGo.transform.SetParent(go.transform);
        XRSimpleInteractable interactable = interactableGo.AddComponent<XRSimpleInteractable>();

        // setup model state for spawning
        blockSpawnerModel.BlockPrefabs = new[] { prefab };
        blockSpawnerModel.BlockScale = 1.0f;
        blockSpawnerModel.CurrentBlockIndex = 0;

        // setup xr events after interactable exists
        Assert.DoesNotThrow(() => comp.SetupXREvents());
        interactable.selectEntered.Invoke(new SelectEnterEventArgs());

        yield return null;

        // verify a block was spawned and model state updated
        Assert.IsNotNull(blockSpawnerModel.LastSpawnedBlock);
    }
}