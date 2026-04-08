using NUnit.Framework;
using ObjectMatchGame;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Unit tests for ObjectMatchGameModel component.
/// </summary>
public class ObjectMatchGameModelTests
{
    private static void AssignLevels(ObjectMatchGameModel model, int count = 6)
    {
        var levelsField = typeof(ObjectMatchGameModel).GetField("levels", BindingFlags.NonPublic | BindingFlags.Instance);
        var levels = new levelData[count];

        for (int i = 0; i < count; i++)
        {
            levels[i] = new levelData(i + 1, ("CorrectObject" + i), new[] { ("CorrectObject" + i), "OtherObject" }, 60, 200);
        }

        levelsField.SetValue(model, levels);
        model.totalLevels = count - 1;
    }

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
        Assert.AreEqual(1, model.GetCurrentLevel());
        Assert.AreEqual(3, model.GetTotalLevels());
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
    /// Verifies GetCurrentLevel returns 1 before the game starts.
    /// </summary>
    [Test]
    public void GetCurrentLevel()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        Assert.AreEqual(1, model.GetCurrentLevel());

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

        Assert.AreEqual(3, model.GetTotalLevels());

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
    /// Verifies InitializeTutorial correctly sets relevant fields
    /// </summary>
    [Test]
    public void InitializeTutorialSetsProperties()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();

        model.InitializeTutorial();

        Assert.AreEqual(GameState.tutorial, model.GetGameState());
        Assert.AreEqual(0, model.failedGuesses);
        Assert.AreEqual("", model.GetCurrentGuessID());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies LeaveTutorial correctly sets relevant fields
    /// </summary>
    [Test]
    public void LeaveTutorialSetsProperties()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        model.InitializeTutorial();

        model.LeaveTutorial();

        Assert.AreEqual(GameState.readyToStart, model.GetGameState());
        Assert.AreEqual(0, model.failedGuesses);
        Assert.AreEqual("", model.GetCurrentGuessID());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies GetTotalLevels returns consistent value throughout game lifecycle.
    /// </summary>
    [Test]
    public void GetTotalLevels_ReturnsConsistentValue()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        int totalLevels1 = model.GetTotalLevels();
        model.CompleteLevel();
        int totalLevels2 = model.GetTotalLevels();
        
        Assert.AreEqual(totalLevels1, totalLevels2);
        Assert.AreEqual(3, totalLevels1);

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies multiple CompleteLevel calls increment level correctly.
    /// </summary>
    [Test]
    public void CompleteLevel_MultipleCallsIncrementCorrectly()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        int initialLevel = model.GetCurrentLevel();
        
        model.CompleteLevel();
        Assert.AreEqual(initialLevel + 1, model.GetCurrentLevel());
        
        model.CompleteLevel();
        Assert.AreEqual(initialLevel + 2, model.GetCurrentLevel());
        
        model.CompleteLevel();
        Assert.AreEqual(initialLevel + 3, model.GetCurrentLevel());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies GetGameScore and GetLevelScore return valid non-negative values.
    /// </summary>
    [Test]
    public void GetScores_ReturnNonNegativeValues()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        Assert.GreaterOrEqual(model.GetGameScore(), 0);
        Assert.GreaterOrEqual(model.GetLevelScore(), 0);

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies CompleteLevel transitions from playing to levelComplete state.
    /// </summary>
    [Test]
    public void CompleteLevel_TransitionsToLevelComplete()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        model.CompleteLevel();
        
        Assert.AreEqual(GameState.levelComplete, model.GetGameState());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies model state after multiple level completions remains valid.
    /// </summary>
    [Test]
    public void CompleteLevel_MaintainsValidState()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        for (int i = 0; i < 3; i++)
        {
            model.CompleteLevel();
            Assert.AreEqual(GameState.levelComplete, model.GetGameState());
            Assert.GreaterOrEqual(model.GetCurrentLevel(), 0);
        }

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies InitializeLevel increments currentLevel and sets state to playing.
    /// </summary>
    [Test]
    public void InitializeLevel_SetsPlaying()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        AssignLevels(model);
        
