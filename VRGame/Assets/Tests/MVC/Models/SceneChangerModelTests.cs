
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

public class SceneChangerModelTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        ISceneChangerModel scm = go.AddComponent<SceneChangerModel>();
        Assert.NotNull(scm);

        scm.Init();
        // make sure list of scene paths initialized
        Assert.NotNull(scm.ScenePaths);

        Object.DestroyImmediate(go);
    }

    [Test]
    public void Singleton()
    {
        GameObject go = new GameObject();
        ISceneChangerModel scm1 = go.AddComponent<SceneChangerModel>();
        scm1.Init();
        ISceneChangerModel scm2 = go.AddComponent<SceneChangerModel>();

        // trying to init a second SceneChangerModel should throw exception
        try
        {
            scm2.Init();
            Assert.IsTrue(1==2);
        }
        catch
        {
        }
        Object.DestroyImmediate(go);
    }

    [Test]
    public void DummyScenePath()
    {
        GameObject go = new GameObject();
        ISceneChangerModel scm = go.AddComponent<SceneChangerModel>();
        scm.Init();

        string dummyPath = "scene1";
        scm.ScenePaths.Add(dummyPath);
        Assert.IsTrue(scm.ScenePaths.Contains("scene1"));
        Object.DestroyImmediate(go);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
