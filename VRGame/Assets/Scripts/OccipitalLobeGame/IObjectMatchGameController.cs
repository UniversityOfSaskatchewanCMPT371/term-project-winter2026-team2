using ObjectMatchGame;

public interface IObjectMatchGameController
{
    /// <summary>
    /// Initializes a new level. Will call model to set up the level and the 
    /// view to display the level.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Level is a positive integer where Level <= IObjectMatchGameModel.GetTotalLevels()
    /// Postconditions:
    /// - The model and view are updated to reflect the new level
    /// </remarks>
    public void InitializeLevel(int Level);

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
    /// Calls the model to evaluate the guess to verify if it is correct
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Model is updated depending on correctness of the guess.
    /// </remarks>
    public void checkGuess();
}
