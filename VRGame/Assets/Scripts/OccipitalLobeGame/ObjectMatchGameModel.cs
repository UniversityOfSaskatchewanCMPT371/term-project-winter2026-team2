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

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    /// <inheritdoc/>
    public GameState GetGameState()
    {
        return GameState.readyToStart;
    }

    /// <inheritdoc/>
    public int GetCurrentLevel()
    {
        return -1;
    }

    /// <inheritdoc/>
    public int GetTotalLevels()
    {
        return -1;
    }

    /// <inheritdoc/>
    public int GetCurrentScore()
    {
        return -1;
    }

    /// <inheritdoc/>
    public int GetLevelScore()
    {
        return -1;
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

    void IModel.Init()
    {
        Init();
    }
}
