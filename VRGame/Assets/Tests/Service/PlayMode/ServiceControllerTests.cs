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
        controller = go.AddComponent<ServiceController>();
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
        // skip a frame to call Awake() and invoke Init()
        yield return null;

        // no errors should occur since it doesn't interact with
        // any other layers
    }

    [UnityTest]
    public IEnumerator Singleton()
    {
        // skip a frame to call Awake() and invoke Init()
        yield return null;

        // set up the duplicate
        GameObject go2 = new GameObject();
        go2.AddComponent<ServiceController>();

        // expect a warning since this is a duplicate of this singleton
        LogAssert.Expect(LogType.Warning, "There can be only one active ServiceController.");

        // skip a frame to call Awake() and invoke Init()
        yield return null;

        // game object of the duplicate its attached to is expected to be destroyed
        Assert.IsTrue(go2 == null, "Expected duplicate game object to be destroyed.");
    }

    [UnityTest]
    public IEnumerator Peristence()
    {
        // skip a frame to call Awake() and invoke Init()
        yield return null;

        // load a new scene
        SceneManager.LoadSceneAsync((int)SceneEnum.TestScene);

        // skip a frame to allow the scene to load
        yield return null;

        // game object should not be destroyed since it is kept persistent
        Assert.IsFalse(go == null, "Expected game object to not be destroyed on scene transition.");
    }
}