
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;

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
    public void SingletonCheck()
    {
        GameObject go = new GameObject();
        ISceneChangerModel scm1 = go.AddComponent<SceneChangerModel>();
        scm1.Init();
        ISceneChangerModel scm2 = go.AddComponent<SceneChangerModel>();

        // trying to init a second SceneChangerModel should throw exception

        // tell unity to ignore error log, or else test won't pass 
        LogAssert.Expect(LogType.Error, "SceneChangerModel instance already exists");
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
    public void ScenePathDictLookup()
    {
        GameObject go = new GameObject();
        ISceneChangerModel scm = go.AddComponent<SceneChangerModel>();
        scm.Init();

        string dummyPath = "scene1";
        scm.ScenePaths.Add(1, dummyPath);
        Assert.IsTrue(scm.ScenePaths[1] == "scene1");
        Object.DestroyImmediate(go);
    }

    [Test]
    public void NonExistantScenePathDictLookup()
    {
        // new instantiation, dict should be empty
        GameObject go = new GameObject();
        ISceneChangerModel scm = go.AddComponent<SceneChangerModel>();
        scm.Init();

        try {
            string check = scm.ScenePaths[1];
            Assert.Fail("Searching nonexistent dict element should throw exception");
        }
        catch{} 
        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetStringPath()
    {
        GameObject go = new GameObject();
        ISceneChangerModel scm = go.AddComponent<SceneChangerModel>();
        scm.Init();

        string dummyPath = "scene1";
        scm.ScenePaths.Add(1, dummyPath);
        Assert.IsTrue(scm.GetStringPath(1) == "scene1");
        Object.DestroyImmediate(go);
    }

    [Test]
    public void NonexistentGetStringPath()
    {
        GameObject go = new GameObject();
        ISceneChangerModel scm = go.AddComponent<SceneChangerModel>();
        scm.Init();

        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try {
            scm.GetStringPath(1);
            Assert.Fail("Assertion should have fired from using nonexistant key in string path dict");
        }
        catch {}
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
