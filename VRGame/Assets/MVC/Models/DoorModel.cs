
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
            Debug.Assert(value >= 0);

        }
    }


}