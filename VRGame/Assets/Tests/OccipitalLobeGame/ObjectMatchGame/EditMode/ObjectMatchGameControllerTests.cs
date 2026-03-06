using NUnit.Framework;
using NSubstitute;
using ObjectMatchGame;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

public class ObjectMatchGameControllerTests
{
    private GameObject _go;
    private ObjectMatchGameController _controller;
    private IObjectMatchGameModel _mockModel;
    private IObjectMatchGameView _mockView;

    [SetUp]
    public void Setup()
    {
        _go = new GameObject("TestController");
        _controller = _go.AddComponent<ObjectMatchGameController>();
        
        _mockModel = Substitute.For<IObjectMatchGameModel>();
        _mockView = Substitute.For<IObjectMatchGameView>();

        // Direct assignment now working
        _controller.model = _mockModel;
        _controller.view = _mockView;
    }

    [TearDown]
    public void Teardown() => Object.DestroyImmediate(_go);

    // --- SECTION 1: INITIALIZATION & REF CHECKS ---

    [Test]
    public void Init_ValidPreconditions_SuccessfullyInitializes()
    {
        // Pre-condition: Model and View are assigned
        // Post-condition: No exceptions thrown
        Assert.DoesNotThrow(() => _controller.Init());
    }

    [Test]
    public void Init_NullModel_LogsError()
    {
        // Pre-condition: Model is null
        _controller.model = null;
        
        // Post-condition: Base class CheckModelRef triggers an error/assertion
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try {
            _controller.Init();
        } catch { /* Absorbing potential Assert failure from base class */ }
    }

    // --- SECTION 2: VIEW TO CONTROLLER INTERACTION ---

    [Test]
    public void PotentialGuess_ValidString_PassesToModel()
    {
        // Pre-condition: View passes a valid ID string
        string testID = "Target_Object_A";

        // Act
        _controller.PotentialGuess(testID);

        // Post-condition: Controller correctly delegated the call to the Model
        _mockModel.Received(1).PotentialGuess(testID);
    }

    [Test]
    public void RemovePotentialGuess_ViewInteraction_CallsModel()
    {
        // Act
        _controller.RemovePotentialGuess();

        // Post-condition: Model state is updated to clear guess
        _mockModel.Received(1).RemovePotentialGuess();
    }

    // --- SECTION 3: CONTROLLER TO MODEL INTERACTION ---

    [Test]
    public void InitializeLevel_CallsModelMethodsSequentially()
    {
        // Arrange
        _mockModel.GetActiveObjectIDs().Returns(new string[] { "ID1" });

        // Act
        _controller.InitializeLevel();

        // Post-condition: Model logic for level setup is triggered
        _mockModel.Received(1).InitializeLevel();
    }

    [Test]
    public void GetCurrentGuessID_QueriesModelForState()
    {
        // Arrange
        _mockModel.GetCurrentGuessID().Returns("Test_ID");

        // Act
        var result = _controller.GetCurrentGuessID();

        // Post-condition: Controller returns exactly what the Model reports
        Assert.AreEqual("Test_ID", result);
        _mockModel.Received(1).GetCurrentGuessID();
    }

    // --- SECTION 4: CONTROLLER TO VIEW (BRIDGE) INTERACTION ---

    [Test]
    public void InitializeLevel_PostCondition_UpdatesViewWithModelData()
    {
        // Arrange: Pre-condition - Model provides specific IDs
        string[] mockIDs = { "Object_1", "Object_2", "Object_3" };
        _mockModel.GetActiveObjectIDs().Returns(mockIDs);

        // Act
        _controller.InitializeLevel();

        // Post-condition: View is commanded to show the exact IDs retrieved from the Model
        _mockView.Received(1).ShowObjects(mockIDs);
    }

    // --- SECTION 5: UNIMPLEMENTED METHODS (POST-CONDITION: THROW) ---

    [Test]
    public void InitializeTutorial_ThrowsNotImplemented()
    {
        Assert.Throws<System.NotImplementedException>(() => _controller.InitializeTutorial());
    }

    [Test]
    public void RestartGame_ThrowsNotImplemented()
    {
        Assert.Throws<System.NotImplementedException>(() => _controller.RestartGame());
    }
}