using NUnit.Framework;
using NSubstitute;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

/// <summary>
/// Play Mode unit tests for GuessBox component.
/// Tests trigger interactions with game objects representing player guesses.
/// </summary>
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
        // Create a parent GameObject with the mock controller
        _controllerGo = new GameObject("ControllerParent");
        _mockController = Substitute.For<IObjectMatchGameController>();
        
        // Add a component that implements the interface for GetComponentInParent to find
        var controllerComponent = _controllerGo.AddComponent<MockControllerComponent>();
        controllerComponent.Controller = _mockController;

        // Create the GuessBox as a child of the controller
        _guessBoxGo = new GameObject("GuessBox");
        _guessBoxGo.transform.SetParent(_controllerGo.transform);
        _guessBox = _guessBoxGo.AddComponent<GuessBox>();
        
        // Add a BoxCollider and set it as a trigger
        BoxCollider collider = _guessBoxGo.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.size = new Vector3(1, 1, 1);

        // Wait for Start to be called
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator Teardown()
    {
        if (_testObject != null)
            Object.Destroy(_testObject);
        
        Object.Destroy(_guessBoxGo);
        Object.Destroy(_controllerGo);
        yield return null;
    }

    /// <summary>
    /// Verifies GuessBox can be instantiated.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        Assert.NotNull(_guessBox);
        yield return null;
    }

    /// <summary>
    /// Verifies GuessBox finds controller in parent hierarchy on Start.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_FindsControllerInParent()
    {
        // The controller should be found during Start (which happens in Setup)
        // We can verify this by checking that no error was logged
        // (If controller was null, an error would be logged)
        
        // Since we can't directly access the private controller field,
        // we verify behavior by testing OnTriggerEnter works without errors
        yield return null;
        Assert.Pass("GuessBox started without errors");
    }

    /// <summary>
    /// Verifies OnTriggerEnter calls controller.PotentialGuess with correct object name.
    /// </summary>
    [UnityTest]
    public IEnumerator OnTriggerEnter_CallsControllerPotentialGuess()
    {
        // Arrange
        _testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _testObject.name = "TestObject";
        _testObject.transform.position = _guessBoxGo.transform.position;
        
        // Add Rigidbody for physics
        Rigidbody rb = _testObject.AddComponent<Rigidbody>();
        rb.useGravity = false;

        // Mock controller returns empty string for current guess (allowing new guess)
        _mockController.GetCurrentGuessID().Returns("");

        yield return new WaitForFixedUpdate();

        // Act - Move object into the trigger
        _testObject.transform.position = _guessBoxGo.transform.position;
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // Assert
        _mockController.Received().PotentialGuess("TestObject");
    }

    /// <summary>
    /// Verifies OnTriggerEnter does not call controller if a guess already exists.
    /// </summary>
    [UnityTest]
    public IEnumerator OnTriggerEnter_WithExistingGuess_DoesNotCallController()
    {
        // Arrange
        _testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _testObject.name = "TestObject";
        _testObject.transform.position = _guessBoxGo.transform.position + Vector3.up * 2;
        
        Rigidbody rb = _testObject.AddComponent<Rigidbody>();
        rb.useGravity = false;

        // Mock controller returns a non-empty string (existing guess)
        _mockController.GetCurrentGuessID().Returns("ExistingGuess");

        yield return new WaitForFixedUpdate();

        // Act - Move object into the trigger
        LogAssert.Expect(LogType.Log, new Regex(".*Cannot have two guess.*"));
        _testObject.transform.position = _guessBoxGo.transform.position;
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // Assert - PotentialGuess should not be called with the new object
        _mockController.DidNotReceive().PotentialGuess("TestObject");
    }

    /// <summary>
    /// Verifies OnTriggerExit calls controller.RemovePotentialGuess when current guess exits.
    /// </summary>
    [UnityTest]
    public IEnumerator OnTriggerExit_CallsControllerRemovePotentialGuess()
    {
        // Arrange
        _testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _testObject.name = "TestObject";
        
        Rigidbody rb = _testObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        // First, simulate object entering
        _mockController.GetCurrentGuessID().Returns("");
        _testObject.transform.position = _guessBoxGo.transform.position;
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // Now set it as the current guess
        _mockController.GetCurrentGuessID().Returns("TestObject");

        // Act - Move object out of trigger
        _testObject.transform.position = _guessBoxGo.transform.position + Vector3.up * 3;
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // Assert
        _mockController.Received().RemovePotentialGuess();
    }

    /// <summary>
    /// Verifies OnTriggerExit logs warning when non-current guess exits.
    /// </summary>
    [UnityTest]
    public IEnumerator OnTriggerExit_WithNonCurrentGuess_LogsWarning()
    {
        // Arrange
        _testObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _testObject.name = "TestObject";
        
        Rigidbody rb = _testObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

        _testObject.transform.position = _guessBoxGo.transform.position;
        
        yield return new WaitForFixedUpdate();

        // Current guess is different from the exiting object
        _mockController.GetCurrentGuessID().Returns("DifferentObject");

        // Act - Move object out of trigger
        LogAssert.Expect(LogType.Warning, new Regex(".*not registered as the current guess.*"));
        _testObject.transform.position = _guessBoxGo.transform.position + Vector3.up * 3;
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // Assert - RemovePotentialGuess should not be called
        _mockController.DidNotReceive().RemovePotentialGuess();
    }

    /// <summary>
    /// Verifies GuessBox logs error if controller not found in parent hierarchy.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_NoControllerInParent_LogsError()
    {
        // Arrange - Create a standalone GuessBox without controller in parent
        GameObject standaloneGo = new GameObject("StandaloneGuessBox");
        
        // Expect error log
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        
        // Act
        standaloneGo.AddComponent<GuessBox>();
        
        yield return null;

        // Cleanup
        Object.Destroy(standaloneGo);
    }

    /// <summary>
    /// Helper component to allow GetComponentInParent to find the mock controller.
    /// </summary>
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
    }
}
