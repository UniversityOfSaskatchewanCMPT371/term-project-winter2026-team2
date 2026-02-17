

using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// View portion of the reusable door module. Collisions are handled here
/// </summary>
/// <remarks>
/// - doorController is always non-null upon calling Init()
/// </remarks>
public class DoorView : MonoBehaviour, IDoorView
{

    /// <summary>
    /// This field exists because Unity can't serialize interfaces. A wrapper
    /// for doorController
    /// </summary>
    [SerializeField]
    private MonoBehaviour serializableDoorController;

    /// Controller portion of door module. The view portion will
    /// call methods of this.
    private IDoorController doorController;

    /// <summary>
    ///  Public accessor of the controller portion of the door module.
    /// </summary>
    public IDoorController DoorController
    {
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
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.LogError("value passed to setDoorController is null");
            }
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
        // If values set through inspector window, set the inner value to those
        if (serializableDoorController != null)
        {
            doorController = (IDoorController) serializableDoorController;
        }

        // sanity checks
        if (doorController == null)
        {
            Debug.LogError("doorController field in DoorView is null");
        }
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
    /// - changes to state created by calling `doorController.OnPlayerEnter()`
    public void OnTriggerEnter(Collider other)
    {
        if (other == null)
        {
            Debug.LogError("Collider other is null");
        }
        Assert.IsNotNull(other, "Collider other can not be null.");

        IColliderWrapper colliderWrapper = new ColliderWrapper(other);

        if (!colliderWrapper.CompareGameObjectTag("MainCamera")) 
        {
            Debug.Log("Component other than player collided with door");
            return;
        }

        
        // ensure main camera has playerModel component
        IPlayerController player = colliderWrapper.GetPlayerFromParent();
        if (player == null)
        {
            Debug.LogError("Collider does not contain playerController component");
        }
        Assert.IsNotNull(player, "MainCamera collider must contain PlayerModel");

        // execute player enter functionality in controller portion.
        doorController.OnPlayerEnter(player);
    }

    /// <summary>
    /// A `MonoBehaviour` function, called on the frame when a script is enabled, before
    /// any `Update()` functions are called. - Important to call on Start() instead of Awake(),
    /// as it depends on the existence of other elements.
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