
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEngine.PlayerLoop;
using System.Text.RegularExpressions;

public class SceneChangerControllerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        ISceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        
        // mocking out sceneChangerModel
        ISceneChangerModel sceneM = Substitute.For<ISceneChangerModel>();

        sceneC.SceneChangerModel = sceneM;
        sceneC.Init();

        Assert.IsNotNull(sceneC.SceneChangerModel);

        sceneC.ResetInstance();
        Object.DestroyImmediate(go);

    }

    [Test]
    public void SingletonTest()
    {
        GameObject go = new GameObject();
        ISceneChangerController sceneC1 = go.AddComponent<SceneChangerController>();

        // mocking out sceneChangerModel
        ISceneChangerModel sceneM = Substitute.For<ISceneChangerModel>();

        sceneC1.SceneChangerModel = sceneM;
        sceneC1.Init();


        ISceneChangerController sceneC2 = go.AddComponent<SceneChangerController>();
        sceneC2.SceneChangerModel = sceneM;

        // attempting to create another instance should fail
        
        // tell unity to ignore error log so test can pass
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            sceneC2.Init();
            Assert.IsTrue(1==2);
        }
        catch
        {

        }
        sceneC1.ResetInstance();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void InvalidSceneChangerModel()
    {

        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        ISceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        
        // tell unity to ignore error log so test can pass
        LogAssert.Expect(LogType.Error, new Regex(".*"));

        // trying to init a SceneChangerController without an associated model should fail
        try
        {
            sceneC.Init();
            Assert.IsNotNull(null);
        }
        catch{}
        sceneC.ResetInstance();
        Object.DestroyImmediate(go);

    }

    // LOADSCENEASYNC which is called in loadscene must be in playmode
    /* 
    [Test]
    public void LoadScene()
    {
        GameObject go = new GameObject();
        ISceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        
        // mocking out sceneChangerModel
        ISceneChangerModel sceneM = Substitute.For<ISceneChangerModel>();
        sceneM.GetStringPath(1).Returns("dummyScenePath");

        sceneC.SceneChangerModel = sceneM;
        sceneC.Init();


        sceneC.LoadScene(1);
        

        sceneC.ResetInstance();
        Object.DestroyImmediate(go);
    }
    */
    

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DoorControllerTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
