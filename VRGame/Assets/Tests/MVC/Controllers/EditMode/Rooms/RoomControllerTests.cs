using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using System;

/// <summary>
/// Unit tests for RoomController class.
/// </summary>
public class RoomControllerTests
{
    /// <summary>
    /// Test the initialization of RoomController.
    /// </summary>
    [Test]
    public void Instantiation()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // confirm that roomController is not null
        Assert.NotNull(roomController, $"roomController cannot be null. Got {roomController}");

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        // initialize the component
        roomController.Init();

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteEducationalDialogue() with missing model layer
    /// </summary>
    [Test]
    public void HandleCompleteEducationalDialogueMissingLayersRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();

        roomController.RoomView = roomView;

        // should throw since there's no valid references to model layer
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompleteEducationalDialogue(), "Expected an exception, but no exception was thrown on missing layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteEducationalDialogue() with model layer
    /// </summary>
    [Test]
    public void HandleCompleteEducationalDialogueValidLayersRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        // should not throw since there's valid references to view and model layer
        Assert.DoesNotThrow(() => roomController.HandleCompleteEducationalDialogue(), "Expected no exceptions, but an exception was thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with missing model layer
    /// </summary>
    [Test]
    public void HandleCompleteMinigameMissingLayersRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();

        roomController.RoomView = roomView;

        // should throw since there's no valid references to model layer
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompleteMinigame(), "Expected an exception, but no exception was thrown on missing layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with model layer
    /// </summary>
    [Test]
    public void HandleCompleteMinigameValidLayersRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        // should not throw since there's valid references to view and model layer
        Assert.DoesNotThrow(() => roomController.HandleCompleteMinigame(), "Expected no exceptions, but an exception was thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with model/view layer
    /// </summary>
    [Test]
    public void HandleCompletionValidLayersRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        // should not throw since there's valid references to view and model layer
        Assert.DoesNotThrow(() => roomController.HandleCompletion(), "Expected no exceptions, but an exceptionw as thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompletion() with missing model/view layer
    /// </summary>
    [Test]
    public void HandleCompletionMissingLayersRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // confirm that roomController is not null
        Assert.NotNull(roomController, $"roomController cannot be null. Got {roomController}");

        // should throw since there is no valid references to view and model layer
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompletion(), "Expected an exception, but no exception was thrown on missing layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompletion() with model/view layer
    /// </summary>
    [Test]
    public void HandleCompletionEdgeCases()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        roomController.RoomView = roomView;

        // should throw since there is no valid reference to model
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompletion(), "Expected an exception, but no exception was thrown on missing layers.");

        roomController.RoomView = null;
        roomController.RoomModel = roomModel;
        
        // should throw since there is no valid reference to view
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompletion(), "Expected an exception, but no exception was thrown on missing layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }
}