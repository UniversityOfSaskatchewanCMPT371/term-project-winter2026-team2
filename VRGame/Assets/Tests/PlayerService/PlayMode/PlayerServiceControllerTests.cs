using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using System;

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
        controller.ResetStatic();
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

    [UnityTest]
    public IEnumerator Singleton()
    {
        // expect errors to occur since 'XRrigPrefab' variable is null.
        // these errors cannot be avoided since Awake() is called 
        // immediately after component is added.
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' variable was not set in inspector.");
        LogAssert.Expect(LogType.Exception, new Regex("'XRrigPrefab' cannot be null.*"));

        // immediately calls Awake() when component is added
        controller = go.AddComponent<PlayerServiceController>();

        // expect errors to occur since 'XRrigPrefab' variable is null.
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' variable was not set in inspector.");
        LogAssert.Expect(LogType.Exception, new Regex("'XRrigPrefab' cannot be null.*"));

        // expect a warning since this is a duplicate of this singleton
        LogAssert.Expect(LogType.Warning, "There can be only one active ServiceController.");

        // create a duplicate
        GameObject go2 = new GameObject();
        go2.AddComponent<PlayerServiceController>();

        // yield to let Destroy() run
        yield return null;

        Assert.IsTrue(go2 == null, "Expected duplicate game object to be destroyed.");

        yield return null;
    }
}