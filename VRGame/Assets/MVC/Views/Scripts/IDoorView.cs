

using UnityEngine;
/// <summary>
/// View portion of the reusable door module. Collisions are handled here
/// </summary>
/// <remarks>
/// - doorController is always non-null upon calling Init()
/// </remarks>
public interface IDoorView
{
    /// <summary>
    ///  Public accessor of the controller portion of the door module.
    /// </summary>
    public IDoorController DoorController 
    {
        /// <summary>
        /// Access the DoorView's DoorController instance variable
        /// </summary>
        /// <remarks>
        /// Precondtions:
        /// - None
        /// Postconditions:
        /// - DoorView's doorController instance variable is returned
        get;


        /// <summary>
        /// Set the value of the DoorView's DoorController instance variable
        /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
        /// scripts are usually set in a GUI window within the Unity editor 
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be non-null
        /// Postconditions:
        /// - DoorView's `doorController` instance variable set to input value.
        set;
    }

    /// <summary>
    /// Initializes the DoorView. Called by the game within the
    /// MonoBehaviour function `Start()` (executes the frame a script is enabled)
    /// - Separated from `Start()`, as this makes unit testing easier.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `doorController` instance var must be non-null
    /// PostConditions:
    /// - Checked to make sure DoorView will be able to function properly, has necessary values set for instance vars
    /// </remarks>
    public void Init();


    /// <summary>
    /// Called when another object's collider enters this door's collider.
    /// Handles player collision with door.
    /// </summary>
    /// <param name="other">Collider than has interacted with this door's collider</param>
    /// <remarks>
    /// Preconditions:
    /// - `Collider other` must be non-null
    /// - `doorController` instance var must be non-null
    /// Postconditions:
    /// - changes to state created by calling `doorController.OnPlayerEnter()`
    public void OnTriggerEnter(Collider other);
}