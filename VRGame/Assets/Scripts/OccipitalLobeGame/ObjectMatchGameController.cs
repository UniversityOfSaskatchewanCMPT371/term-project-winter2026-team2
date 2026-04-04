using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMatchGameController : Controller<IObjectMatchGameModel, IObjectMatchGameView>, IObjectMatchGameController
{
/*
    Internal references to the model and view layer. This lets us use mocks to substitute 
    for the model and view in tests, while still allowing us to assign them in the inspector
    for ease of use in the editor.
    // */
    // internal IObjectMatchGameView view;
    // internal IObjectMatchGameModel model;
    /// </inheritdoc>
    public void PotentialGuess(string GuessItem)
    {
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
        throw new System.NotImplementedException();
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
        bool success = this.modelInstance.SubmitGuess();
        if (success)
        {
            ExitLevel();
        }
    }

    /// <inheritdoc/>
    public void ExitLevel()
    {
        this.viewInstance.ExitLevel();
        this.viewInstance.ClearAllObjects();
    }
}
