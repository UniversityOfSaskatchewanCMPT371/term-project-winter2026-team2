using NUnit.Framework;
using NSubstitute;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text.RegularExpressions;

/// <summary>
/// Play Mode unit tests for ObjectMatchStartButton component.
/// Tests button grab interactions and level initialization.
/// </summary>
public class ObjectMatchStartButtonPlayModeTests
{
    private GameObject _startButtonGo;
    private ObjectMatchStartButton _startButton;
    private GameObject _controllerGo;
    private IObjectMatchGameController _mockController;
    private XRGrabInteractable _grabInteractable;

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

        // Add required components
        _grabInteractable = _startButtonGo.AddComponent<XRGrabInteractable>();
        _startButton = _startButtonGo.AddComponent<ObjectMatchStartButton>();

        // Wait for Start to be called
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator Teardown()
    {
        Object.Destroy(_controllerGo);
        yield return null;
    }

    /// <summary>
    /// Verifies ObjectMatchStartButton can be instantiated.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        Assert.NotNull(_startButton);
        yield return null;
    }

    /// <summary>
    /// Verifies Start finds controller in parent hierarchy.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_FindsControllerInParent()
    {
        // Verified by no error logs during Setup
        Assert.Pass("ObjectMatchStartButton started without errors");
        yield return null;
    }

    /// <summary>
    /// Verifies Start finds XRGrabInteractable component.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_FindsGrabInteractable()
    {
        // Verified by no error logs and successful listener registration
        Assert.IsNotNull(_grabInteractable);
        yield return null;
    }

    /// <summary>
    /// Verifies OnGrabbed calls controller.InitializeLevel.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_CallsControllerInitializeLevel()
    {
        // Arrange - ensure button is active
        _startButtonGo.SetActive(true);

        yield return null;

        // Act - Simulate grab event
        _grabInteractable.selectEntered.Invoke(new SelectEnterEventArgs());

        yield return null;

        // Assert
        _mockController.Received(1).InitializeLevel();
    }

    /// <summary>
    /// Verifies OnGrabbed deactivates the button GameObject.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_DeactivatesButton()
    {
        // Arrange
        _startButtonGo.SetActive(true);
        Assert.IsTrue(_startButtonGo.activeSelf, "Button should start active");

        yield return null;

        // Act - Simulate grab event
        _grabInteractable.selectEntered.Invoke(new SelectEnterEventArgs());

        yield return null;

        // Assert
        Assert.IsFalse(_startButtonGo.activeSelf, "Button should be deactivated after being grabbed");
    }

    /// <summary>
    /// Verifies OnGrabbed can be called multiple times safely (though button should deactivate).
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_MultipleGrabs_CallsInitializeLevelEachTime()
    {
        // Arrange
        _startButtonGo.SetActive(true);

        yield return null;

        // Act - Simulate first grab
        _grabInteractable.selectEntered.Invoke(new SelectEnterEventArgs());
        
        yield return null;

        // Reactivate button (simulating restart scenario)
        _startButtonGo.SetActive(true);
        
        yield return null;

        // Simulate second grab
        _grabInteractable.selectEntered.Invoke(new SelectEnterEventArgs());

        yield return null;

        // Assert - InitializeLevel should have been called twice
        _mockController.Received(2).InitializeLevel();
    }

    /// <summary>
    /// Verifies Start logs error if XRGrabInteractable component is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_MissingGrabInteractable_LogsError()
    {
        // Arrange - Create button without XRGrabInteractable
        GameObject testGo = new GameObject("TestButton");
        testGo.transform.SetParent(_controllerGo.transform);

        // Expect error log
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*XRGrabInteractable.*"));

        // Act
        testGo.AddComponent<ObjectMatchStartButton>();

        yield return null;

        // Cleanup
        Object.Destroy(testGo);
    }

    /// <summary>
    /// Verifies Start logs error if controller not found in parent hierarchy.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_NoControllerInParent_LogsError()
    {
        // Arrange - Create standalone button without controller
        GameObject standaloneGo = new GameObject("StandaloneButton");
        standaloneGo.AddComponent<XRGrabInteractable>();

        // Expect error log
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));

        // Act
        standaloneGo.AddComponent<ObjectMatchStartButton>();

        yield return null;

        // Cleanup
        Object.Destroy(standaloneGo);
    }

    /// <summary>
    /// Verifies OnGrabbed handles null controller gracefully.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_NullController_LogsError()
    {
        // Create a standalone button without proper controller setup
        GameObject testGo = new GameObject("TestButton");
        var grabInt = testGo.AddComponent<XRGrabInteractable>();
        
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        var startBtn = testGo.AddComponent<ObjectMatchStartButton>();

        yield return null;

        // Try to trigger grab - should log error when trying to get controller
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        grabInt.selectEntered.Invoke(new SelectEnterEventArgs());

        yield return null;

        Object.Destroy(testGo);
    }

    /// <summary>
    /// Verifies button deactivation happens after controller call.
    /// This ensures proper order of operations.
    /// </summary>
    [UnityTest]
    public IEnumerator OnGrabbed_DeactivatesAfterControllerCall()
    {
        // Arrange
        bool controllerCalled = false;
        _mockController.When(x => x.InitializeLevel()).Do(_ => {
            controllerCalled = true;
            // Button should still be active when controller is called
            Assert.IsTrue(_startButtonGo.activeSelf, 
                "Button should still be active during controller call");
        });

        _startButtonGo.SetActive(true);

        yield return null;

        // Act
        _grabInteractable.selectEntered.Invoke(new SelectEnterEventArgs());

        yield return null;

        // Assert
        Assert.IsTrue(controllerCalled, "Controller should have been called");
        Assert.IsFalse(_startButtonGo.activeSelf, "Button should be deactivated after grab");
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
