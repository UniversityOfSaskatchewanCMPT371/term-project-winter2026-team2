using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ServiceControllerTest
{
    GameObject go;
    ServiceController controller;

    /// <summary>
    /// Called once before every test functions. 
    /// Handles setting up the test environment.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        go = new GameObject();
        controller = go.AddComponent<ServiceController>();
    }

    /// <summary>
    /// Called once after every test functions.
    /// Handles cleanning up the test environment.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerable Instantiation()
    {
        yield return null;

        // no errors should occur since it doesn't interact with
        // any other layers
    }

    [UnityTest]
    public IEnumerable Singleton()
    {
        controller.Init();

        GameObject go2 = new GameObject();
        go2.AddComponent<ServiceController>();

        yield return null;

        // expected a warning since this is a duplicate of this singleton
        // game object of the duplicate its attached to is expected to be destroyed
    }
}