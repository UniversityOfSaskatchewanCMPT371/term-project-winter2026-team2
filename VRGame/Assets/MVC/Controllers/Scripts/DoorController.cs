using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Runtime.CompilerServices;

// makes it so test scripts can access 
// this class's internal fields
[assembly: InternalsVisibleTo("Tests")]

/// <summary>
/// Controller Portion of the reusable door module. Interaction logic is handled here
/// </summary>
/// <remarks>
/// - doorModel, sceneChangerController must be set before calling Init();
/// </remarks>
public class DoorController : MonoBehaviour, IDoorController
{

    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for doorModel
    /// </summary>
    [SerializeField]
    private MonoBehaviour serializableDoorModel;

    /// <summary>
    /// Model portion of door module. Controller portion uses data from this
    /// </summary>
    private IDoorModel doorModel;


    /// <summary>
    /// Public accessor of model portion of door module
    /// </summary>
    internal IDoorModel DoorModel {

        /// <summary>
        /// Set the value of DoorController's DoorModel instance variable
        /// Note: this is for testing purposes - instance variables of MonoBehavior 
        /// scripts are usually set in a GUI window within the Unity editor
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be non-null
        /// Postconditions:
        /// - DoorController's `doorModel` instance variable set to input value
        set
        {
            if (value == null)
            {
                Debug.LogError("value passed to set DoorModel is null");
                Assert.IsNotNull(value, "Door model must not be null");
            }
            doorModel = value;
        }
    }

    /// <summary>
    /// This field exists because interfaces cannot
    /// be serialized in unity, meaning otherwise this
    /// value could not be set in the inspector window
    /// - A wrapper for sceneChangerController
    /// </summary>
    [SerializeField]
    private MonoBehaviour serializableSceneChangerController;

    /// <summary>
    /// Reference to singleton SceneChangerController, handles scene changes
    /// </summary>
    private ISceneChangerController sceneChangerController;

    /// <summary>
    /// Public accessor for singleton SceneChangerController, handles scene changes
    /// </summary>
    internal ISceneChangerController SceneChangerController
    {

        /// <summary>
        /// Set the value for DoorControllers reference to SceneChangerController
        /// Note: This is for unit testing purposes - the instance variables of MonoBehaviour
        /// scripts are usually set in a GUI window within the Unity editor 
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - `value` must be non-null
        /// Postconditiosn:
        /// - DoorController's `sceneChangerController` instance var set to input value
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.LogError("value passed to setSceneChangerController is null");
                Assert.IsNotNull(value);
            }
            sceneChangerController = value;
        }
    }

    /// <summary>
    /// Guard variable, stops player from entering door more than once
    /// </summary>
    private static bool triggerDebounce = false;

    /// <inheritdoc/>
    public bool TriggerDebounce
    {
        /// <summary>
        /// View current status of triggerDebounce
        /// </summary> 
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - triggerDebounce is returned 
        get
        {
            return triggerDebounce;
        }
    }


    /// <inheritdoc/>
    public void Init()
    {
        // If field was set in inspector window, set the internal values to that
        if (serializableDoorModel != null)
        {
            doorModel = (IDoorModel) serializableDoorModel;
        }
        if (serializableSceneChangerController != null)
        {
            sceneChangerController = (ISceneChangerController) serializableSceneChangerController;
        }

        // error checking
        if (doorModel == null)
        {
            Debug.LogError("doorModel is null");
        }
        Assert.IsNotNull(doorModel, "DoorModel field cannot be null.");

        if (sceneChangerController == null)
        {
            Debug.LogError("sceneChangerController is null");
        }
        Assert.IsNotNull(sceneChangerController, "SceneChangerController field cannot be null.");

        Debug.Log("DoorController initialized");
    }

    /// <inheritdoc/>
    public void OnPlayerEnter(IPlayerController playerController)
    {
        if (playerController == null)
        {
            Debug.LogError("playerController passed to OnPlayerEnter is null");
        }
        Assert.IsNotNull(playerController, "Player controller must be non-null.");

        // makes it so the player can only enter the door once
        if (triggerDebounce) return;
        triggerDebounce = true;


        IDoorModel targetDoor;
        Vector3 teleportPosition = new Vector3(0, 0, 0);
        Quaternion teleportRotation = new Quaternion();

        // load this door's destination scene
        int sceneId = doorModel.DestinationSceneId;

        // ensure destination scene actually exists
        if (!Enum.IsDefined(typeof(SceneEnum), sceneId))
        {
            Debug.LogError("Invalid destination scene id. Not in enum");
        }
        Assert.IsTrue(Enum.IsDefined(typeof(SceneEnum), sceneId));

        // load new scene with scene changer
        IAsyncOperationWrapper loadingScene = sceneChangerController.LoadScene(sceneId);
            
        loadingScene.Completed += (o) =>
        {
            targetDoor = doorModel.GetTargetDoor();
            teleportPosition = targetDoor.GetTeleportPosition();
            teleportRotation = targetDoor.GetTeleportRotation();

            playerController.teleportPlayerTo(teleportPosition, teleportRotation);
            triggerDebounce = false;
        };


    }
}