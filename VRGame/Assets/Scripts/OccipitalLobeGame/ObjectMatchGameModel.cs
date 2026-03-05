using System.Collections;
using System.Collections.Generic;
using ObjectMatchGame;
using UnityEngine;

public class ObjectMatchGameModel : MonoBehaviour, IObjectMatchGameModel
{
    private int currentLevel;
    private int totalLevels;
    private int gameScore;
    private int levelScore;
    private int failedGuesses;
    private GameState gameState;

    /// <summary>
    /// Called before the first frame update. Initializes the game model with default values.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - currentLevel is set to 0
    /// - totalLevels is set to 5
    /// - gameScore is set to 0
    /// - levelScore is set to 0
    /// - failedGuesses is set to 0
    /// - gameState is set to GameState.readyToStart
    /// <remarks/>
    void Start()
    {
        currentLevel = 0;
        totalLevels = 5;
        gameScore = 0;
        levelScore = 0;
        failedGuesses = 0;
        gameState = GameState.readyToStart;
    }

    private void Init()
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
}
