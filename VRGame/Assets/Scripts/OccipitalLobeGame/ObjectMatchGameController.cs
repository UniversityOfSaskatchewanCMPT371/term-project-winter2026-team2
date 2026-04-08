using System.Collections;
using System.Collections.Generic;
using ObjectMatchGame;
using UnityEngine;

public class ObjectMatchGameController : Controller<IObjectMatchGameModel, IObjectMatchGameView>, IObjectMatchGameController
{
    /// </inheritdoc>
    public void PotentialGuess(string GuessItem)
    {
        if (GuessItem == null || GuessItem.Length == 0)
        {
            Debug.LogError("ObjectMatchGameController was given an empty or null guess item. No potential guess will be registered.");
            return;
        }
        this.modelInstance.PotentialGuess(GuessItem);
    }

    /// </inheritdoc>
    public override void Init()
    {
        this.CheckModelRef();
        this.CheckViewRef();
    }

    /// </inheritdoc>
    public void InitializeLevel()
    {
        Debug.Log("Initializing level " + this.modelInstance.GetCurrentLevel());
        this.modelInstance.InitializeLevel();
        this.viewInstance.EnterLevel();
        this.viewInstance.ShowObjects(this.modelInstance.GetActiveObjectIDs());
    }

    /// </inheritdoc>
    public void InitializeTutorial()
    {
        this.modelInstance.InitializeTutorial();
        this.viewInstance.EnterTutorial();
        this.viewInstance.ShowObjects(this.modelInstance.GetTutorialObjectIDs());
    }

    /// </inheritdoc>
    public void RestartGame()
    {
        throw new System.NotImplementedException();
    }

    /// </inheritdoc>
    public string GetCurrentGuessID()
    {
        return this.modelInstance.GetCurrentGuessID();
    }

    /// </inheritdoc>
    public void RemovePotentialGuess()
    {
        this.modelInstance.RemovePotentialGuess();
    }

    /// <inheritdoc/>
    public void SubmitGuess()
    {
        if (this.modelInstance.GetCurrentGuessID() == null || this.modelInstance.GetCurrentGuessID().Length == 0)
        {
            Debug.LogError("ObjectMatchGameController was asked to submit a guess but the current guess ID is empty or null. No guess will be submitted.");
            return;
        }
        bool success = this.modelInstance.SubmitGuess();
        if (success)
        {
            ExitLevel();
        }
    }

    /// <inheritdoc/>
    public void ExitLevel()
    {
        this.viewInstance.UpdateScore(
            this.modelInstance.GetGameScore(),
            this.modelInstance.GetLevelScores()
        );
        this.viewInstance.ExitLevel();
        this.viewInstance.ClearAllObjects();
    }

    /// <inheritdoc/>
    public void Update()
    {
        if (modelInstance == null) return;
        if (modelInstance.GetGameState() != GameState.playing) return;
        viewInstance.UpdateTimer(modelInstance.GetTimeRemaining());
    }

    /// <inheritdoc/>
    public void LeaveTutorial()
    {
        this.modelInstance.LeaveTutorial();
        this.viewInstance.ExitTutorial();
        this.viewInstance.ClearAllObjects();
    }
}
