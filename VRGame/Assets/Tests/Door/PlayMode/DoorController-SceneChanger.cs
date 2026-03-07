
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;
using System;


public class DoorController_SceneChanger
{

    [UnityTest]
    public IEnumerator OnPlayerEnter_InvalidPlayerController()
    {
        GameObject go = new GameObject();
        DoorController doorC = go.AddComponent<DoorController>();

        DoorModel doorM = go.AddComponent<DoorModel>();
        doorM.DestinationSceneId = 0;
        doorC.DoorModel = doorM;
        doorM.TargetDoorId = 2;

        // create target for our door
        DoorModel targetDoor = go.AddComponent<DoorModel>();
        targetDoor.DoorId = 2;
        targetDoor.TargetDoorId = 1;


        //create actual sceneChangerController
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        SceneManagerWrapper sceneMW = new SceneManagerWrapper();
        sceneC.SceneManagerWrapper = (ISceneManagerWrapper) sceneMW;
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
        doorM.DestinationSceneId = 0;
        doorC.DoorModel = doorM;
        doorM.DoorId = 1;
        doorM.TargetDoorId = 2;

        // create target for our door
        DoorModel targetDoor = go.AddComponent<DoorModel>();
        targetDoor.DoorId = 2;
        targetDoor.TargetDoorId = 1;


        //create actual sceneChangerController
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        SceneManagerWrapper sceneMW = new SceneManagerWrapper();
        sceneC.SceneManagerWrapper = sceneMW;
        doorC.SceneChangerController = sceneC;

        doorC.SceneChangerController = sceneC;

        // init
        yield return null;

        // enter 
        IPlayerController playerMock = Substitute.For<IPlayerController>();
        doorC.OnPlayerEnter(playerMock);

        yield return null;
        // trigger debounce should be true, to stop entrance logic from triggering multiple
        // times
        Assert.IsTrue(doorC.TriggerDebounce);


        // let finished event be detected
        yield return null;

        
        // trigger debounce should be false, to allow entrance again
        Assert.IsFalse(doorC.TriggerDebounce);

        doorM.ResetDoorLookup();
        UnityEngine.Object.Destroy(go);

        yield return null;
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