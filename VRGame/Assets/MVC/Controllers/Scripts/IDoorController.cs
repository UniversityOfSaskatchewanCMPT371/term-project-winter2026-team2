
using UnityEngine;
/// <summary>
/// Controller Portion of the reusable door module. Interaction logic is handled here
/// </summary>
/// <remarks>
/// - doorModel, sceneChangerController must be set before calling Init();
/// </remarks>
public interface IDoorController
{
    /// <summary>
    /// Public accessor of model portion of door module
    /// </summary>
    public IDoorModel DoorModel 
    {
        /// <summary>
        /// Access DoorController's DoorModel instance
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - DoorController's doorModel reference is returned
        /// </remarks>
        get;

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

        set;
    }

    /// <summary>
    /// Public accessor for singleton SceneChangerController, handles scene changes
    /// </summary>
    public ISceneChangerController SceneChangerController
    {
        /// <summary>
        /// Access the sceneChangerController
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - sceneChangerController is returned
        /// </remarks>
        get;

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
        set;
    }

    /// <summary>
    /// Handles logic for player entering a door
    /// </summary>
    /// <param name="playerController"> Controller portion of player module </param>
    /// <remarks>
    /// Preconditions:
    /// - playerController must be non-null
    /// PostConditions:
    /// - Changes to state made in argument, `teleportPlayerTo` called on playerController,
    /// moving them to position of `doorModel`s target door
    /// </remarks>
    void OnPlayerEnter(IPlayerController player);

    /// <summary>
    /// Initializes the DoorController. Called by the game within the
    /// MonoBehaviour function `Start()` (executes the frame a script is enabled)
    /// - Separated from `Start()`, as this makes unit testing easier.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `doorModel` and `sceneChangerController` instance vars must be non-null
    /// PostConditions:
    /// - Checked to make sure DoorController will be able to function properly, has values set for instance variables
    /// </remarks>
    public void Init();
}