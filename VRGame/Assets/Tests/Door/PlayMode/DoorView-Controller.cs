
using System.Collections;
using System.Text.RegularExpressions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorView_Controller_Integration
{
    
    [UnityTest]
    public IEnumerator InstantiateDoorView()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        // real door controller, not mocked
        DoorController doorC = go.AddComponent<DoorController>();
        doorV.DoorController = doorC;

        // doorController needs model reference to init properly        
        IDoorModel doorMock = Substitute.For<IDoorModel>();
        doorC.DoorModel = doorMock;


        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;

        // if it is initialized without causing exception, we're good
        yield return null;

        Object.Destroy(go);
        yield return null;
    }

    [UnityTest]
    public IEnumerator InvalidDoorController()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();


        // this test should trigger assertion, tell unity to ignore associated
        // error log
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            doorV.Init();
            Assert.Fail("Attempting to init without setting doorController should trigger assertion");
        }
        catch { }

        Object.Destroy(go);
        yield return null;
    }


    [UnityTest]
    public IEnumerator NullCollider_OnTriggerEnter()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();
        DoorController doorC = go.AddComponent<DoorController>();

        doorV.DoorController = doorC;
        // doorController needs model reference to init properly        
        IDoorModel doorMock = Substitute.For<IDoorModel>();
        doorC.DoorModel = doorMock;

        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;
        // init
        yield return null;

        // this test should trigger assertion, tell unity to ignore associated
        // error log
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            doorV.OnTriggerEnter(null);
            Assert.Fail("OnTriggerEnter with null collider should trigger assertion");
        }
        catch { }

        Object.Destroy(go);
        yield return null;
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter_NonPlayerCollider()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        yield return null;

        // actual collider object
        GameObject colliderGo = new GameObject();
        // non "MainCamera" tag
        Collider otherC = colliderGo.AddComponent<BoxCollider>();

        // actual function called, not separated logic function
        doorV.OnTriggerEnter(otherC);

        // this log message indicates a non-player collision has been handled properly
        LogAssert.Expect(LogType.Log, "Component other than player collided with door");


        Object.Destroy(colliderGo);
        Object.Destroy(go);

        yield return null;
    }


    [UnityTest]
    public IEnumerator OnTriggerEnter_MainCameraNoPlayerController()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        DoorController doorC = go.AddComponent<DoorController>();
        doorV.DoorController = doorC;

        // doorController needs model reference to init properly        
        IDoorModel doorMock = Substitute.For<IDoorModel>();
        doorC.DoorModel = doorMock;

        ISceneChangerController sceneC = Substitute.For<ISceneChangerController>();
        doorC.SceneChangerController = sceneC;
        
        yield return null;

        // no playerController within this gameObject
        GameObject colliderGo = new GameObject();
        colliderGo.tag = "MainCamera";
        Collider otherC = colliderGo.AddComponent<BoxCollider>();

        LogAssert.Expect(LogType.Error, "Collider does not contain playerController component");
        try
        {
            doorV.OnTriggerEnter(otherC);
            Assert.Fail("Improperly set up camera collider's parent does not contain playerController component. Should have triggered assertion");
        }
        catch
        {
        }

        Object.Destroy(colliderGo);
        Object.Destroy(go);

        yield return null;
    }


}
