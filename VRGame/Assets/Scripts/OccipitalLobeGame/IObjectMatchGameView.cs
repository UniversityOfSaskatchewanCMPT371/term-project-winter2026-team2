using ObjectMatchGame;

public interface IObjectMatchGameView: IView
{
    /// <summary>
    /// Initializes the view by deactivating all objects in the game and checking for a 
    /// reference to the controller. Called when the object is instantiated.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Has a valid reference to the controller
    /// Postconditions:
    /// - All options for game levels are hidden
    /// </remarks>
    new public void Init();

    /// <summary>
    /// Takes a list of object IDs corresponding to the objects that are options in
    /// the current level and displays those objects
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - ObjectIDs is a non-empty string array containing the IDs of GameObjects
    /// - ObjectIDs only contains IDs of GameObjects that are in the game scene and can be displayed
    /// - ObjectIDs in not null
    /// Postconditions:
    /// - The objects corresponding to the IDs in ObjectIDs are displayed in the scene
    /// </remarks>
    public void ShowObjects(string[] ObjectIDs);

    /// <summary>
    /// Clears all objects from the scene, making them invisible and non-interactable.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - All objects in the scene are invisible and non-interactable
    /// </remarks>
    public void ClearAllObjects();

    /// <summary>
    /// Prepares the game for displaying a level by enabling the guess box and submit button and
    /// disabling the start level and tutorial buttons.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The guess box and submit button are enabled and the start level and tutorial buttons are disabled
    /// </remarks>
    public void EnterLevel();

    /// <summary>
    /// Prepares the game for exiting a level by disabling the guess box and submit button and enabling
    /// the start level and tutorial buttons
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The guess box and submit button are disabled and the start level and tutorial buttons are enabled
    /// </remarks>
    public void ExitLevel();

    /// <summary>
    /// Updates the timer text to reflect actual timer in the model
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The text on the canvas is updated to match the time remaining
    /// </remarks>
    public void UpdateTimer(int seconds);

    /// <summary>
    /// Updates the canvas to show the correct total and level scores
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - levelScores is not null
    /// Postconditions:
    /// - The canvas is updated to reflect the total and level scores
    /// </remarks>
    public void UpdateScore(int totalScore, int[] levelScores);

    public void EnterTutorial();
    public void ExitTutorial();
}
