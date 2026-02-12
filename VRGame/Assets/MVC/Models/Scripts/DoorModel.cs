
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DoorModel : MonoBehaviour, IDoorModel {

    private static Dictionary<int, IDoorModel> doorLookup;

    private int doorId;
    public int DoorId {
        get {
            return doorId;
        }
        set {
            Debug.Assert(value >= 0, "doorId must be positive");
            doorId = value;
        }
    }

    private int targetDoorId;
    public int TargetDoorId {
        get {
            return targetDoorId;
        }
        set {
            Debug.Assert(value >= 0, "doorId must be positive");
            targetDoorId = value;
        }
    }

    public IDoorModel GetTargetDoor()
    {
        Debug.Assert(doorLookup.ContainsKey(targetDoorId));

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