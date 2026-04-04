using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectMatchGame;
using UnityEngine;

public class ObjectMatchGameModel : Model, IObjectMatchGameModel
{
    // Holds the level the user is currently on
    internal int currentLevel;
    // Total number of levels, not counting the tutorial
    private int totalLevels;
    // The total score across all levels
    private int gameScore;
    // The score for the current level, which is added to the game score when the level is completed
    private int levelScore;
    // The number of failed guesses for the current level, which is used to calculate the level score when the level is completed
    private int failedGuesses;
    // The current state of the game, which is used to control the flow of the game and determine what actions are allowed at any given time
    private GameState gameState;
    // The ID of the current guess, which is set when the user makes a potential guess and cleared when the user removes their potential guess
    private string currentGuessID = "";
    // The data for each level, which includes the IDs of all objects in the level and the ID of the correct object that the user is trying to guess
    [SerializeField] internal levelData[] levels;


    /// <summary>
    /// Initialize the game model. Sets all variables to their starting values.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - currentLevel is set to -1
    /// - totalLevels is set to the total number of levels in the game (not counting the tutorial)
    /// - gameScore is set to 0
    /// - levelScore is set to 0
    /// - failedGuesses is set to 0
    /// - gameState is set to GameState.readyToStart
    /// - currentGuessID is set to an empty string
    /// </remarks>
    public override void Init()
    {
        currentLevel = -1;
        totalLevels = 5;
        gameScore = 0;
        levelScore = 0;
        failedGuesses = 0;
        gameState = GameState.readyToStart;
        currentGuessID = "";
    }

    /// <inheritdoc/>
    public GameState GetGameState()
    {
        return gameState;
    }

    /// <inheritdoc/>
    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    /// <inheritdoc/>
    public int GetTotalLevels()
    {
        return totalLevels;
    }

    /// <inheritdoc/>
    public int GetGameScore()
    {
        return gameScore;
    }

    /// <inheritdoc/>
    public int GetLevelScore()
    {
        return levelScore;
    }

    /// <inheritdoc/>
    public void CompleteLevel()
    {
        gameState = GameState.levelComplete;
        currentLevel++;
    }

    /// <inheritdoc/>
    public void InitializeLevel()
    {
        if (currentLevel >= totalLevels)
        {
            Debug.Log("All levels completed!");
            return;
        }
        currentLevel++;
        failedGuesses = 0;

        levelData currentLevelData = levels[currentLevel];

        gameState = GameState.playing;
    }

    /// <inheritdoc/>
    public string[] GetActiveObjectIDs()
    {
        return levels[currentLevel].AllObjectIDs;
    }

    /// <inheritdoc/>
    public string GetCurrentGuessID()
    {
        return currentGuessID;
    }

    /// <inheritdoc/>
    public void InitializeTutorial()
    {
    }

    /// <inheritdoc/>
    public void PotentialGuess(string Guess)
    {
        if (!levels[currentLevel].AllObjectIDs.Contains(Guess))
        {
            Debug.LogWarning("Model got unexpected GameObject named: \"" + Guess + "\" in PotentialGuess");
            return;
        }
        currentGuessID = Guess;

        Debug.Log("Model received potential guess: " + Guess);
    }

    /// <inheritdoc/>
    public void RemovePotentialGuess()
    {
        currentGuessID = "";
        Debug.Log("Model removed potential guess, current guess is now empty string");
    }

    /// <inheritdoc/>
    public void SubmitGuess()
    {
        if (currentGuessID == "")
        {
            Debug.LogWarning("SubmitGuess called with empty current guess");
            return;
        }

        if (currentGuessID == levels[currentLevel].CorrectObjectID)
        {
            Debug.Log("Correct guess!");
            CompleteLevel();
        }
        else
        {
            Debug.Log("Incorrect guess.");
            failedGuesses++;
        }
    }
}
