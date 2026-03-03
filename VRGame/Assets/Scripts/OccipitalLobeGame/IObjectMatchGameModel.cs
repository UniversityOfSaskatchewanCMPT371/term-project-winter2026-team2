/// <summary>
/// Interface for the model of the object matching minigame for the occipital lobe
/// The model is responsible for keeping track of the state of the game
/// </summary>
public interface IObjectMatchGameModel
{

    enum GameState
    {
        playing,
        levelComplete,
        levelFailed,
        readyToStart,
        tutorial
    }

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
    /// Sets the current state of the game to the input value
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `value` must be a valid GameState enum value
    /// Postconditions:
    /// - The current state of the game is set to the input value
    /// </remarks>
    public void SetGameState(GameState newState);

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
    /// Gets the current score for the active game session.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Returns the current score for the active game session as an integer.
    ///   Score is reset to 0 at the start of each new game session.
    /// </remarks>
    public int GetCurrentScore();

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
    
}
