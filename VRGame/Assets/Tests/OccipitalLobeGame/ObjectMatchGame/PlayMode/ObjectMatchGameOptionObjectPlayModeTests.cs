using NUnit.Framework;
using NSubstitute;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text.RegularExpressions;

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
        // 1. Create Grandparent (Controller)
        _controllerGo = new GameObject("ControllerParent");
        _mockController = Substitute.For<IObjectMatchGameController>();
        
        var controllerComponent = _controllerGo.AddComponent<MockControllerComponent>();
        controllerComponent.Controller = _mockController;

        // 2. Create Parent (OptionsParent)
        _parentGo = new GameObject("OptionsParent");
        _parentGo.transform.SetParent(_controllerGo.transform);

        // 3. Create GuessBox (Must be child of ControllerParent for .parent.parent.Find to work)
        _guessBoxGo = new GameObject("GuessBox");
        _guessBoxGo.transform.SetParent(_controllerGo.transform);
        _guessBoxGo.transform.position = new Vector3(5, 5, 5);
        _guessBoxGo.transform.rotation = Quaternion.Euler(0, 90, 0);

        // 4. Create OptionObject
        _optionObjectGo = new GameObject("OptionObject");
        _optionObjectGo.transform.SetParent(_parentGo.transform);
        
        // Set initial position BEFORE adding component so Start() captures it
        _optionObjectGo.transform.position = new Vector3(1, 1, 1);
        _optionObjectGo.transform.rotation = Quaternion.identity;

        // 5. Add components
        _grabInteractable = _optionObjectGo.AddComponent<XRGrabInteractable>();
        _optionObject = _optionObjectGo.AddComponent<ObjectMatchGameOptionObject>();

        // 6. Wait for Start() to execute
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator Teardown()
    {
        Object.Destroy(_controllerGo);
        yield return null;
    }

    [UnityTest]
    public IEnumerator OnReleased_NotCurrentGuess_ResetsToInitialPosition()
    {
        // Arrange
        _mockController.GetCurrentGuessID().Returns("DifferentObject");

        // Act: Move object and then release
        _optionObjectGo.transform.position = new Vector3(10, 10, 10);
        _grabInteractable.selectExited.Invoke(new SelectExitEventArgs());

        yield return null;

        // Assert
        // Its supposed to fail becuase of the float value , it may be very close to y axis by 0.001 
        Assert.AreEqual(new Vector3(1, 1, 1), _optionObjectGo.transform.position, 
            "Object did not reset to its starting position.");
    }

    [UnityTest]
    public IEnumerator OnReleased_IsCurrentGuess_MovesToGuessBox()
    {
        // Arrange
        _mockController.GetCurrentGuessID().Returns("OptionObject");

        // Act: Move object and then release
        _optionObjectGo.transform.position = new Vector3(2, 2, 2);
        _grabInteractable.selectExited.Invoke(new SelectExitEventArgs());

        yield return null;
        // Its supposed to fail becuase of the float value , it may be very close to y axis by 0.001 
        // Assert
        Assert.AreEqual(new Vector3(5, 5, 5), _optionObjectGo.transform.position,
            "Object did not move to the GuessBox position.");
        Assert.AreEqual(Quaternion.Euler(0, 90, 0).eulerAngles, _optionObjectGo.transform.rotation.eulerAngles,
            "Object did not adopt the GuessBox rotation.");
    }

    [UnityTest]
public IEnumerator Start_MissingGuessBox_LogsError()
{
    // Arrange: Create a standalone object with NO controller in the parent hierarchy
    GameObject standaloneGo = new GameObject("StandaloneOption");
    standaloneGo.AddComponent<XRGrabInteractable>();

    // 1. Expect the Controller Error (Line 40 in your script)
    LogAssert.Expect(LogType.Error, new Regex(".*could not find.*IObjectMatchGameController.*"));

    // 2. Expect the GuessBox Exception (Because parent.parent.Find will fail on a standalone object)
    LogAssert.Expect(LogType.Exception, new Regex(".*NullReferenceException.*"));

    // Act
    standaloneGo.AddComponent<ObjectMatchGameOptionObject>();

    yield return null;

    // Cleanup
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