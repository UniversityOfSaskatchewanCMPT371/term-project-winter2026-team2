using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorViewTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        GameObject go = new GameObject();
        IDoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        doorV.Init();

        Assert.IsNotNull(doorV.DoorController);

        Object.DestroyImmediate(go);
    }

    [Test]
    public void InvalidDoorController()
    {
        GameObject go = new GameObject();
        IDoorView doorV = go.AddComponent<DoorView>();


        // this test should trigger assertion, tell unity to ignore associated
        // error log
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try {
            doorV.Init();
            Assert.Fail("Attempting to init without setting doorController should trigger assertion");
        }
        catch{}

        Object.DestroyImmediate(go);
    }


    [Test]
    public void NullCollider_OnTriggerEnter()
    {
        GameObject go = new GameObject();
        IDoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        doorV.Init();

        // this test should trigger assertion, tell unity to ignore associated
        // error log
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try {
            doorV.OnTriggerEnter(null);
            Assert.Fail("OnTriggerEnter with null collider should trigger assertion");
        }
        catch{}

        Object.DestroyImmediate(go);
    }

    [Test]
    public void OnTriggerEnter_NonPlayerCollider()
    {
        GameObject go = new GameObject();
        IDoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        doorV.Init();

        // I can't properly mock out unity types like this. Just make a collider
        // without MainCamera tag.

        //Collider otherM = Substitute.For<Collider>();
        //otherM.gameObject.Returns(Substitute.For<GameObject>());
        //otherM.gameObject.CompareTag("MainCamera").Returns(false);
        Collider otherM = go.AddComponent<BoxCollider>();
        otherM.gameObject.tag = "Untagged";

        doorV.OnTriggerEnter(otherM);
        // this log message indicates a non-player collision has been handled properly
        LogAssert.Expect(LogType.Log, "Component other than player collided with door");


        Object.DestroyImmediate(go);
    }


    [Test]
    public void OnTriggerEnter_MainCameraNoPlayerController()
    {
        GameObject go = new GameObject();
        IDoorView doorV = go.AddComponent<DoorView>();

        IDoorController doorC = Substitute.For<IDoorController>();
        doorV.DoorController = doorC;

        doorV.Init();

        // I can't properly mock out unity types like this. Just make a collider
        // with MainCamera tag. It also needs to include PlayerController in parent hierarchy,
        // so making a second gameObject.

        GameObject colliderParent = new GameObject();
        Collider otherM = colliderParent.AddComponent<BoxCollider>();
        otherM.gameObject.tag = "MainCamera";

        // test should fail, tell unity to ignore associated error log
        LogAssert.Expect(LogType.Error, "Collider does not contain playerController component");
        try {
            doorV.OnTriggerEnter(otherM);
            Assert.Fail("Improperly set up camera collider's parent does not contain playerController component. Should have triggered assertion");
        }
        catch
        {
        }

        Object.DestroyImmediate(go);
    }

    //NOTE - As playerController isn't an actual monobehavior, I can't attach
    // it to a gameObject. TODO - figure out how to properly unit test Player properly
    // colliding with door


    // A UnityTest behaves like a Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DoorViewTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
