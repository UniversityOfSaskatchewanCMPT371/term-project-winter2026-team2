
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model portion of the reusable door module. Data is stored here
/// </summary>
/// <remarks>
/// - doorId and targetDoorId, destinationSceneId must be set before calling Init(), targetDoorId must exist.
/// targetSceneId must exist in SceneChangerModel service's path collection
/// </remarks>
public class DoorModel : MonoBehaviour, IDoorModel {

    /// <summary>
    /// A static lookup table visible to all DoorModels. Used to Ensure that
    /// a door's `targetDoorId` actually exists
    /// </summary>
    private static Dictionary<int, IDoorModel> doorLookup;


    /// <summary>
    /// Integer Id associated with this door.
    /// </summary>
    private int doorId;
    /// <summary>
    /// Public accessor for door's id
    /// </summary>
    public int DoorId {
        /// <summary>
        /// Access the DoorModel's Id
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - DoorModel's id is returned
        get {
            return doorId;
        }

        /// <summary>
        /// Set the id of this DoorModel
        /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
        /// scripts are usually set in a GUI window within the Unity editor 
        /// </summary>
        /// <remarks>
        /// Precondintions:
        /// - value must be positive
        /// Postconditions:
        /// - DoorModel's `doorId` instance variable set to input value
        set {
            if (value < 0)
            {
                Debug.LogError("value passed to setDoorId is negative");
            }
            Assert.IsTrue(value >= 0, "doorId must be positive");
            doorId = value;
        }
    }

    /// <summary>
    /// Id of another door that this door targets
    /// </summary>
    private int targetDoorId ;

    /// <summary>
    /// Public accessor for this door's target id 
    /// </summary>
    public int TargetDoorId {

        /// <summary>
        /// Access this DoorModel's target door Id
        /// </summary> 
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - DoorModel's `targetDoorId` instance variable is returned
        /// </remarks>
        get {
            return targetDoorId;
        }

        /// <summary>
        /// Set the value of the DoorModel's targetDoorId instance variable
        /// Note: This is for unit testing purposes - the instance variables of Monobehavior 
        /// scripts are usually set in a GUI window in the Unity editor
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be positive
        /// Postconditions:
        /// - DoorModel's `targetDoorId` instance variable set to input value
        /// </remarks>
        set {
            if (value < 0)
            {
                Debug.LogError("value passed to setTargetDoorId is negative");
            }
            Assert.IsTrue(value >= 0, "doorId must be positive");
            targetDoorId = value;
        }
    }

    /// <summary>
    /// Id of the scene this door is targeting. Id should exist as a key
    /// in SceneChangerModel's path collection
    /// </summary>
    private int destinationSceneId;

    /// <summary>
    /// Public accessor for DoorModel's destinationSceneId instance variable
    /// </summary>
    public int DestinationSceneId
    {
        /// <summary>
        /// Access this DoorModel's destination scene Id
        /// </summary> 
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - DoorModel's `destinationSceneId` instance variable is returned
        /// </remarks>
        get
        {
            return destinationSceneId;
        }
        /// <summary>
        /// Set the value of the DoorModel's destinationSceneId instance variable
        /// Note: This is for unit testing purposes - the instance variables of Monobehavior 
        /// scripts are usually set in a GUI window in the Unity editor
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be positive. Must exist as a key in SceneChangerModel's path collection
        /// Postconditions:
        /// - DoorModel's `destinationSceneId` instance variable set to input value
        /// </remarks>
        set
        {
            if (value < 0)
            {
                Debug.LogError("value passed to set destinationSceneId is negative");
            }
            Assert.IsTrue(value >= 0, "destinationSceneId must be positive");
            destinationSceneId = value;
        }
    }

    /// <summary>
    /// Used when a player exits the door associated with this DoorModel.
    /// They are teleported using this offset facing forwards
    /// </summary>
    private Vector3 teleportOffset;


    /// <summary>
    /// Retrieve the destinationDoor associated with this doors targetDoorId
    /// </summary>
    /// <returns>DoorModel associated with this door's targetId</returns>
    /// <remarks>
    /// Preconditions:
    /// - static lookup table `doorLookup` must contain door associated with target Id
    /// Postconditions:
    /// - target door is returned
    public IDoorModel GetTargetDoor()
    {
        if (!doorLookup.ContainsKey(targetDoorId))
        {
            Debug.LogError("Target door does not exist");
        }
        Assert.IsTrue(doorLookup.ContainsKey(targetDoorId));

        IDoorModel target = doorLookup[targetDoorId];

        return target;
    }

    /// <summary>
    /// Retrieves this door's teleport position in world space
    /// </summary>
    /// <returns>This door's teleport position in world space </returns>
    /// <remarks>
    /// Preconditions:
    ///  - none
    /// Postconditions:
    /// - This doors teleport position in world space in returned
    /// </remarks>
    public Vector3 GetTeleportPosition()
    {
        return transform.position + teleportOffset;
    }

    /// <summary>
    /// Retrieves this door's teleport rotation in world space
    /// </summary>
    /// <returns>The door's teleport rotation in worldspace</returns>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Door's teleport rotation in worldspace is returned
    /// </remarks>
    public Quaternion GetTeleportRotation()
    {
        return Quaternion.LookRotation(transform.forward, Vector3.up);
    }


    /// <summary>
    /// Initializes this doorModel. Called by the game within the MonoBehaviour
    /// function `Start()` (exectures the frame the script is enables) - Separated
    /// from `Start()` to make unit testing easier
    /// </summary>
    /// <remarks>
    /// Precondtions:
    /// - All instance variables of DoorModel must be set. Another doorModel with
    /// the same Id must not already exist
    /// Postconditions:
    /// - Static lookup table for all DoorModels allocated if doesn't already exits.
    /// This door is added to it.
    public void Init()
    {
        if (doorLookup == null)
        {
            doorLookup = new Dictionary<int, IDoorModel>();
            Debug.Log("doorLookup dictionary created");
        }
        Assert.IsFalse(doorLookup.ContainsKey(doorId), "A doorModel with this ID already exists");


        doorLookup[doorId] = this;
        Debug.Log("DoorModel Initialized");
    }

    /// <summary>
    /// Resets the static lookup table of DoorModels. Used for testing purposes
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - doorLookup is cleared.
    public void ResetDoorLookup()
    {
        doorLookup.Clear();
        Debug.Log("doorLookup dictionary cleared");
    }

    /// <summary>
    /// A `MonoBehaviour` function, called on the frame when a script is enabled, before
    /// any `Update()` functions are called. - Important to call on Start() instead of Awake(),
    /// as it depends on the existence of other elements.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - all that is required to run Init();
    /// PostConditions:
    /// - all side effects caused by calling Init();
    /// </remarks>
    private void Start()
    {
        Init();
    }
}