using System.Collections;
using System.Collections.Generic;
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
        Assert.NotNull(door);

        Object.DestroyImmediate(go);
    }
    [Test]
    public void SetDoorId()
    {
        GameObject go = new GameObject();
        IDoorModel door = go.AddComponent<DoorModel>();

        door.DoorId = 1;
        Assert.AreEqual(door.DoorId, 1);
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
        Assert.AreEqual(door1.TargetDoorId, 2);
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
