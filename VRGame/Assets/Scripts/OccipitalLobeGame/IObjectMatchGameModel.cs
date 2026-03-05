using ObjectMatchGame;
/// <summary>
/// Interface for the model of the object matching minigame for the occipital lobe
/// The model is responsible for keeping track of the state of the game
/// </summary>
public interface IObjectMatchGameModel: IModel
{

    /// <summary>
    /// Gets the current state of the game
    /// </summary>
    /// <remarks> 
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The current state of the game is returned as a GameState enum value
    /// </remarks>
    public GameState GetGameState();

    /// <summary>
    /// Gets the current level of the game. If the game is in the tutorial or 
    /// ready to start state, this will return 0. Otherwise, it will return 
    /// the current level number, starting at 1 for the first level. If the 
    /// player is in between levels, this will return the next level
    /// (number 1 higher than level just completed.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns the current level of the game as an integer. 0 if in tutorial or
    /// ready to start state, otherwise the current level number. If in between 
    /// levels, returns the next level number (1 higher than level just completed).
    /// </remarks>
    public int GetCurrentLevel();

    /// <summary>
    ///  Gets the total number of levels in the game. This is a fixed value that does not
    ///  change during gameplay.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns the total number of levels in the game as an integer
    /// </remarks>
    public int GetTotalLevels();

    /// <summary>
    /// Gets the current score for the active game session.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns the current score for the active game session as an integer.
    ///   Score is reset to 0 at the start of each new game session.
    /// </remarks>
    public int GetGameScore();

    /// <summary>
    /// Gets the score achieved for the current level. This is the score that
    /// will be added to the player's total score at the end of the level, and 
    /// is reset at the start of each new level. Returns 0 if no level is active
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns the score achieved for the current level as an integer. 
    ///   This will be 0 if the game is not currently in an active level 
    ///   (i.e. if in tutorial, ready to start, or between levels).
    /// </remarks>
    public int GetLevelScore();

    /// <summary>
    /// Updates game state to reflect completion of the current level
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Updates the GameState to levelComplete
    /// - Updates numberOfFailures to 0
    /// - increment level count
    /// - adds levelScore to gameScore, then clears levelScore and gameScore
    public void CompleteLevel();

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
    /// Checks if the guess made by the player is correct and updates
    /// the game state
    /// accordingly.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Guess is a non-empty string corresponding to the name of one of the options
    /// Postconditions:
    /// - Updates the GameState, numberOfFailures
    public void CheckGuess(string Guess);

    /// <summary>
    /// Returns a string array containing the IDs of the objects that are active
    /// in the current level.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns a string array containing the IDs of the objects that are active in
    ///   the current level
    /// </remarks>
    public string[] GetActiveObjectIDs();
}
