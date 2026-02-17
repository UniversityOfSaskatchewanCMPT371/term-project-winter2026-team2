
using UnityEditor.Build.Reporting;
using UnityEngine;
/// <summary>
/// Model portion of the reusable door module. Data is stored here
/// </summary>
/// <remarks>
/// - doorId and targetDoorId, destinationSceneId must be set before calling Init(), targetDoorId must exist.
/// targetSceneId must exist in SceneChangerModel service's path collection
/// </remarks>
public interface IDoorModel {

    /// <summary>
    /// Public accessor for door's id
    /// </summary>
    int DoorId
    {
        /// <summary>
        /// Access the DoorModel's Id
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - DoorModel's id is returned
        get; 
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
        set;
    }

    /// <summary>
    /// Public accessor for this door's target id 
    /// </summary>
    int TargetDoorId
    {
        /// <summary>
        /// Access this DoorModel's target door Id
        /// </summary> 
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - DoorModel's `targetDoorId` instance variable is returned
        /// </remarks>
        get; 
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
        set;
    }

    /// <summary>
    /// Public accessor for DoorModel's destinationSceneId instance variable
    /// </summary>
    int DestinationSceneId
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
        get; 
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
        set;
    }

    /// <summary>
    /// Public Accessor for teleport offset field
    /// </summary>
    public Vector3 TeleportOffset
    {
        /// <summary>
        /// Retrieve doorModel's teleportOffset
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - DoorModel's teleport offset is returned
        /// </remarks>
        get;

        /// <summary>
        /// Set doorModel's teleportOffset
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be valid Vector3
        /// Postconditions:
        /// - DoorModel's teleportOffset is set to value
        /// </remarks>
        set;

    }

    /// <summary>
    /// Retrieve the destinationDoor associated with this doors targetDoorId
    /// </summary>
    /// <returns>DoorModel associated with this door's targetId</returns>
    /// <remarks>
    /// Preconditions:
    /// - static lookup table `doorLookup` must contain door associated with target Id
    /// Postconditions:
    /// - target door is returned
    IDoorModel GetTargetDoor();


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
    Vector3 GetTeleportPosition();

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
    Quaternion GetTeleportRotation();

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
    public void Init();

    /// <summary>
    /// Resets the static lookup table of DoorModels. Used for testing purposes
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - doorLookup is cleared.
    public void ResetDoorLookup();
}