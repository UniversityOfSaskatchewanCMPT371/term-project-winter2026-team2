using System.Diagnostics;
using System.Linq;
using ObjectMatchGame;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;

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
    // A stopwatch to time the level completion time
    private Stopwatch stopwatch;
    // The amount of points to be added per second remaining at end of level
    private int pointsForTimeLeft = 5; 


    /// <summary>
    /// Initialize the game model. Sets all variables to their starting values.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - currentLevel is set to 1
    /// - totalLevels is set to the total number of levels in the game (not counting the tutorial)
    /// - gameScore is set to 0
    /// - levelScore is set to 0
    /// - failedGuesses is set to 0
    /// - gameState is set to GameState.readyToStart
    /// - currentGuessID is set to an empty string
    /// - stopwatch is a new Stopwatch object
    /// </remarks>
    public override void Init()
    {
        levels = LevelData.levels;
        
        currentLevel = 1;
        totalLevels = levels.Length - 1;
        gameScore = 0;
        levelScore = 0;
        failedGuesses = 0;
        gameState = GameState.readyToStart;
        currentGuessID = "";
        stopwatch = new Stopwatch();
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
        stopwatch.Stop();
        double levelTime = stopwatch.Elapsed.TotalSeconds;
        CalculateScore();
        gameState = GameState.levelComplete;
        currentLevel++;
    }

    /// <inheritdoc/>
    public void InitializeLevel()
    {
        if (currentLevel > totalLevels)
        {
            UnityEngine.Debug.Log("All levels completed!"); // replace with screen in game
            return;
        }

        gameState = GameState.playing;
        currentGuessID = "";
        failedGuesses = 0;

        stopwatch.Restart();
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
        gameState = GameState.tutorial;
        currentGuessID = "";
        failedGuesses = 0;
    }

    /// <inheritdoc/>
    public void LeaveTutorial()
    {
        gameState = GameState.readyToStart;
        currentGuessID = "";
        failedGuesses = 0;
    }

    /// <inheritdoc/>
    public void PotentialGuess(string Guess)
    {
        if (!levels[currentLevel].AllObjectIDs.Contains(Guess))
        {
            UnityEngine.Debug.LogWarning("Model got unexpected GameObject named: \"" + Guess + "\" in PotentialGuess");
            return;
        }
        currentGuessID = Guess;

        UnityEngine.Debug.Log("Model received potential guess: " + Guess);
    }

    /// <inheritdoc/>
    public void RemovePotentialGuess()
    {
        currentGuessID = "";
        UnityEngine.Debug.Log("Model removed potential guess, current guess is now empty string");
    }

    /// <inheritdoc/>
    public bool SubmitGuess()
    {
        if (currentGuessID == "")
        {
            UnityEngine.Debug.LogWarning("SubmitGuess called with empty current guess");
            return false;
        }

        if (currentGuessID == levels[currentLevel].CorrectObjectID)
        {
            UnityEngine.Debug.Log("Correct guess!");
            CompleteLevel();
            return true;
        }
        else
        {
            UnityEngine.Debug.Log("Incorrect guess.");
            failedGuesses++;
            return false;
        }
    }

    /// <inheritdoc/>
    public void CalculateScore()
    {
        int score;
        int timeLeft = GetTimeRemaining();
        if (timeLeft > 0)
        {
            score = timeLeft * pointsForTimeLeft + levels[currentLevel].winPoints -
                failedGuesses * (levels[currentLevel].winPoints / 2);
        }
        else
        {
            score = levels[currentLevel].winPoints -
                failedGuesses * (levels[currentLevel].winPoints / 2);
        }

        if (score < 50)
        {
            score = 50;
        }
        levels[currentLevel].Score = score;
        levelScore = score;
        gameScore += score;
        UnityEngine.Debug.Log("level score: " + levelScore);
        UnityEngine.Debug.Log("game score: " + gameScore);
    }

    /// <inheritdoc/>
    public int GetTimeRemaining()
    {
   

        double levelTime = stopwatch.Elapsed.TotalSeconds;
        int timeLeft = (int)Math.Ceiling((double)levels[currentLevel].maxTime - levelTime);

        return timeLeft;
    }

    /// <inheritdoc/>
    public int[] GetLevelScores()
    {
        int[] scores = new int[totalLevels];
        for (int i = 0; i < totalLevels; i++)
            scores[i] = levels[i+1].Score;
        return scores;
    }

    /// <inheritdoc/>
    public string[] GetTutorialObjectIDs()
    {
        return levels[0].AllObjectIDs;
    }
}

