using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        IDoorModel door = go.AddComponent<DoorModel>();
        // ensure doormodel instantiated correctly
        door.Init();
        Assert.NotNull(door);

        Object.DestroyImmediate(go);
    }
    [Test]
    public void SetDoorId()
    {
        GameObject go = new GameObject();
        IDoorModel door = go.AddComponent<DoorModel>();

        door.DoorId = 1;
        door.Init();

        Assert.AreEqual(door.DoorId, 1);
        Object.DestroyImmediate(go);
    }

    [Test]
    public void SetTargetDoorId()
    {
        GameObject go = new GameObject();

        IDoorModel door1 = go.AddComponent<DoorModel>();
        IDoorModel door2 = go.AddComponent<DoorModel>();
        door1.DoorId = 1;
        door2.DoorId = 2;
        door1.TargetDoorId = 2;
        door2.TargetDoorId = 1;


        door1.Init();
        door2.Init();
        
        Assert.AreEqual(door1.TargetDoorId, 2);
        Assert.AreEqual(door2.TargetDoorId, 1);
    }

    [Test]
    public void GetTargetDoor()
    {
        GameObject go = new GameObject();

        IDoorModel door1 = go.AddComponent<DoorModel>();
        IDoorModel door2 = go.AddComponent<DoorModel>();
        door1.DoorId = 1;
        door2.DoorId = 2;
        door1.TargetDoorId = 2;
        door2.TargetDoorId = 1;


        door1.Init();
        door2.Init();

        Assert.AreEqual(door1.GetTargetDoor(), door2);
        Assert.AreEqual(door2.GetTargetDoor(), door1); 
    }

    [Test]
    public void InvalidTargetDoor()
    {
        GameObject go = new GameObject();

        IDoorModel door1 = go.AddComponent<DoorModel>();
        IDoorModel door2 = go.AddComponent<DoorModel>();
        door1.DoorId = 1;
        door1.TargetDoorId = 2;

        door1.Init();
        door2.Init();

        //TODO: Find better way to do this
        try {
            door1.GetTargetDoor();
            Assert.IsTrue(1==2);
        }
        catch {}
            
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
