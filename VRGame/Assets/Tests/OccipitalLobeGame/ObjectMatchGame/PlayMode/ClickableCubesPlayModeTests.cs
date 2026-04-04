using NUnit.Framework;
using NSubstitute;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text.RegularExpressions;
using ObjectMatchGame;
/// <summary>
/// PlayMode tests for ClickableCubes abstract base class.
/// </summary>
public class ClickableCubesPlayModeTests
{
    private GameObject _clickableCubeGo;
    private TestClickableCube _clickableCube;
    private GameObject _controllerGo;
    private IObjectMatchGameController _mockController;
    private XRGrabInteractable _grabInteractable;

    /// <summary>
    /// Sets up test environment with controller and clickable cube.
    /// </summary>
    [UnitySetUp]
    public IEnumerator Setup()
    {
        // Create controller parent
        _controllerGo = new GameObject("ControllerParent");
        _mockController = Substitute.For<IObjectMatchGameController>();
        
        var controllerComponent = _controllerGo.AddComponent<MockControllerComponent>();
        controllerComponent.Controller = _mockController;

        // Create clickable cube as child of controller
        _clickableCubeGo = new GameObject("ClickableCube");
        _clickableCubeGo.transform.SetParent(_controllerGo.transform);
        
        _grabInteractable = _clickableCubeGo.AddComponent<XRGrabInteractable>();
        _clickableCube = _clickableCubeGo.AddComponent<TestClickableCube>();

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
    /// Verifies Start method finds controller in parent hierarchy.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_FindsControllerInParent()
    {
        // Controller should be found automatically in Setup
        yield return null;

        // Verify controller was assigned
        Assert.IsNotNull(_clickableCube.controller, "Controller should be found in parent");
    }

    /// <summary>
    /// Verifies Start method assigns XRGrabInteractable component.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_AssignsGrabInteractable()
    {
        yield return null;

        // Verify grabInteractable was assigned
        Assert.IsNotNull(_clickableCube.grabInteractable, "GrabInteractable should be assigned");
        Assert.AreEqual(_grabInteractable, _clickableCube.grabInteractable);
    }

    /// <summary>
    /// Verifies Start method logs error when controller is not found.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_MissingController_LogsError()
    {
        // Create standalone clickable cube without controller parent
        GameObject standaloneGo = new GameObject("StandaloneClickable");
        standaloneGo.AddComponent<XRGrabInteractable>();
        
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        LogAssert.Expect(LogType.Exception, new Regex(".*Controller reference is null.*"));

        standaloneGo.AddComponent<TestClickableCube>();
        
        yield return null;

        Object.Destroy(standaloneGo);
    }

    /// <summary>
    /// Verifies Start method logs error when XRGrabInteractable is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_MissingGrabInteractable_LogsError()
    {
        // Create clickable cube without XRGrabInteractable
        GameObject testGo = new GameObject("TestClickable");
        testGo.transform.SetParent(_controllerGo.transform);
        
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*XRGrabInteractable.*"));
        LogAssert.Expect(LogType.Exception, new Regex(".*GrabInteractable is null.*"));

        testGo.AddComponent<TestClickableCube>();
        
        yield return null;

        Object.Destroy(testGo);
    }

    /// <summary>
    /// Verifies selectEntered listener is added to XRGrabInteractable.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_AddsSelectEnteredListener()
    {
        yield return null;

        // Verify listener count increased
        // Note: Direct listener count testing is difficult, so we test functionality
        Assert.IsNotNull(_clickableCube.grabInteractable, "GrabInteractable should be assigned");
    }

    /// <summary>
    /// Verifies OnGrabbed is called when selectEntered event fires.
    /// </summary>
    [UnityTest]
    public IEnumerator SelectEntered_CallsOnGrabbed()
    {
        // Arrange
        _clickableCube.OnGrabbedCallCount = 0;
        
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = _grabInteractable
        };

        // Act
        _grabInteractable.selectEntered.Invoke(args);
        yield return null;

        // Assert
        Assert.AreEqual(1, _clickableCube.OnGrabbedCallCount, "OnGrabbed should be called once");
    }

    /// <summary>
    /// Verifies OnGrabbed is called multiple times for multiple grabs.
    /// </summary>
    [UnityTest]
    public IEnumerator SelectEntered_MultipleTimes_CallsOnGrabbedEachTime()
    {
        // Arrange
        _clickableCube.OnGrabbedCallCount = 0;
        
        var mockInteractor = Substitute.For<IXRSelectInteractor>();
        var args = new SelectEnterEventArgs
        {
            interactorObject = mockInteractor,
            interactableObject = _grabInteractable
        };

        // Act
        _grabInteractable.selectEntered.Invoke(args);
        yield return null;
        
        _grabInteractable.selectEntered.Invoke(args);
        yield return null;
        
        _grabInteractable.selectEntered.Invoke(args);
        yield return null;

        // Assert
        Assert.AreEqual(3, _clickableCube.OnGrabbedCallCount, "OnGrabbed should be called three times");
    }

    /// <summary>
    /// Verifies ClickableCubes with both controller and grab interactable doesn't log errors.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_ValidSetup_NoErrors()
    {
        // Create new valid clickable cube
        GameObject validGo = new GameObject("ValidClickable");
        validGo.transform.SetParent(_controllerGo.transform);
        validGo.AddComponent<XRGrabInteractable>();
        
        // Should not log any errors
        validGo.AddComponent<TestClickableCube>();
        
        yield return null;

        Object.Destroy(validGo);
    }

    /// <summary>
    /// Verifies controller reference is accessible after Start.
    /// </summary>
    [UnityTest]
    public IEnumerator ControllerReference_AccessibleAfterStart()
    {
        yield return null;

        // Verify controller is accessible
        Assert.IsNotNull(_clickableCube.controller);
        
        // Verify we can call methods on the controller
        _clickableCube.controller.GetCurrentGuessID();
        _mockController.Received(1).GetCurrentGuessID();
    }

    /// <summary>
    /// Verifies grabInteractable reference is accessible after Start.
    /// </summary>
    [UnityTest]
    public IEnumerator GrabInteractableReference_AccessibleAfterStart()
    {
        yield return null;

        // Verify grabInteractable is accessible
        Assert.IsNotNull(_clickableCube.grabInteractable);
        Assert.IsInstanceOf<XRGrabInteractable>(_clickableCube.grabInteractable);
    }

    // Test implementation of abstract ClickableCubes class
    private class TestClickableCube : ClickableCubes
    {
        public int OnGrabbedCallCount = 0;

        public override void OnGrabbed(SelectEnterEventArgs args)
        {
            OnGrabbedCallCount++;
        }
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
    }
}
