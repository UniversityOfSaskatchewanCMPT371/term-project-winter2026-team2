using System.Collections;
using System.Collections.Generic;
using ObjectMatchGame;
using UnityEngine;

public class ObjectMatchGameModel : Model, IObjectMatchGameModel
{
    private int currentLevel;
    private int totalLevels;
    private int gameScore;
    private int levelScore;
    private int failedGuesses;
    private GameState gameState;
    [SerializeField] private levelData[] levels;


    public override void Init()
    {
        currentLevel = -1;
        totalLevels = 5;
        gameScore = 0;
        levelScore = 0;
        failedGuesses = 0;
        gameState = GameState.readyToStart;

    }
    // Update is called once per frame
    void Update()
    {
        
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

    public string[] GetActiveObjectIDs()
    {
        return levels[currentLevel].AllObjectIDs;
    }

    /// <inheritdoc/>
    public void InitializeTutorial()
    {
    }

    /// <inheritdoc/>
    public void CheckGuess(string Guess)
    {
        if (Guess == levels[currentLevel].CorrectObjectID)
        {
            gameState = GameState.levelComplete;
            Debug.Log("Correct guess!");
        }
        else
        {
            failedGuesses++;
            Debug.Log("Incorrect guess. Total failed guesses: " + failedGuesses);
        }
    }
}
