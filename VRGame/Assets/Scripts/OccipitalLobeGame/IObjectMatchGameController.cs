using ObjectMatchGame;

public interface IObjectMatchGameController: IController
{
    /// <summary>
    /// Initializes a new level. Will call model to set up the level and the 
    /// view to display the level.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The model and view are updated to reflect the new level
    /// </remarks>
    public void InitializeLevel();

    /// <summary>
    /// Initializes the tutorial system and prepares it for user interaction.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The tutorial system is initialized and ready for user interaction
    /// </remarks>
    public void InitializeTutorial();

    /// <summary>
    /// Restarts the game, setting it back to original state as if the application had
    /// just been launched.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The game is reset to its original state, with all progress cleared and ready
    ///   for a new game session to be started.
    /// </remarks>
    public void RestartGame();

    /// <summary>
    /// Notifies the model of the ID of object placed in guess box
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - GuessItem is a non-empty, non-null string corresponding to the name of one of the options
    ///   in the current level.
    /// Postconditions:
    /// - Model is updated to reflect the guess
    /// </remarks>
    public void PotentialGuess(string GuessItem);

    /// <summary>
    /// Gets the object ID corresponding to the current guess
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The string ID corresponding to the current guess
    /// </remarks>
    public string GetCurrentGuessID();

    /// <summary>
    /// Clears the current guess, allowing the player to make a new guess
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Calls the model to clear the current guess
    /// </remarks>
    public void RemovePotentialGuess();

    /// <summary>
    /// Submits the current guess to be evaluated by the model.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - the model has a current guess that is ready to be submitted
    /// Postconditions:
    /// - Calls submit guess in the model
    /// </remarks>
    public void SubmitGuess();
}
