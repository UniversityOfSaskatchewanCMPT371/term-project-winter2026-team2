using System;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class PlayerServiceControllerTests
{
    GameObject go;
    PlayerServiceController controller;

    /// <summary>
    /// Called once before every test functions. 
    /// Handles setting up the test environment.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        go = new GameObject();
        controller = go.AddComponent<PlayerServiceController>();
    }

    /// <summary>
    /// Called once after every test functions.
    /// Handles cleanning up the test environment.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator Instantiation()
    {
        // expect errors to occur since 'XRrigPrefab' variable is null
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' variable was not set in inspector.");
        Assert.Throws<AssertionException>(() => controller.Init(), "Expected exception to be thrown.");

        yield return null;
    }

    public IEnumerator SpawnPlayerCreatesNewObjectWhenNoneExists()
    {
        contr
    }

    public IEnumerator SingletonDestroysDuplicateOnAwake()
    {
        
    }

    public IEnumerator SpawnPlayerOnLoadWorksWhenEnabled();
}