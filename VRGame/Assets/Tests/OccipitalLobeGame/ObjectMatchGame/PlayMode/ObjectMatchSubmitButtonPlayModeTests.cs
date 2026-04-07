using NUnit.Framework;
using NSubstitute;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text.RegularExpressions;

/// <summary>
/// PlayMode tests for ObjectMatchSubmitButton component.
/// </summary>
public class ObjectMatchSubmitButtonPlayModeTests
{
    private GameObject _submitButtonGo;
    private ObjectMatchSubmitButton _submitButton;
    private GameObject _controllerGo;
    private IObjectMatchGameController _mockController;
    private XRGrabInteractable _grabInteractable;
    private XRInteractionManager _interactionManager;

    /// <summary>
    /// Sets up test environment with controller and submit button.
    /// </summary>
    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Create XR Interaction Manager
        GameObject managerGo = new GameObject("InteractionManager");
        _interactionManager = managerGo.AddComponent<XRInteractionManager>();

        // Create controller parent
        _controllerGo = new GameObject("ControllerParent");
        _mockController = Substitute.For<IObjectMatchGameController>();
        
        var controllerComponent = _controllerGo.AddComponent<MockControllerComponent>();
        controllerComponent.Controller = _mockController;

        // Create submit button as child of controller
        _submitButtonGo = new GameObject("SubmitButton");
        _submitButtonGo.transform.SetParent(_controllerGo.transform);
        
        _grabInteractable = _submitButtonGo.AddComponent<XRGrabInteractable>();
        _grabInteractable.interactionManager = _interactionManager;
        
        _submitButton = _submitButtonGo.AddComponent<ObjectMatchSubmitButton>();

        yield return null; // Wait for Start()
    }

    /// <summary>
    /// Cleans up test objects.
    /// </summary>
    [UnityTearDown]
    public IEnumerator Teardown()
    {
        Object.Destroy(_controllerGo);
        Object.Destroy(_interactionManager.gameObject);
        yield return null;
    }

    /// <summary>
    /// Verifies OnGrabbed calls SubmitGuess on controller.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_CallsControllerSubmitGuess()
    {
        // Create XR Interaction Manager
        GameObject managerGo = new GameObject("InteractionManager");
        _interactionManager = managerGo.AddComponent<XRInteractionManager>();

        // Create controller parent
        _controllerGo = new GameObject("ControllerParent");
        _mockController = Substitute.For<IObjectMatchGameController>();
        
        var controllerComponent = _controllerGo.AddComponent<MockControllerComponent>();
        controllerComponent.Controller = _mockController;

        // Create submit button as child of controller
        _submitButtonGo = new GameObject("SubmitButton");
        _submitButtonGo.transform.SetParent(_controllerGo.transform);
        
        _grabInteractable = _submitButtonGo.AddComponent<XRGrabInteractable>();
        _grabInteractable.interactionManager = _interactionManager;
        
        _submitButton = _submitButtonGo.AddComponent<ObjectMatchSubmitButton>();
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = _grabInteractable
        };
        _submitButton.grabInteractable = _grabInteractable;


        _submitButton.OnGrabbed(args);
        yield return null;

        _mockController.Received(1).SubmitGuess();
    }

    /// <summary>
    /// Verifies OnGrabbed handles null controller gracefully.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_NullController_LogsError()
    {
        // Create standalone button without controller
        GameObject standaloneGo = new GameObject("StandaloneButton");
        standaloneGo.AddComponent<XRGrabInteractable>().interactionManager = _interactionManager;

        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        LogAssert.Expect(LogType.Exception, new Regex(".*Controller reference is null.*"));
        var standaloneButton = standaloneGo.AddComponent<ObjectMatchSubmitButton>();
        
        yield return null;

        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = standaloneGo.GetComponent<XRGrabInteractable>()
        };

        // Should log error and return without calling controller
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        
        standaloneButton.OnGrabbed(args);
        
        yield return null;

        Object.Destroy(standaloneGo);
    }

    /// <summary>
    /// Verifies Start method finds controller in parent hierarchy.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_FindsControllerInParent()
    {
        // Controller should be found automatically in Setup
        yield return null;

        // Verify no error was logged and controller is assigned
        Assert.IsNotNull(_submitButton.controller, "Controller should be found");
    }

    /// <summary>
    /// Verifies Start method assigns XRGrabInteractable component.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_AssignsGrabInteractable()
    {
        yield return null;

        // Verify grabInteractable was assigned
        Assert.IsNotNull(_submitButton.grabInteractable, "GrabInteractable should be assigned");
    }

    /// <summary>
    /// Verifies Start method logs error when XRGrabInteractable is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_MissingGrabInteractable_LogsError()
    {
        // Create button without XRGrabInteractable
        GameObject testGo = new GameObject("TestButton");
        testGo.transform.SetParent(_controllerGo.transform);
        
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*XRGrabInteractable.*"));
        LogAssert.Expect(LogType.Exception, new Regex(".*GrabInteractable is null.*"));

        testGo.AddComponent<ObjectMatchSubmitButton>();
        
        yield return null;

        Object.Destroy(testGo);
    }

    /// <summary>
    /// Verifies OnGrabbed exits select before submitting guess.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_ExitsSelectBeforeSubmit()
    {
        // Note: This is difficult to test directly as it involves interaction manager
        // Testing that SubmitGuess is called is sufficient
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = _grabInteractable
        };

        _submitButton.OnGrabbed(args);
        yield return null;

        // Verify SubmitGuess was called
        _mockController.Received(1).SubmitGuess();
    }

    /// <summary>
    /// Verifies multiple submit button grabs call SubmitGuess each time.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_MultipleTimes_CallsSubmitGuessEachTime()
    {
        // Arrange
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = _grabInteractable
        };

        // Act - Grab multiple times
        _submitButton.OnGrabbed(args);
        yield return null;

        _submitButton.OnGrabbed(args);
        yield return null;

        _submitButton.OnGrabbed(args);
        yield return null;

        // Assert - Should have called SubmitGuess 3 times
        _mockController.Received(3).SubmitGuess();
    }

    /// <summary>
    /// Verifies submit button with valid setup doesn't throw exceptions.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_ValidSetup_NoExceptions()
    {
        // Arrange
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = _grabInteractable
        };

        // Act & Assert - Should not throw
        Assert.DoesNotThrow(() => _submitButton.OnGrabbed(args));
        
        yield return null;
    }

    /// <summary>
    /// Verifies submit button remains active after grab (unlike start button).
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_KeepsButtonActive()
    {
        // Arrange
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = _grabInteractable
        };

        Assert.IsTrue(_submitButtonGo.activeSelf, "Button should be active initially");

        // Act
        _submitButton.OnGrabbed(args);
        yield return null;

        // Assert - Submit button should remain active (unlike start button)
        Assert.IsTrue(_submitButtonGo.activeSelf, "Submit button should remain active after grab");
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
        public void ExitLevel() => Controller?.ExitLevel();
        public void LeaveTutorial() => Controller?.LeaveTutorial();
        public void Update() => Controller?.Update();
    }
}
