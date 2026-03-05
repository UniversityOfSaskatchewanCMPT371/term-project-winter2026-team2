using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMatchGameController : Controller<IObjectMatchGameModel, IObjectMatchGameView>, IObjectMatchGameController
{
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
        this.modelInstance.InitializeLevel();
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

    /// <inheritdoc/>
    public void RemovePotentialGuess()
    {
        this.modelInstance.RemovePotentialGuess();
    }

    /// <inheritdoc/>
    public void SubmitGuess()
    {
        this.modelInstance.SubmitGuess();
    }
}
