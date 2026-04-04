using NUnit.Framework;
using NSubstitute;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text.RegularExpressions;

/// <summary>
/// PlayMode tests for ObjectMatchStartButton component.
/// </summary>
public class ObjectMatchStartButtonPlayModeTests
{
    private GameObject _startButtonGo;
    private ObjectMatchStartButton _startButton;
    private GameObject _controllerGo;
    private IObjectMatchGameController _mockController;
    private XRGrabInteractable _grabInteractable;

    /// <summary>
    /// Sets up test environment with controller and start button.
    /// </summary>
    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Create controller parent
        _controllerGo = new GameObject("ControllerParent");
        _mockController = Substitute.For<IObjectMatchGameController>();
        
        var controllerComponent = _controllerGo.AddComponent<MockControllerComponent>();
        controllerComponent.Controller = _mockController;

        // Create start button as child of controller
        _startButtonGo = new GameObject("StartButton");
        _startButtonGo.transform.SetParent(_controllerGo.transform);
        
        _grabInteractable = _startButtonGo.AddComponent<XRGrabInteractable>();
        _startButton = _startButtonGo.AddComponent<ObjectMatchStartButton>();

        yield return null; // Wait for Start()
    }

    /// <summary>
    /// Cleans up test objects.
    /// </summary>
    [UnityTearDown]
    public IEnumerator Teardown()
    {
        Object.Destroy(_controllerGo);
        yield return null;
    }

    /// <summary>
    /// Verifies OnGrabbed calls InitializeLevel on controller.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_CallsControllerInitializeLevel()
    {
        // Arrange
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var mockInteractable = Substitute.For<IXRSelectInteractable>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = mockInteractable
        };

        // Act
        _startButton.OnGrabbed(args);
        yield return null;

        // Assert
        _mockController.Received(1).InitializeLevel();
    }

    /// <summary>
    /// Verifies OnGrabbed deactivates the start button GameObject.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_DeactivatesButton()
    {
        // Arrange
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var mockInteractable = Substitute.For<IXRSelectInteractable>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = mockInteractable
        };

        Assert.IsTrue(_startButtonGo.activeSelf, "Button should be active initially");

        // Act
        _startButton.OnGrabbed(args);
        yield return null;

        // Assert
        Assert.IsFalse(_startButtonGo.activeSelf, "Button should be deactivated after grab");
    }

    /// <summary>
    /// Verifies OnGrabbed handles null controller gracefully.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_NullController_LogsError()
    {
        // Create standalone button without controller
        GameObject standaloneGo = new GameObject("StandaloneButton");
        standaloneGo.AddComponent<XRGrabInteractable>();
        
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        LogAssert.Expect(LogType.Exception, new Regex(".*Controller reference is null.*"));
        var standaloneButton = standaloneGo.AddComponent<ObjectMatchStartButton>();
        
        yield return null;

        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var mockInteractable = Substitute.For<IXRSelectInteractable>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = mockInteractable
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

        // Verify by checking that OnGrabbed works without errors
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var mockInteractable = Substitute.For<IXRSelectInteractable>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = mockInteractable
        };

        Assert.DoesNotThrow(() => _startButton.OnGrabbed(args));
    }

    /// <summary>
    /// Verifies Start method assigns XRGrabInteractable component.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_AssignsGrabInteractable()
    {
        yield return null;

        // Verify that grab interactable exists on the GameObject
        var grabInteractable = _startButtonGo.GetComponent<XRGrabInteractable>();
        Assert.IsNotNull(grabInteractable, "GrabInteractable should be assigned to GameObject");
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

        testGo.AddComponent<ObjectMatchStartButton>();
        
        yield return null;

        Object.Destroy(testGo);
    }

    /// <summary>
    /// Verifies multiple grabs call InitializeLevel multiple times.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_MultipleTimes_CallsInitializeLevelEachTime()
    {
        // Arrange
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var mockInteractable = Substitute.For<IXRSelectInteractable>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = mockInteractable
        };

        // Act - Re-enable and grab multiple times
        _startButtonGo.SetActive(true);
        _startButton.OnGrabbed(args);
        yield return null;

        _startButtonGo.SetActive(true);
        _startButton.OnGrabbed(args);
        yield return null;

        _startButtonGo.SetActive(true);
        _startButton.OnGrabbed(args);
        yield return null;

        // Assert - Should have called InitializeLevel 3 times
        _mockController.Received(3).InitializeLevel();
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
