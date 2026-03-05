
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
        doorV.Init();


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

        doorV.Init();

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
    public void OnTriggerEnter_NonPlayerCollider()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        doorV.Init();

        // mocked out collider which does not have main camera tag in gameObject,
        // i.e. is not the player
        IColliderWrapper otherC = Substitute.For<IColliderWrapper>();
        otherC.CompareGameObjectTag("MainCamera").Returns(false);

        doorV.OnTriggerEnterLogic(otherC);
        // this log message indicates a non-player collision has been handled properly
        LogAssert.Expect(LogType.Log, "Component other than player collided with door");


        Object.DestroyImmediate(go);
    }


    [UnityTest]
    public void OnTriggerEnter_MainCameraNoPlayerController()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        doorV.Init();

        // create improperly set up collider; has MainCamera tag but not IPlayerController component in
        // it's gameObject parent. This should cause an error
        IColliderWrapper otherC = Substitute.For<IColliderWrapper>();
        otherC.CompareGameObjectTag("MainCamera").Returns(true);

        otherC.GetPlayerFromParent().Returns((IPlayerController)null);

        LogAssert.Expect(LogType.Error, "Collider does not contain playerController component");
        try
        {
            doorV.OnTriggerEnterLogic(otherC);
            Assert.Fail("Improperly set up camera collider's parent does not contain playerController component. Should have triggered assertion");
        }
        catch
        {
        }

        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public void OnTriggerEnter_Player()
    {
        GameObject go = new GameObject();
        DoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        doorV.Init();

        // create improperly set up collider; has MainCamera tag but not IPlayerController component in
        // it's gameObject parent. This should cause an error
        IColliderWrapper otherC = Substitute.For<IColliderWrapper>();
        otherC.CompareGameObjectTag("MainCamera").Returns(true);

        // playerController getPlayerFromParent will return
        IPlayerController pMock = Substitute.For<IPlayerController>();
        otherC.GetPlayerFromParent().Returns(pMock);


        doorV.OnTriggerEnterLogic(otherC);
        // this log lets us know it worked
        LogAssert.Expect(LogType.Log, "Player collision handled");

    }



    // A UnityUnityTest behaves like a Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    public IEnumerator DoorViewUnityTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
