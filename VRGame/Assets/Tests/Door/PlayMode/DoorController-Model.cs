
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;
using System;


public class DoorController_Model
{
    // A Test behaves as an ordinary method
    [UnityTest]
    public IEnumerator Instantiation()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        DoorController doorC = go.AddComponent<DoorController>();

        // real door model   
        DoorModel doorM = go.AddComponent<DoorModel>();
        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        Assert.NotNull(doorC);

        doorC.DoorModel = doorM;
        doorC.SceneChangerController = sceneC;

        // instead of calling Init(), yield return null. This skips a frame, will call awake.
        yield return null;
        // no assertion triggered, meaning it worked

        doorM.ResetDoorLookup();
        UnityEngine.Object.DestroyImmediate(go);
        yield return null;
    }

    [UnityTest]
    public IEnumerator triggerDebounceCheck()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        DoorController doorC = go.AddComponent<DoorController>();

        // mocking out door model 
        DoorModel doorM = go.AddComponent<DoorModel>();
        doorM.ResetDoorLookup();
        doorM.DoorId = 1;
        doorM.Init();
        doorC.DoorModel = doorM;

        
        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;


        // trigger debounce should default to false, or else scnen change can never
        // be triggered
        
        yield return null;

        Assert.IsFalse(doorC.TriggerDebounce);


        doorM.ResetDoorLookup();
        UnityEngine.Object.DestroyImmediate(go);
        yield return null;
    }





    [UnityTest]
    public IEnumerator InvalidDoorModel()
    {
        GameObject go = new GameObject();
        DoorController doorC = go.AddComponent<DoorController>();

        ISceneChangerController sceneC = go.AddComponent<SceneChangerController>();
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
        yield return null;
    }

    [UnityTest]

    public IEnumerator InvalidSceneChanger()
    {
        GameObject go = new GameObject();
        DoorController doorC = go.AddComponent<DoorController>();

        DoorModel doorM = go.AddComponent<DoorModel>();
        doorM.ResetDoorLookup();
        doorC.DoorModel = doorM;
        doorM.DoorId = 1;

        doorM.Init();
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
        yield return null;
    }

    [UnityTest]
    public IEnumerator OnPlayerEnter_InvalidPlayerController()
    {
        GameObject go = new GameObject();
        DoorController doorC = go.AddComponent<DoorController>();

        DoorModel doorM = go.AddComponent<DoorModel>();
        doorM.DoorId = 1;
        doorC.DoorModel = doorM;
        doorM.TargetDoorId = 2;
        doorM.ResetDoorLookup();

        // create target for our door
        DoorModel targetDoor = go.AddComponent<DoorModel>();
        targetDoor.DoorId = 2;
        targetDoor.TargetDoorId = 1;
        doorM.ResetDoorLookup();

        doorM.Init();

        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;


        yield return null;
        //trying to call OnPlayerEnter with null playerController should cause error
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            doorC.OnPlayerEnter(null);
            Assert.Fail("Passing null playerController to OnPlayerEnter should've triggered assertion");
        }
        catch { }

        doorM.ResetDoorLookup();
        UnityEngine.Object.DestroyImmediate(go);
        yield return null;
    }

    // invalid scene ID test doesn't make sense in play mode, as this is checked when door is initialized
    // No need to adapt it



    [UnityTest]
    public IEnumerator OnPlayerEnterValid()
    {
        GameObject go = new GameObject();
        DoorController doorC = go.AddComponent<DoorController>();

        DoorModel doorM = go.AddComponent<DoorModel>();
        doorM.ResetDoorLookup();
        doorM.DestinationSceneId = 0;
        doorC.DoorModel = doorM;
        doorM.DoorId = 1;
        doorM.TargetDoorId = 2;

        // create target for our door
        DoorModel targetDoor = go.AddComponent<DoorModel>();
        doorM.ResetDoorLookup();
        targetDoor.DoorId = 2;
        targetDoor.TargetDoorId = 1;


        doorM.Init();
        targetDoor.Init();
        // async operation that sceneChangerController will return
        IAsyncOperationWrapper loadingScene = Substitute.For<IAsyncOperationWrapper>();

        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        sceneC.LoadScene(0).Returns(loadingScene);
        doorC.SceneChangerController = sceneC;

        // init
        yield return null;

        // enter 
        IPlayerController playerMock = Substitute.For<IPlayerController>();
        doorC.OnPlayerEnter(playerMock);

        // trigger debounce should be true, to stop entrance logic from triggering multiple
        // times
        Assert.IsTrue(doorC.TriggerDebounce);

        // manually set mocked async event to completed
        loadingScene.Completed += Raise.Event<Action<IAsyncOperationWrapper>>(loadingScene);

        // let finished event be detected
        while (doorC.TriggerDebounce) {
            yield return null;
        }

        
        // trigger debounce should be false, to allow entrance again
        Assert.IsFalse(doorC.TriggerDebounce);

        UnityEngine.Object.DestroyImmediate(go);

        yield return null;
    }

}