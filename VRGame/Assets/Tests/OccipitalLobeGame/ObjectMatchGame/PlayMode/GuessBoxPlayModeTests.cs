using NUnit.Framework;
using NSubstitute;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

public class GuessBoxPlayModeTests
{
    private GameObject _guessBoxGo;
    private GuessBox _guessBox;
    private GameObject _controllerGo;
    private IObjectMatchGameController _mockController;
    private GameObject _testObject;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        _controllerGo = new GameObject("ControllerParent");
        _mockController = Substitute.For<IObjectMatchGameController>();
        
        var controllerComponent = _controllerGo.AddComponent<MockControllerComponent>();
        controllerComponent.Controller = _mockController;

        _guessBoxGo = new GameObject("GuessBox");
        _guessBoxGo.transform.SetParent(_controllerGo.transform);
        _guessBox = _guessBoxGo.AddComponent<GuessBox>();
        
        BoxCollider collider = _guessBoxGo.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(1, 1, 1);

        yield return null; // Wait for Start()
    }

    [UnityTearDown]
    public IEnumerator Teardown()
    {
        if (_testObject != null) Object.Destroy(_testObject);
        Object.Destroy(_guessBoxGo);
        Object.Destroy(_controllerGo);
        yield return null;
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter_CallsControllerPotentialGuess()
    {
        _testObject = new GameObject("TestObject");
        _testObject.AddComponent<BoxCollider>();
        _testObject.AddComponent<Rigidbody>().useGravity = false;

        _mockController.GetCurrentGuessID().Returns("");

        // Act
        _testObject.transform.position = _guessBoxGo.transform.position;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // Assert
        _mockController.Received(1).PotentialGuess("TestObject");
    }

    [UnityTest]
    public IEnumerator OnTriggerEnter_MultipleObjects_HandlesSequentially()
    {
        GameObject obj1 = new GameObject("Object1");
        obj1.AddComponent<BoxCollider>();
        obj1.AddComponent<Rigidbody>().useGravity = false;
        
        GameObject obj2 = new GameObject("Object2");
        obj2.AddComponent<BoxCollider>();
        obj2.AddComponent<Rigidbody>().useGravity = false;

        // First Object
        _mockController.GetCurrentGuessID().Returns(""); 
        obj1.transform.position = _guessBoxGo.transform.position;
        yield return new WaitForFixedUpdate();

        // Second Object
        _mockController.GetCurrentGuessID().Returns("Object1");
        
        // UNCOMMENTED: This catches the log your script generates
        LogAssert.Expect(LogType.Log, new Regex(".*Cannot have two guess.*"));
        
        obj2.transform.position = _guessBoxGo.transform.position;
        yield return new WaitForFixedUpdate();

        // Assert
        _mockController.Received(1).PotentialGuess("Object1");
        _mockController.DidNotReceive().PotentialGuess("Object2");

        Object.Destroy(obj1);
        Object.Destroy(obj2);
    }

    [UnityTest]
    public IEnumerator OnTriggerExit_WithNonCurrentGuess_LogsWarning()
    {
        _testObject = new GameObject("TestObject");
        _testObject.AddComponent<BoxCollider>();
        _testObject.AddComponent<Rigidbody>().useGravity = false;

        // Position it inside first
        _testObject.transform.position = _guessBoxGo.transform.position;
        yield return new WaitForFixedUpdate();

        // Simulate a different object is actually the one registered
        _mockController.GetCurrentGuessID().Returns("DifferentObject");

        LogAssert.Expect(LogType.Warning, new Regex(".*not registered as the current guess.*"));
        
        // Move out
        _testObject.transform.position = _guessBoxGo.transform.position + Vector3.up * 5;
        yield return new WaitForFixedUpdate();

        _mockController.DidNotReceive().RemovePotentialGuess();
    }

    /// <summary>
    /// Verifies OnTriggerStay does not re-send the same guess.
    /// </summary>
    [UnityTest]
    public IEnumerator OnTriggerStay_WithMatchingGuess_DoesNotCallPotentialGuessAgain()
    {
        _testObject = new GameObject("TestObject");
        _testObject.AddComponent<BoxCollider>();
        _testObject.AddComponent<Rigidbody>().useGravity = false;

        _mockController.GetCurrentGuessID().Returns("TestObject");

        // Position it inside
        _testObject.transform.position = _guessBoxGo.transform.position;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        _mockController.DidNotReceive().PotentialGuess("TestObject");

        Object.Destroy(_testObject);
    }

    /// <summary>
    /// Verifies OnTriggerStay prevents multiple guesses at once.
    /// </summary>
    [UnityTest]
    public IEnumerator OnTriggerStay_PreventsDuplicateGuesses()
    {
        _testObject = new GameObject("TestObject");
        _testObject.AddComponent<BoxCollider>();
        _testObject.AddComponent<Rigidbody>().useGravity = false;

        // Simulate different object already being the guess
        _mockController.GetCurrentGuessID().Returns("DifferentObject");

        LogAssert.Expect(LogType.Log, new Regex(".*Cannot have two guess.*"));

        // Position it inside
        _testObject.transform.position = _guessBoxGo.transform.position;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // Verify PotentialGuess was NOT called for TestObject
        _mockController.DidNotReceive().PotentialGuess("TestObject");

        Object.Destroy(_testObject);
    }

    /// <summary>
    /// Verifies OnTriggerExit correctly removes matching guess.
    /// </summary>
    [UnityTest]
    public IEnumerator OnTriggerExit_WithMatchingGuess_CallsRemovePotentialGuess()
    {
        _testObject = new GameObject("TestObject");
        _testObject.AddComponent<BoxCollider>();
        _testObject.AddComponent<Rigidbody>().useGravity = false;

        // Position it inside first
        _testObject.transform.position = _guessBoxGo.transform.position;
        yield return new WaitForFixedUpdate();

        // Set controller to return this object as current guess
        _mockController.GetCurrentGuessID().Returns("TestObject");

        // Move out
        _testObject.transform.position = _guessBoxGo.transform.position + Vector3.up * 5;
        yield return new WaitForFixedUpdate();

        // Verify RemovePotentialGuess was called
        _mockController.Received(1).RemovePotentialGuess();

        Object.Destroy(_testObject);
    }

    /// <summary>
    /// Verifies GuessBox handles missing controller gracefully on Start.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_MissingController_LogsError()
    {
        // Create standalone GuessBox without controller parent
        GameObject standaloneGo = new GameObject("StandaloneGuessBox");
        standaloneGo.AddComponent<BoxCollider>().isTrigger = true;
        
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        
        standaloneGo.AddComponent<GuessBox>();
        
        yield return null;

        Object.Destroy(standaloneGo);
    }

    // Helper component to bridge the Interface to Unity's GetComponentInParent
    private class MockControllerComponent : MonoBehaviour, IObjectMatchGameController
    {
        public IObjectMatchGameController Controller;
        public void Init() => Controller?.Init();
        public void CheckModelRef() => Controller?.CheckModelRef();
        public void CheckViewRef() => Controller?.CheckViewRef();
        public void InitializeLevel() => Controller?.InitializeLevel();
        public void InitializeTutorial() => Controller?.InitializeTutorial();
        public void RestartGame() => Controller?.RestartGame();
        public void PotentialGuess(string GuessItem) => Controller?.PotentialGuess(GuessItem);
        public string GetCurrentGuessID() => Controller?.GetCurrentGuessID() ?? "";
        public void RemovePotentialGuess() => Controller?.RemovePotentialGuess();
        public void SubmitGuess() => Controller?.SubmitGuess();
    }
}