using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;

public class DoorModelTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        DoorModel door = go.AddComponent<DoorModel>();
        // ensure doormodel instantiated correctly
        door.Init();
        Assert.NotNull(door);

        door.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }
    [Test]
    public void SetDoorId()
    {
        GameObject go = new GameObject();
        DoorModel door = go.AddComponent<DoorModel>();

        door.DoorId = 1;
        door.Init();

        Assert.AreEqual(door.DoorId, 1);

        door.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetTargetDoorId()
    {
        GameObject go = new GameObject();

        DoorModel door1 = go.AddComponent<DoorModel>();
        DoorModel door2 = go.AddComponent<DoorModel>();
        door1.DoorId = 1;
        door2.DoorId = 2;
        door1.TargetDoorId = 2;
        door2.TargetDoorId = 1;


        door1.Init();
        door2.Init();
        
        Assert.AreEqual(door1.TargetDoorId, 2);
        Assert.AreEqual(door2.TargetDoorId, 1);

        door1.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void InvalidTargetDoorId()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        DoorModel door = go.AddComponent<DoorModel>();
        // ensure doormodel instantiated correctly
        door.Init();

        // test should fire assertion, tell unity to ignore
        // associated error log
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            door.TargetDoorId = -1;
        }
        catch{}
        door.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetTargetDoor()
    {
        GameObject go = new GameObject();

        DoorModel door1 = go.AddComponent<DoorModel>();
        DoorModel door2 = go.AddComponent<DoorModel>();
        door1.DoorId = 1;
        door2.DoorId = 2;
        door1.TargetDoorId = 2;
        door2.TargetDoorId = 1;


        door1.Init();
        door2.Init();

        Assert.AreEqual(door1.GetTargetDoor(), door2);
        Assert.AreEqual(door2.GetTargetDoor(), door1); 

        door1.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void InvalidTargetDoor()
    {
        GameObject go = new GameObject();

        DoorModel door1 = go.AddComponent<DoorModel>();
        door1.DoorId = 1;
        door1.TargetDoorId = 2;

        door1.Init();

        // test should fail, but need to tell unity
        // to ignore error log, or else test will not pass
        LogAssert.Expect(LogType.Error, "Target door does not exist");
        try {
            door1.GetTargetDoor();
            Assert.IsTrue(1==2);
        }
        catch {}

        door1.ResetDoorLookup(); 
        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetDestinationSceneId()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        DoorModel door = go.AddComponent<DoorModel>();
        // ensure doormodel instantiated correctly
        door.DoorId = 0;
        door.TargetDoorId = 1;
        door.DestinationSceneId = 0;
        door.Init();

        Assert.AreEqual(door.DestinationSceneId, 0);
        door.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void InvalidDestinationSceneId()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        DoorModel door = go.AddComponent<DoorModel>();
        // ensure doormodel instantiated correctly

        // this test should fire assertion. Tell unity to ignore associated error message
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try {
            door.DestinationSceneId = -1;
            Assert.Fail();
        }
        catch{}
        door.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetTeleportPosition()
    {
        GameObject go = new GameObject();
        DoorModel door = go.AddComponent<DoorModel>();
        // ensure doormodel instantiated correctly

        door.Init();

        Assert.IsNotNull(door.GetTeleportPosition());
        // this test should fire assertion. Tell unity to ignore associated error message
        door.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetTeleportRotation()
    {
        GameObject go = new GameObject();
        DoorModel door = go.AddComponent<DoorModel>();
        // ensure doormodel instantiated correctly

        door.Init();

        Assert.IsNotNull(door.GetTeleportRotation());
        // this test should fire assertion. Tell unity to ignore associated error message
        door.ResetDoorLookup();
        Object.DestroyImmediate(go);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
