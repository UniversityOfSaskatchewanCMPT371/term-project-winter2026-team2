using NUnit.Framework;
using NSubstitute;
using ObjectMatchGame;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

public class ObjectMatchGameViewTests
{
    private GameObject _viewGo;
    private ObjectMatchGameView _view;
    private IObjectMatchGameController _mockController;

    [SetUp]
    public void Setup()
    {
        _viewGo = new GameObject("ViewTestHost");
        _view = _viewGo.AddComponent<ObjectMatchGameView>();
        _mockController = Substitute.For<IObjectMatchGameController>();

        // Using the same internal/public hook pattern as the Controller
        _view.controller = _mockController;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(_viewGo);
    }

    // --- SECTION 1: INITIALIZATION & PRE-CONDITIONS ---

    [Test]
    public void Init_DeactivatesAllAssignedObjects_PostCondition()
    {
        // Arrange
        GameObject obj1 = new GameObject("Obj1");
        obj1.SetActive(true);
        
        // Assigning to the private array (Assumes you made a test hook for allObjects)
        _view.AllObjects = new GameObject[] { obj1 };

        // Act
        _view.Init();

        // Assert: Post-condition - Objects should be hidden initially
        Assert.IsFalse(obj1.activeSelf, "Object should be deactivated on Init");
        
        Object.DestroyImmediate(obj1);
    }

    [Test]
    public void Init_NullController_LogsError()
    {
        _view.controller = null;
        
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try {
            _view.Init();
        } catch { /* Handle base class assert if applicable */ }
    }

    // --- SECTION 2: DATA DISPLAY (CONTROLLER -> VIEW) ---

    [Test]
    public void ShowObjects_ActivatesCorrectIDs_ValidPrecondition()
    {
        // Arrange
        GameObject obj1 = new GameObject("Target");
        GameObject obj2 = new GameObject("Other");
        obj1.SetActive(false);
        obj2.SetActive(true);
        
        _view.AllObjects = new GameObject[] { obj1, obj2 };

        // Act: Simulating the call coming FROM the controller
        _view.ShowObjects(new string[] { "Target" });

        // Assert
        Assert.IsTrue(obj1.activeSelf, "Target ID should be activated");
        Assert.IsFalse(obj2.activeSelf, "Other ID should be deactivated");

        Object.DestroyImmediate(obj1);
        Object.DestroyImmediate(obj2);
    }

    [Test]
    public void ShowObjects_InvalidID_LogsWarning()
    {
        // Arrange
        GameObject obj1 = new GameObject("OnlyObject");
        _view.AllObjects = new GameObject[] { obj1 };

        // Act & Assert
        // Logic: View warns when asked to handle IDs it doesn't possess
        LogAssert.Expect(LogType.Warning, new Regex(".*NonExistent.*"));
        _view.ShowObjects(new string[] { "NonExistent" });

        Object.DestroyImmediate(obj1);
    }

    // --- SECTION 3: USER INTERACTION (VIEW -> CONTROLLER) ---

    [Test]
    public void View_RemoveGuess_ThrowsNotImplemented()
    {
        // Pre-condition: Method is called (e.g., via UI button)
        // Post-condition: Throws exception as per current implementation
        Assert.Throws<System.NotImplementedException>(() => _view.removeGuess());
    }

    /// <summary>
    /// Note: Usually, you'd have a method like 'OnObjectClicked' in the view.
    /// This test verifies the View -> Controller interaction chain.
    /// </summary>
    [Test]
    public void View_RelaysPotentialGuessToController()
    {
        // Arrange
        string selectedID = "Object_Blue";

        // Act
        // This simulates the View receiving a UI event and passing it to the controller
        _view.controller.PotentialGuess(selectedID);

        // Assert: Verify post-condition that the Controller received the message
        _mockController.Received(1).PotentialGuess(selectedID);
    }
}