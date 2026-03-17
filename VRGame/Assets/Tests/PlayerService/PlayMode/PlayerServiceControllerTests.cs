using System;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR;
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
        // expect errors to occur since 'XRrigPrefab' variable is null.
        // these errors cannot be avoided since Awake() is called 
        // immediately after component is added.
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' variable was not set in inspector.");
        LogAssert.Expect(LogType.Exception, new Regex("'XRrigPrefab' cannot be null.*"));

        // immediately calls Awake()
        controller = go.AddComponent<PlayerServiceController>();
        
        yield return null;
    }

    public IEnumerator SpawnPlayerCreatesNewObjectWhenNoneExists()
    {
        // expect errors to occur since 'XRrigPrefab' variable is null.
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' variable was not set in inspector.");
        LogAssert.Expect(LogType.Exception, new Regex("'XRrigPrefab' cannot be null.*"));

        // immediately calls Awake()
        controller = go.AddComponent<PlayerServiceController>();

        GameObject XRrig = new GameObject();
        XRrig.AddComponent<PlayerController>();

        controller.MockXRrigPrefab = XRrig;

        Vector3 position = Vector3.zero;
        Quaternion rotation = new Quaternion();

        controller.SpawnPlayer(position, rotation);

        yield return null;
    }

    public IEnumerator SingletonDestroysDuplicateOnAwake()
    {
        yield return null;
    }

    public IEnumerator SpawnPlayerOnLoadWorksWhenEnabled()
    {
        yield return null;
    }
}