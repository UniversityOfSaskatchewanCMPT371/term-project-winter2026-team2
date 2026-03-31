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
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' variable is null.");
        LogAssert.Expect(LogType.Exception, new Regex("'XRrigPrefab' variable was not set in inspector.*"));

        // immediately calls Awake()
        controller = go.AddComponent<PlayerServiceController>();
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator Singleton()
    {

        // expect errors to occur since 'XRrigPrefab' variable is null.
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' variable is null.");
        LogAssert.Expect(LogType.Exception, new Regex("'XRrigPrefab' variable was not set in inspector.*"));

        // immediately calls Awake() when component is added
        controller = go.AddComponent<PlayerServiceController>();

        // mock the xr rig with player controller component
        GameObject XRrigMock = new GameObject();
        XRrigMock.AddComponent<PlayerController>();

        // create a duplicate
        GameObject go2 = new GameObject();

        // immediately calls Awake() when component is added
        PlayerServiceController controller2 = go2.AddComponent<PlayerServiceController>();

        // this is needed to actually test the singleton, otherwise it Init() would exit early
        Assert.DoesNotThrow(() => {controller2.MockXRrigPrefab = XRrigMock;}, "No exception expected.");

        LogAssert.Expect(LogType.Exception, new Regex("PlayerModel reference cannot be null.*"));

        // need to initialize again because XRrigPrefab was not set before Awake() was called
        controller2.Init();

        // yield to let Destroy() run
        yield return null;

        Assert.IsTrue(go2 == null, "Expected duplicate game object to be destroyed.");

        yield return null;
    }
}