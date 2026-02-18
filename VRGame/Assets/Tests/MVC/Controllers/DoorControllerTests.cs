using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;
using System;


public class DoorControllerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        IDoorController doorC = go.AddComponent<DoorController>();
        
        // mocking out door model 
        IDoorModel doorM = Substitute.For<IDoorModel>();
        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        Assert.NotNull(doorC);

        doorC.DoorModel = doorM;
        doorC.SceneChangerController = sceneC;
        doorC.Init();
        // no assertion triggered, meaning it worked

        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]
    public void triggerDebounceCheck()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        IDoorController doorC = go.AddComponent<DoorController>();
        
        // mocking out door model 
        IDoorModel doorM = Substitute.For<IDoorModel>();
        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        Assert.NotNull(doorC);

        doorC.DoorModel = doorM;
        doorC.SceneChangerController = sceneC;
        doorC.Init();

        // trigger debounce should default to false, or else scnen change can never
        // be triggered
        Assert.IsFalse(doorC.TriggerDebounce); 

        UnityEngine.Object.DestroyImmediate(go);
    }





    [Test]
    public void InvalidDoorModel()
    {
        GameObject go = new GameObject();
        IDoorController doorC = go.AddComponent<DoorController>();

        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;

        // should fail, need to set doorModel
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            doorC.Init();
            Assert.Fail("Null door model should've triggered assertion");

        } 
        catch
        {

        }
        

        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]

    public void InvalidSceneChanger()
    {
        GameObject go = new GameObject();
        IDoorController doorC = go.AddComponent<DoorController>();

        IDoorModel doorM = Substitute.For<IDoorModel>();
        doorC.DoorModel = doorM;

        // should fail, need to set sceneChangerController
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            doorC.Init();
            Assert.IsNotNull(null);

        } 
        catch
        {

        }
        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]
    public void OnPlayerEnter_InvalidPlayerController()
    {
        GameObject go = new GameObject();
        IDoorController doorC = go.AddComponent<DoorController>();
        
        // mocking out door model 
        IDoorModel doorM = Substitute.For<IDoorModel>();
        doorM.DestinationSceneId = 1;
        doorC.DoorModel = doorM;
        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;
        
        doorC.Init();

        //trying to call OnPlayerEnter with null playerController should cause error
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            doorC.OnPlayerEnter(null);
            Assert.Fail("Passing null playerController to OnPlayerEnter should've triggered assertion");
        }
        catch{}

        UnityEngine.Object.DestroyImmediate(go);

    }

    public void OnPlayerEnter_InvalidSceneId()
    {
        GameObject go = new GameObject();
        IDoorController doorC = go.AddComponent<DoorController>();
        
        // mocking out door model 
        IDoorModel doorM = Substitute.For<IDoorModel>();

        // invalid sceneId
        doorM.DestinationSceneId = -1;

        doorC.DoorModel = doorM;
        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;
        
        doorC.Init();

        IPlayerController playerMock = Substitute.For<IPlayerController>();

        //trying to call OnPlayerEnter with invalid scene Id
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            doorC.OnPlayerEnter(playerMock);
            Assert.Fail("DoorModel with invalid sceneId with OnPlayerEnter should've triggered assertion");
        }
        catch{}

        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]
    public void OnPlayerEnterValid()
    {
        GameObject go = new GameObject();
        IDoorController doorC = go.AddComponent<DoorController>();
        
        // mocking out door model 
        IDoorModel doorM = Substitute.For<IDoorModel>();
        doorM.DestinationSceneId = 0;
        doorC.DoorModel = doorM;

        // async operation that sceneChangerController will return
        IAsyncOperationWrapper loadingScene = Substitute.For<IAsyncOperationWrapper>();

        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        sceneC.LoadScene(0).Returns(loadingScene);
        doorC.SceneChangerController = sceneC;
        
        doorC.Init();

        // enter 
        IPlayerController playerMock = Substitute.For<IPlayerController>();
        doorC.OnPlayerEnter(playerMock);

        // trigger debounce should be true, to stop entrance logic from triggering multiple
        // times
        Assert.IsTrue(doorC.TriggerDebounce);

        // manually set mocked async event to completed
        loadingScene.Completed += Raise.Event<Action<IAsyncOperationWrapper>>(loadingScene);

        // trigger debounce should be false, to allow entrance again
        Assert.IsFalse(doorC.TriggerDebounce);

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
