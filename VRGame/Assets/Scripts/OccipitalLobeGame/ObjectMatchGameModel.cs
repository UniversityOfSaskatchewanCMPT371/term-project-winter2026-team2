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

    public override void Init()
    {
        currentLevel = 0;
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
    }

    /// <inheritdoc/>
    public void InitializeLevel(int Level)
    {
    }

    /// <inheritdoc/>
    public void InitializeTutorial()
    {
    }

    /// <inheritdoc/>
    public void CheckForLevelCompletion()
    {
    }
}
