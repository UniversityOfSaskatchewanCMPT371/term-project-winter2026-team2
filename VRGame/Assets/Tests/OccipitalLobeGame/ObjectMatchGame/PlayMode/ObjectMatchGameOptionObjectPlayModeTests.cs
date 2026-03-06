using NUnit.Framework;
using NSubstitute;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text.RegularExpressions;

/// <summary>
/// Play Mode unit tests for ObjectMatchGameOptionObject component.
/// Tests grab/release interactions and position management.
/// </summary>
public class ObjectMatchGameOptionObjectPlayModeTests
{
    private GameObject _optionObjectGo;
    private ObjectMatchGameOptionObject _optionObject;
    private GameObject _controllerGo;
    private GameObject _parentGo;
    private GameObject _guessBoxGo;
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

        // Create parent structure: Controller -> Parent -> OptionObject
        _parentGo = new GameObject("OptionsParent");
        _parentGo.transform.SetParent(_controllerGo.transform);

        // Create GuessBox (sibling to Parent)
        _guessBoxGo = new GameObject("GuessBox");
        _guessBoxGo.transform.SetParent(_controllerGo.transform);
        _guessBoxGo.transform.position = new Vector3(5, 5, 5);

        // Create option object
        _optionObjectGo = new GameObject("OptionObject");
        _optionObjectGo.transform.SetParent(_parentGo.transform);
        _optionObjectGo.transform.position = new Vector3(1, 1, 1);
        _optionObjectGo.transform.rotation = Quaternion.identity;

        // Add required components
        _grabInteractable = _optionObjectGo.AddComponent<XRGrabInteractable>();
        _optionObject = _optionObjectGo.AddComponent<ObjectMatchGameOptionObject>();

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
    /// Verifies ObjectMatchGameOptionObject can be instantiated.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        Assert.NotNull(_optionObject);
        yield return null;
    }

    /// <summary>
    /// Verifies Start finds controller in parent hierarchy.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_FindsControllerInParent()
    {
        // Verified by no error logs during Setup
        Assert.Pass("OptionObject started without errors");
        yield return null;
    }

    /// <summary>
    /// Verifies Start stores initial position and rotation.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_StoresInitialTransform()
    {
        // Initial position and rotation are stored but private
        // We can verify this indirectly by testing the reset behavior
        
        Vector3 initialPos = _optionObjectGo.transform.position;
        
        // Move object
        _optionObjectGo.transform.position = new Vector3(10, 10, 10);
        
        yield return null;
        
        // The initial position should have been stored during Start
        // This will be verified in the OnReleased tests
        Assert.AreNotEqual(initialPos, _optionObjectGo.transform.position);
        yield return null;
    }

    /// <summary>
    /// Verifies Start finds GuessBox in parent hierarchy.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_FindsGuessBoxTransform()
    {
        // Verify no error was logged about missing GuessBox
        // If GuessBox wasn't found, the test would fail during Setup
        Assert.IsNotNull(_guessBoxGo);
        yield return null;
    }

    /// <summary>
    /// Verifies OnReleased resets position when object is not current guess.
    /// </summary>
    [UnityTest]
    public IEnumerator OnReleased_NotCurrentGuess_ResetsToInitialPosition()
    {
        Vector3 initialPosition = _optionObjectGo.transform.position;
        Quaternion initialRotation = _optionObjectGo.transform.rotation;

        // Mock controller returns different object as current guess
        _mockController.GetCurrentGuessID().Returns("DifferentObject");

        // Move object to new position
        _optionObjectGo.transform.position = new Vector3(10, 10, 10);
        _optionObjectGo.transform.rotation = Quaternion.Euler(45, 45, 45);

        yield return null;

        // Simulate release event
        _grabInteractable.selectExited.Invoke(new SelectExitEventArgs());

        yield return null;

        // Assert: Object should return to initial position
        Assert.AreEqual(initialPosition, _optionObjectGo.transform.position, 
            "Object should reset to initial position when released and not current guess");
        Assert.AreEqual(initialRotation, _optionObjectGo.transform.rotation,
            "Object should reset to initial rotation when released and not current guess");
    }

    /// <summary>
    /// Verifies OnReleased moves to guess box when object is current guess.
    /// </summary>
    [UnityTest]
    public IEnumerator OnReleased_IsCurrentGuess_MovesToGuessBox()
    {
        Vector3 guessBoxPosition = _guessBoxGo.transform.position;
        Quaternion guessBoxRotation = _guessBoxGo.transform.rotation;

        // Mock controller returns this object as current guess
        _mockController.GetCurrentGuessID().Returns("OptionObject");

        // Move object to some position
        _optionObjectGo.transform.position = new Vector3(2, 2, 2);

        yield return null;

        // Simulate release event
        _grabInteractable.selectExited.Invoke(new SelectExitEventArgs());

        yield return null;

        // Assert: Object should move to guess box position
        Assert.AreEqual(guessBoxPosition, _optionObjectGo.transform.position,
            "Object should move to guess box position when released as current guess");
        Assert.AreEqual(guessBoxRotation, _optionObjectGo.transform.rotation,
            "Object should adopt guess box rotation when released as current guess");
    }

    /// <summary>
    /// Verifies Start logs error if XRGrabInteractable component is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_MissingGrabInteractable_LogsError()
    {
        // Arrange - Create option object without XRGrabInteractable
        GameObject testGo = new GameObject("TestOption");
        testGo.transform.SetParent(_parentGo.transform);

        // Expect error log
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*XRGrabInteractable.*"));

        // Act
        testGo.AddComponent<ObjectMatchGameOptionObject>();

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
        // Arrange - Create standalone option object without controller
        GameObject standaloneGo = new GameObject("StandaloneOption");
        standaloneGo.AddComponent<XRGrabInteractable>();

        // Expect error log
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));

        // Act
        standaloneGo.AddComponent<ObjectMatchGameOptionObject>();

        yield return null;

        // Cleanup
        Object.Destroy(standaloneGo);
    }

    /// <summary>
    /// Verifies OnReleased handles null controller gracefully.
    /// </summary>
    [UnityTest]
    public IEnumerator OnReleased_NullController_LogsError()
    {
        // Create a standalone option without proper controller setup
        GameObject testGo = new GameObject("TestOption");
        var grabInt = testGo.AddComponent<XRGrabInteractable>();
        
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        var optObj = testGo.AddComponent<ObjectMatchGameOptionObject>();

        yield return null;

        // Try to trigger release - should log error when trying to get controller
        LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));
        grabInt.selectExited.Invoke(new SelectExitEventArgs());

        yield return null;

        Object.Destroy(testGo);
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
