using NUnit.Framework;
using ObjectMatchGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectMatchGameModelTests
{
    [Test]
    public void Instantiation()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        Assert.NotNull(model);

        Object.DestroyImmediate(go);
    }

    [Test]
    public void InitializationDefaultValues()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(GameState.readyToStart, model.GetGameState());
        Assert.AreEqual(-1, model.GetCurrentLevel());
        Assert.AreEqual(5, model.GetTotalLevels());
        Assert.AreEqual(0, model.GetGameScore());
        Assert.AreEqual(0, model.GetLevelScore());
        Assert.AreEqual("", model.GetCurrentGuessID());

        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetGameState()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(GameState.readyToStart, model.GetGameState());

        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetCurrentLevel()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(-1, model.GetCurrentLevel());

        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetTotalLevels()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(5, model.GetTotalLevels());

        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetGameScore()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(0, model.GetGameScore());

        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetLevelScore()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(0, model.GetLevelScore());

        Object.DestroyImmediate(go);
    }

    [Test]
    public void CompleteLevel()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        int initialLevel = model.GetCurrentLevel();
        model.CompleteLevel();

        Assert.AreEqual(GameState.levelComplete, model.GetGameState());
        Assert.AreEqual(initialLevel + 1, model.GetCurrentLevel());

        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetCurrentGuessID_Initially()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual("", model.GetCurrentGuessID());

        Object.DestroyImmediate(go);
    }

    [Test]
    public void RemovePotentialGuess()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        model.RemovePotentialGuess();

        Assert.AreEqual("", model.GetCurrentGuessID());
        LogAssert.Expect(LogType.Log, "Model removed potential guess, current guess is now empty string");

        Object.DestroyImmediate(go);
    }

    [Test]
    public void InitializeTutorial()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        // InitializeTutorial is currently empty but should not throw
        model.InitializeTutorial();

        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator ObjectMatchGameModelTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
