using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

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

        doorC.DoorModel = doorM;
        doorC.SceneChangerController = sceneC;
        doorC.Init();
        Assert.NotNull(doorC);
    }

    [Test]
    public void GetDoorModel()
    {
        GameObject go = new GameObject();
        IDoorController doorC = go.AddComponent<DoorController>();
        
        // mocking out door model 
        IDoorModel doorM = Substitute.For<IDoorModel>();
        doorC.DoorModel = doorM;
        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;
        
        doorC.Init();
        Assert.IsNotNull(doorC.DoorModel);

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
