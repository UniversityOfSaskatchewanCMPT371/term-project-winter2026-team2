using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Collections;

public class BlockSpawnSystemTest
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;

    /// <summary>
    /// The component that is being tested.
    /// TODO : Replace the type to the class you are testing.
    /// </summary>
    MonoBehaviour comp;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [UnitySetUp]
    public IEnumerator Setup()
    {
        go = new GameObject();

        // NOTE: adding component in play mode will automatically call Awake() & Start().
        // If necessary, you may move this directly in the test function instead
        comp = go.AddComponent<Block>(); // TODO : Replace generic with component you are testing
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
        yield return null;
    }

    [UnityTest]
    public IEnumerator Instantiation()
    {
        yield return null;
    }
}