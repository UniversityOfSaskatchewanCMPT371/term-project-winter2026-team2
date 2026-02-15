

using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// View portion of the reusable door module. Collisions are handled here
/// </summary>
/// <var
/// <remarks>
/// - doorController is always non-null upon calling Init()
/// </remarks>
public class DoorView : MonoBehaviour, IDoorView
{
    
    /// Controller portion of door module. The view portion will
    /// call methods of this.
    private IDoorController doorController;

    /// <summary>
    ///  Public accessor of the controller portion of the door module.
    /// </summary>
    public IDoorController DoorController
    {
        /// <summary>
        /// Access the DoorView's DoorController instance variable
        /// </summary>
        get
        {
            return doorController;
        }
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
        set
        {
            Assert.IsNotNull(value, "doorController cannot be null.");
            doorController = value;
        }
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
    public void Init()
    {
        // sanity checks
        Assert.IsNotNull(doorController, "Field doorController cannot be null.");
    }

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
    /// 
    public void OnTriggerEnter(Collider other)
    {
        Assert.IsNotNull(other, "Collider other can not be null.");

        // Ignore interaction with any collider that is not the player's.
        if (!other.gameObject.CompareTag("MainCamera")) {
            return;
        }

        
        // ensure main camera has playerModel component
        IPlayerController player = other.GetComponentInParent<IPlayerController>();
        Assert.IsNotNull(player, "MainCamera collider must contain PlayerModel");

        // execute player enter functionality in controller portion.
        doorController.OnPlayerEnter(player);
    }

    /// <summary>
    /// A `MonoBehaviour` function, called on the frame when a script is enabled, before
    /// any `Update()` functions are called
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `doorController` instance var must be non-null
    /// PostConditions:
    /// - Checked to maked sure DoorView will be able to function properly, has necessary values set for instance vars
    /// </remarks>
    private void Start()
    {
        Init();
    }
}