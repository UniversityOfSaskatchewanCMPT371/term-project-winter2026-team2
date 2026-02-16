
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEngine.PlayerLoop;

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

        try
        {
            sceneC2.Init();
            Assert.IsTrue(1==2);
        }
        catch
        {

        }
        Object.DestroyImmediate(go);
    }

    

    

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
