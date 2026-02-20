using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using System;
using UnityEngine.TestTools;
using System.Collections;

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

        // initialize the component.
        roomController.Init();

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteEducationalDialogue() with missing model layer
    /// </summary>
    [Test]
    public void HandleCompleteEducationalDialogueMissingLayers()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();

        roomController.RoomView = roomView;

        // test if it will throw an exception
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompleteEducationalDialogue(), "Expected an exception, but no exception was thrown on missing layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteEducationalDialogue() with model layer
    /// </summary>
    [Test]
    public void HandleCompleteEducationalDialogueValidLayers()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        // test if it will not throw an exception
        Assert.DoesNotThrow(() => roomController.HandleCompleteEducationalDialogue(), "Expected no exceptions, but an exception was thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with missing model layer
    /// </summary>
    [Test]
    public void HandleCompleteMinigameMissingLayers()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();

        roomController.RoomView = roomView;

        // test if it will throw an exception
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompleteMinigame(), "Expected an exception, but no exception was thrown on missing layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with model layer
    /// </summary>
    [Test]
    public void HandleCompleteMinigameValidLayers()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        // test if it will not throw an exception
        Assert.DoesNotThrow(() => roomController.HandleCompleteMinigame(), "Expected no exceptions, but an exception was thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with model/view layer
    /// </summary>
    [Test]
    public void HandleCompletionValidLayers()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        // test if it will not throw an exception
        Assert.DoesNotThrow(() => roomController.HandleCompletion(), "Expected no exceptions, but an exceptionw as thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompletion() with missing model/view layer
    /// </summary>
    [Test]
    public void HandleCompletionMissingLayers()
    {
        // test setup
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // confirm that roomController is not null
        Assert.NotNull(roomController, $"roomController cannot be null. Got {roomController}");

        // test if it will throw an exception
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

        // test if it will throw an exceptions on just the model layer missing
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompletion(), "Expected an exception, but no exception was thrown on missing layers.");

        roomController.RoomView = null;
        roomController.RoomModel = roomModel;
        
        // test if it will throw an exceptions on just the view layer missing
        Assert.Throws<MissingFieldException>(() => roomController.HandleCompletion(), "Expected an exception, but no exception was thrown on missing layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator Initialization()
    {
        yield return null;
    }
}