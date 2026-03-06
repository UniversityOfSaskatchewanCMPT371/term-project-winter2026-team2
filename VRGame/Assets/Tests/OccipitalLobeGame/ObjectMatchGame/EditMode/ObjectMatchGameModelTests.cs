using NUnit.Framework;
using ObjectMatchGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Unit tests for ObjectMatchGameModel component.
/// </summary>
public class ObjectMatchGameModelTests
{
    /// <summary>
    /// Verifies that ObjectMatchGameModel component can be instantiated and initialized.
    /// </summary>
    [Test]
    public void Instantiation()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        Assert.NotNull(model);

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies all properties have correct default values after initialization.
    /// </summary>
    [Test]
    public void InitializationDefaultValues()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        /// Initial values check 
        Assert.AreEqual(GameState.readyToStart, model.GetGameState());
        Assert.AreEqual(-1, model.GetCurrentLevel());
        Assert.AreEqual(5, model.GetTotalLevels());
        Assert.AreEqual(0, model.GetGameScore());
        Assert.AreEqual(0, model.GetLevelScore());
        Assert.AreEqual("", model.GetCurrentGuessID());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies GetGameState returns the correct initial state (readyToStart).
    /// </summary>
    [Test]
    public void GetGameState()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(GameState.readyToStart, model.GetGameState());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies GetCurrentLevel returns -1 before the game starts.
    /// </summary>
    [Test]
    public void GetCurrentLevel()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(-1, model.GetCurrentLevel());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies GetTotalLevels returns the expected total of 5 levels.
    /// </summary>
    [Test]
    public void GetTotalLevels()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(5, model.GetTotalLevels());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies GetGameScore returns 0 at game initialization.
    /// </summary>
    [Test]
    public void GetGameScore()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(0, model.GetGameScore());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies GetLevelScore returns 0 at game initialization.
    /// </summary>
    [Test]
    public void GetLevelScore()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(0, model.GetLevelScore());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies CompleteLevel increments the level and sets game state to levelComplete.
    /// </summary>
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

    /// <summary>
    /// Verifies GetCurrentGuessID returns empty string when no guess is active.
    /// </summary>
    [Test]
    public void GetCurrentGuessID_Initially()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual("", model.GetCurrentGuessID());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies RemovePotentialGuess clears the current guess and logs appropriate message.
    /// </summary>
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

    /// <summary>
    /// Verifies InitializeTutorial executes without errors (currently empty implementation).
    /// </summary>
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

    /// <summary>
    /// Placeholder Unity coroutine test for future async testing scenarios.
    /// </summary>
    [UnityTest]
    public IEnumerator ObjectMatchGameModelTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
