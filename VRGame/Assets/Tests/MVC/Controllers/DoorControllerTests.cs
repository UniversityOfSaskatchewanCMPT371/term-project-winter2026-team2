using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;


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
        
        Object.DestroyImmediate(go);
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
            Assert.IsNotNull(null);

        } 
        catch
        {

        }
        

        Object.DestroyImmediate(go);
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
        

        Object.DestroyImmediate(go);
    }

    /* Having a lot of trouble  mocking out AsyncOperations 
    [Test]
    public void OnPlayerEnter()
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


        IPlayerController Ipc = Substitute.For<IPlayerController>();

        // target door returned from doorModel within function
        IDoorModel targetMock = Substitute.For<IDoorModel>();
        doorM.GetTargetDoor().Returns(targetMock);
        targetMock.GetTeleportPosition().Returns(new Vector3());
        targetMock.GetTeleportRotation().Returns(new Quaternion());

        // async operation within onPlayerEnter from sceneChangerController
        AsyncOperation asyncMock = Substitute.For<AsyncOperation>();
        sceneC.LoadScene(doorM.DestinationSceneId).Returns(asyncMock);

        // should not throw exception
        doorC.OnPlayerEnter(Ipc);

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
