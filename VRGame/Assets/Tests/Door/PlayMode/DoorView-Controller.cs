
using System.Collections;
using System.Text.RegularExpressions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorView_Controller_Integration
{
    
    [UnityTest]
    public IEnumerator Instantiation()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        // real door controller, not mocked
        IDoorController doorC = go.AddComponent<DoorController>();
        doorV.DoorController = doorC;

        // if it is initialized without causing exception, we're good
        yield return null;

        Object.DestroyImmediate(go);
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

        Object.DestroyImmediate(go);
        yield return null;
    }


    [UnityTest]
    public IEnumerator NullCollider_OnTriggerEnter()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();
        IDoorController doorC = go.AddComponent<DoorController>();

        doorV.DoorController = doorC;

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

        Object.DestroyImmediate(go);
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


        Object.DestroyImmediate(colliderGo);
        Object.DestroyImmediate(go);

        yield return null;
    }


    [UnityTest]
    public IEnumerator OnTriggerEnter_MainCameraNoPlayerController()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = go.AddComponent<DoorController>();
        doorV.DoorController = doorC;

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

        Object.DestroyImmediate(colliderGo);
        Object.DestroyImmediate(go);

        yield return null;
    }


    //Player components don't seem to be testing ready yet, can't access fields within script
    //[UnityTest]
    /*
    public IEnumerator OnTriggerEnter_Player()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        yield return null;

        // create player to send through our door
        GameObject colliderGo = new GameObject();
        PlayerModel playerM = colliderGo.AddComponent<PlayerModel>();
        playerM.getPlayerName = ":)";
        playerM.getPlayerId = 0;

        PlayerController playerC = colliderGo.AddComponent<PlayerController>();
        playerC.G

        // no playerController within this gameObject
        GameObject colliderGo = new GameObject();
        PlayerController pc = colliderGo.AddComponent<PlayerController>();
        colliderGo.tag = "MainCamera";
        Collider otherC = colliderGo.AddComponent<BoxCollider>();




        doorV.OnTriggerEnter(otherC);
        // this log lets us know it worked
        LogAssert.Expect(LogType.Log, "Player collision handled");

        Object.DestroyImmediate(colliderGo);
        Object.DestroyImmediate(go);

        yield return null;

    }
*/


    // A UnityUnityTest behaves like a Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    public IEnumerator DoorViewUnityTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
