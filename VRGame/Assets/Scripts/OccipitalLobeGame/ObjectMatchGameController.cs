using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMatchGameController : Controller<IObjectMatchGameModel, IObjectMatchGameView>, IObjectMatchGameController
{
    public void checkGuess(string GuessItem)
    {
        this.modelInstance.CheckGuess(GuessItem);
    }

    public override void Init()
    {
        this.CheckModelRef();
        this.CheckViewRef();
    }

    public void InitializeLevel()
    {
        this.modelInstance.InitializeLevel();
        this.viewInstance.ShowObjects(this.modelInstance.GetActiveObjectIDs());
    }

    public void InitializeTutorial()
    {
        throw new System.NotImplementedException();
    }

    public void RestartGame()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
