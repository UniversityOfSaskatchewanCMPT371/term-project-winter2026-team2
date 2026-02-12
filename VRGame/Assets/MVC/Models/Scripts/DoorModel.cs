
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DoorModel : MonoBehaviour, IDoorModel {

    private static Dictionary<int, IDoorModel> doorLookup;

    private int doorId = 0;
    public int DoorId {
        get {
            return doorId;
        }
        set {
            Assert.IsTrue(value >= 0, "doorId must be positive");
            doorId = value;
        }
    }

    private int targetDoorId = 0;
    public int TargetDoorId {
        get {
            return targetDoorId;
        }
        set {
            Assert.IsTrue(value >= 0, "doorId must be positive");
            targetDoorId = value;
        }
    }

    public IDoorModel GetTargetDoor()
    {
        Assert.IsTrue(doorLookup.ContainsKey(targetDoorId));

        IDoorModel target = doorLookup[targetDoorId];

        return target;
    }

    public void Init()
    {
        if (doorLookup == null)
        {
            doorLookup = new Dictionary<int, IDoorModel>();
        }
        doorLookup[doorId] = this;
    }

}