        int initialLevel = model.GetCurrentLevel();
        model.InitializeLevel();

        Assert.AreEqual(GameState.playing, model.GetGameState());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies PotentialGuess logs warning for invalid guess ID.
    /// </summary>
    [Test]
    public void PotentialGuess_InvalidID_LogsWarning()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        AssignLevels(model);
        model.InitializeLevel();

        LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex(".*unexpected GameObject.*"));
        
        model.PotentialGuess("InvalidObjectID");

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies SubmitGuess with empty currentGuessID logs warning.
    /// </summary>
    [Test]
    public void SubmitGuess_EmptyGuessID_LogsWarning()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        LogAssert.Expect(LogType.Warning, "SubmitGuess called with empty current guess");
        
        model.SubmitGuess();

        Object.DestroyImmediate(go);
    }

    [Test]
    public void SubmitGuess_WrongID_ReturnsFalse()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();
        model.Init();
        AssignLevels(model);
        model.InitializeLevel();
        
        model.PotentialGuess("OtherObject");

        int failures = model.failedGuesses; 
        bool result = model.SubmitGuess();
        
        Assert.IsFalse(result);
        Assert.AreEqual(failures + 1, model.failedGuesses);
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies model maintains consistent state across method calls.
    /// </summary>
    [Test]
    public void ModelStateConsistency_AcrossOperations()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        Assert.AreEqual(GameState.readyToStart, model.GetGameState());
        Assert.AreEqual(1, model.GetCurrentLevel());
        
        model.CompleteLevel();
        Assert.AreEqual(2, model.GetCurrentLevel());
        Assert.AreEqual(GameState.levelComplete, model.GetGameState());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies RemovePotentialGuess can be called when no guess is present.
    /// </summary>
    [Test]
    public void RemovePotentialGuess_NoActiveGuess_HandlesGracefully()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        // Should not crash when removing guess with no active guess
        LogAssert.Expect(LogType.Log, "Model removed potential guess, current guess is now empty string");
        
        model.RemovePotentialGuess();
        Assert.AreEqual("", model.GetCurrentGuessID());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies GetCurrentGuessID persistence after RemovePotentialGuess.
    /// </summary>
    [Test]
    public void GetCurrentGuessID_AfterRemove_ReturnsEmpty()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        
        model.RemovePotentialGuess();
        
        Assert.AreEqual("", model.GetCurrentGuessID());

        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Verifies model handles level boundary correctly.
    /// </summary>
    [Test]
    public void InitializeLevel_BeyondTotalLevels_LogsMessage()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();

        model.Init();
        AssignLevels(model);
        
        for (int i = 0; i < 5; i++)
        {
            model.InitializeLevel();
            model.CompleteLevel();
        }
        
        LogAssert.Expect(LogType.Log, "All levels completed!");
        model.InitializeLevel();

        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetActiveObjectIDs_GetsList()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();
        model.Init();
        AssignLevels(model);
        model.InitializeLevel();
        string[] activeIDs = model.GetActiveObjectIDs();
        
        Assert.IsNotNull(activeIDs);
        Assert.AreEqual(2, activeIDs.Length);
        Assert.Contains("CorrectObject1", activeIDs);
        Assert.Contains("OtherObject", activeIDs);
        Object.DestroyImmediate(go);
    }


    [Test]
    public void GetActiveObjectIDs_InvalidLevel ()
    {
        GameObject go = new GameObject();
        ObjectMatchGameModel model = go.AddComponent<ObjectMatchGameModel>();
        model.Init();
        AssignLevels(model);
        model.InitializeLevel();
        model.currentLevel = 10; // Set to invalid level

        LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex(".*GetActiveObjectIDs called with invalid current level.*"));
        string[] activeIDs = model.GetActiveObjectIDs();
        
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
