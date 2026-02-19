
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEngine.PlayerLoop;
using System.Text.RegularExpressions;
using NSubstitute.Extensions;
using System;
public class SceneChangerControllerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        
        ISceneManagerWrapper sMWrapper = Substitute.For<ISceneManagerWrapper>();
        sceneC.SceneManagerWrapper = sMWrapper;

        // if no exception triggered, ok
        sceneC.Init();


        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]
    public void DebounceCheck()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        
        ISceneManagerWrapper sMWrapper = Substitute.For<ISceneManagerWrapper>();
        sceneC.SceneManagerWrapper = sMWrapper;

        sceneC.Init();

        // loadDebounce should be false, allows scene to be loaded
        Assert.IsFalse(sceneC.LoadDebounce);

        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]
    public void Invalid_SceneManagerWrapper()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        ISceneChangerController sceneC = go.AddComponent<SceneChangerController>();

        // not setting sceneManagerWrapper 

        // test should cause error, tell unity to ignore error log
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try {
            sceneC.Init();
            Assert.Fail("Null sceneManagerWrapper should have triggered exception");
        }
        catch{}

        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
    }


    [Test]
    public void SingletonTest()
    {
        GameObject go = new GameObject();
        SceneChangerController sceneC1 = go.AddComponent<SceneChangerController>();

        ISceneManagerWrapper sMWrapper = Substitute.For<ISceneManagerWrapper>();
        sceneC1.SceneManagerWrapper = sMWrapper;
        sceneC1.Init();


        SceneChangerController sceneC2 = go.AddComponent<SceneChangerController>();
        sceneC2.SceneManagerWrapper = sMWrapper;
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
        UnityEngine.Object.DestroyImmediate(go);
    }


    [Test]
    public void LoadScene()
    {


        // mock out async return value
        IAsyncOperationWrapper aMock = Substitute.For<IAsyncOperationWrapper>();

        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        
        ISceneManagerWrapper sMWrapper = Substitute.For<ISceneManagerWrapper>();
        sMWrapper.LoadSceneAsync(0).Returns(aMock);

        sceneC.SceneManagerWrapper = sMWrapper;

        sceneC.Init();

        // trigger async operation completion manually 
        // Completed is an Action<IAsyncOperationWrapper>, so raising event on that
        aMock.Completed += Raise.Event<Action<IAsyncOperationWrapper>>(aMock);

        sceneC.LoadScene(0);

        // sceneChanger should now prevent other attempts to load scene
        Assert.IsTrue(sceneC.LoadDebounce);

        // trigger async operation completion manually 
        // Completed is an Action<IAsyncOperationWrapper>, so raising event on that
        aMock.Completed += Raise.Event<Action<IAsyncOperationWrapper>>(aMock);


        // sceneChanger should now allow other attempts to load scene
        Assert.IsFalse(sceneC.LoadDebounce);


        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]
    public void NonExistent_LoadScene()
    {
        IAsyncOperationWrapper aMock = Substitute.For<IAsyncOperationWrapper>();

        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        
        ISceneManagerWrapper sMWrapper = Substitute.For<ISceneManagerWrapper>();
        sMWrapper.LoadSceneAsync(0).Returns(aMock);

        sceneC.SceneManagerWrapper = sMWrapper;

        sceneC.Init();

        // trigger async operation completion manually 
        // Completed is an Action<IAsyncOperationWrapper>, so raising event on that
        aMock.Completed += Raise.Event<Action<IAsyncOperationWrapper>>(aMock);

        LogAssert.Expect(LogType.Error, "Invalid sceneKey passed to LoadScene. Not in enum");
        try {
            // won't have negative scene ids ever
            sceneC.LoadScene(-1);
            Assert.Fail("Loading invalid sceneId should've triggered assertion");
        }
        catch {}

        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
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
