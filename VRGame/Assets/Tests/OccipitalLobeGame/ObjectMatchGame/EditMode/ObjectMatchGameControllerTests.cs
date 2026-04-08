using NUnit.Framework;
using NSubstitute;
using ObjectMatchGame;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

/// <summary>
/// Unit tests for ObjectMatchGameController using localized Arrange-Act-Assert blocks.
/// </summary>
public class ObjectMatchGameControllerTests
{
    // --- SECTION 1: INITIALIZATION & REF CHECKS ---

    [Test]
    public void Init_ValidPreconditions_SuccessfullyInitializes()
    {
        // Arrange
        GameObject go = new GameObject("TestController");
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();
        IObjectMatchGameModel mockModel = Substitute.For<IObjectMatchGameModel>();
        IObjectMatchGameView mockView = Substitute.For<IObjectMatchGameView>();

     
        controller.ViewMock = mockView;
        controller.ModelMock = mockModel;

        // Act & Assert
        Assert.DoesNotThrow(() => controller.Init());

        // Cleanup
        Object.DestroyImmediate(go);
    }

    // --- SECTION 2: DELEGATION LOGIC ---

    [Test]
    public void PotentialGuess_ValidID_DelegatesToModel()
    {
        // Arrange
        GameObject go = new GameObject("TestController");
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();
        IObjectMatchGameModel mockModel = Substitute.For<IObjectMatchGameModel>();
        controller.ModelMock = mockModel;
        controller.ViewMock = Substitute.For<IObjectMatchGameView>();
        
        string testID = "Object_A";

        // Act
        controller.PotentialGuess(testID);

        // Assert
        mockModel.Received(1).PotentialGuess(testID);

        // Cleanup
        Object.DestroyImmediate(go);
    }

    [Test]
    public void SubmitGuess_ValidCall_TriggersModelSubmit()
    {
        // Arrange
        GameObject go = new GameObject("TestController");
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();
        IObjectMatchGameModel mockModel = Substitute.For<IObjectMatchGameModel>();
        controller.ModelMock = mockModel;
        controller.ViewMock = Substitute.For<IObjectMatchGameView>();
        mockModel.GetCurrentGuessID().Returns("object1");

        // Act
        controller.SubmitGuess();

        // Assert
        mockModel.Received(1).SubmitGuess();

        // Cleanup
        Object.DestroyImmediate(go);
    }

    // --- SECTION 3: FLOW & STATE MEDIATION ---

    [Test]
    public void InitializeLevel_FetchesIDsAndUpdatesView()
    {
        // Arrange
        GameObject go = new GameObject("TestController");
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();
        IObjectMatchGameModel mockModel = Substitute.For<IObjectMatchGameModel>();
        IObjectMatchGameView mockView = Substitute.For<IObjectMatchGameView>();
        controller.ModelMock = mockModel;
        controller.ViewMock = mockView;

        string[] mockIDs = { "ID_1", "ID_2" };
        mockModel.GetActiveObjectIDs().Returns(mockIDs);

        // Act
        controller.InitializeLevel();

        // Assert
        mockModel.Received(1).InitializeLevel();
        mockView.Received(1).ShowObjects(mockIDs);

        // Cleanup
        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetCurrentGuessID_ReturnsValueFromModel()
    {
        // Arrange
        GameObject go = new GameObject("TestController");
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();
        IObjectMatchGameModel mockModel = Substitute.For<IObjectMatchGameModel>();
        controller.ModelMock = mockModel;
        controller.ViewMock = Substitute.For<IObjectMatchGameView>();

        string expectedID = "Target_Object_XYZ";
        mockModel.GetCurrentGuessID().Returns(expectedID);

        // Act
        string actualID = controller.GetCurrentGuessID();

        // Assert
        Assert.AreEqual(expectedID, actualID);
        mockModel.Received(1).GetCurrentGuessID();

        // Cleanup
        Object.DestroyImmediate(go);
    }

    // --- SECTION 4: INTEGRATION FLOW ---

    [Test]
    public void CompleteGameFlow_MaintainsCorrectSequence()
    {
        // Arrange
        GameObject go = new GameObject("TestController");
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();
        IObjectMatchGameModel mockModel = Substitute.For<IObjectMatchGameModel>();
        IObjectMatchGameView mockView = Substitute.For<IObjectMatchGameView>();
        controller.ModelMock = mockModel;
        controller.ViewMock = mockView;

        string[] levelIDs = { "A", "B" };
        mockModel.GetActiveObjectIDs().Returns(levelIDs);
        mockModel.GetCurrentGuessID().Returns("A");

        // Act
        controller.InitializeLevel();
        controller.PotentialGuess("A");
        string guess = controller.GetCurrentGuessID();
        controller.SubmitGuess();

        // Assert
        mockModel.Received(1).InitializeLevel();
        mockView.Received(1).ShowObjects(levelIDs);
        mockModel.Received(1).PotentialGuess("A");
        mockModel.Received(1).SubmitGuess();
        Assert.AreEqual("A", guess);

        // Cleanup
        Object.DestroyImmediate(go);
    }

    // --- SECTION 5: ERROR HANDLING & EDGE CASES ---

    [Test]
    public void InitializeLevel_NullIDs_LogsWarningOrHandlesGracefully()
    {
        // Arrange
        GameObject go = new GameObject("TestController");
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();
        IObjectMatchGameModel mockModel = Substitute.For<IObjectMatchGameModel>();
        IObjectMatchGameView mockView = Substitute.For<IObjectMatchGameView>();
        controller.ModelMock = mockModel;
        controller.ViewMock = mockView;

        // Model returns null array
        mockModel.GetActiveObjectIDs().Returns((string[])null);

        // Act & Assert
        // We expect it not to crash, but perhaps log a warning depending on your implementation
        Assert.DoesNotThrow(() => controller.InitializeLevel());

        // Cleanup
        Object.DestroyImmediate(go);
    }
}