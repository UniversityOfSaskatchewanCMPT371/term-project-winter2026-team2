using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// This object holds data for a door object.
/// </summary>
public class DoorData : MonoBehaviour
{
    // Start is called before the first frame update
    private static Dictionary<int, DoorData> doorLookup { get; set; }   // Holds a dictionary of DoorData classes using their unique ids

    public Scenes sceneDestination = Scenes.Hub;                        // Used to determine which scene this door leads to
    public int doorId;                                                  // Used to uniquely identify this door
    public int targetDoorId;                                            // Used to uniquely identify the target door this will lead to
    public Vector3 teleportOffset = new Vector3(0, 0, 1f);              // Used when a player exits this door in which they are teleported using this offset facing forwards

    /// <summary>
    /// 
    /// When this script is loaded it will process sanity checks, and
    /// add this DoorData in the doorLookup dictionary.
    /// 
    /// </summary>
    private void Awake()
    {
        Assert.IsFalse(doorLookup.ContainsKey(doorId), "DoorId is not unique");

        doorLookup[doorId] = this;
    }

    /// <summary>
    /// 
    /// Retrieves this door's destination door.
    /// 
    /// </summary>
    /// <returns>The DoorData associated with this door's targetDoorId.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the door targets itself or the target door cannot be found.</exception>
    public DoorData GetTargetDoor()
    {
        if (doorId == targetDoorId)
            throw new InvalidOperationException($"Door {doorId} cannot target itself.");

        if (!doorLookup.ContainsKey(targetDoorId))
            throw new InvalidOperationException($"Door {doorId} cannot find target door {targetDoorId}.");

        return doorLookup[targetDoorId];
    }

    /// <summary>
    /// 
    /// Retrieves this door's teleport position in world space.
    /// 
    /// </summary>
    /// <returns>The door's teleport position in world space.</returns>
    public Vector3 GetTeleportPosition()
    {
        return transform.TransformPoint(teleportOffset);
    }

    /// <summary>
    /// 
    /// Retrieves this door's teleport rotation in world space.
    /// 
    /// </summary>
    /// <returns>The door's teleport rotation position in world space.</returns>
    public Quaternion GetTeleportRotation()
    { 
        return Quaternion.LookRotation(transform.forward, Vector3.up);
    }
}