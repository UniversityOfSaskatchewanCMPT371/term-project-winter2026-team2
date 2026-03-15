using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
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
        // adding the component immediately invokes Awake()
        controller = go.AddComponent<ServiceController>();

        // no errors should occur since it doesn't interact with
        // any other layers

        yield return null;
    }

    [UnityTest]
    public IEnumerator Singleton()
    {
        // adding the component immediately invokes Awake()
        controller = go.AddComponent<ServiceController>();

        // set up the duplicate
        GameObject go2 = new GameObject();

        // expect a warning since this is a duplicate of this singleton
        LogAssert.Expect(LogType.Warning, "There can be only one active ServiceController.");

        // add component to invoke Awake()
        go2.AddComponent<ServiceController>();

        // yield to let Destroy() run
        yield return null;

        // game object of the duplicate its attached to is expected to be destroyed
        Assert.IsTrue(go2 == null, "Expected duplicate game object to be destroyed.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator Peristence()
    {
        // adding the component immediately invokes Awake()
        controller = go.AddComponent<ServiceController>();

        // load a new scene
        SceneManager.LoadSceneAsync((int)SceneEnum.TestScene);

        // let scene load
        yield return null;

        // game object should not be destroyed since it is kept persistent
        Assert.IsNotNull(go, "Expected game object to not be destroyed on scene transition.");

        yield return null;
    }
}