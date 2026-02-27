using System;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.TestTools;

/// <summary>
/// Unit tests for RoomView class.
/// </summary>
public class RoomViewTests
{
    /// <summary>
    /// Test the initialization of RoomView.
    /// </summary>
    [Test]
    public void Instantiation()
    {
        // test setup
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        // confirm that roomView is not null
        Assert.NotNull(roomView, $"roomView cannot be null. Got {roomView}");

        // substitute mocks
        IRoomController roomController = Substitute.For<IRoomController>();

        roomView.RoomController = roomController;

        // initialize the component
        roomView.Init();

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test to see if MinigameCompleted() will throw an error
    /// with valid controller reference.
    /// </summary>
    [Test]
    public void MinigameCompletedValidLayerRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        // substitute mocks
        IRoomController roomController = Substitute.For<IRoomController>();

        roomView.RoomController = roomController;

        // should not throw since there is a valid reference to controller
        Assert.DoesNotThrow(() => roomView.MinigameCompleted(), "Expected no exception, but an exception was thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test to see if MinigameCompleted() will throw an error
    /// with missing controller reference.
    /// </summary>
    [Test]
    public void MinigameCompletedMissingLayerRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        // substitute mocks
        IRoomController roomController = Substitute.For<IRoomController>();

        roomView.RoomController = roomController;

        // should not throw since there is a valid reference to controller
        Assert.DoesNotThrow(() => roomView.MinigameCompleted(), "Expected no exception, but an exception was thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }


    /// <summary>
    /// Test to see if EducationalDialoguesCompleted() will throw an error
    /// with valid controller reference.
    /// </summary>
    [Test]
    public void EducationalDialoguesCompletedValidLayerRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        // substitute mocks
        IRoomController roomController = Substitute.For<IRoomController>();

        roomView.RoomController = roomController;

        // should not throw since there is a valid reference to controller
        Assert.DoesNotThrow(() => roomView.EducationalDialoguesCompleted(), "Expected no exception, but an exception was thrown on valid layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test to see if EducationalDialoguesCompleted() will throw an error
    /// with missing controller reference.
    /// </summary>
    [Test]
    public void EducationalDialoguesCompletedMissingLayerRef()
    {
        // test setup
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");

        // should throw since there is no valid reference to controller
        Assert.Throws<MissingFieldException>(() => roomView.EducationalDialoguesCompleted(), "Expected an exception, but no exception was thrown on missing layers.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test if InvokeOnRoomComplete() will invoke listeners.
    /// </summary>
    [Test]
    public void InvokeOnRoomComplete()
    {
        // test setup
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        var result = false;
        roomView.onRoomCompleted.AddListener(() =>
        {
            result = true;
        });

        roomView.InvokeOnRoomComplete();

        // result should be true since we invoked the event
        Assert.IsTrue(result, "Expected listeners to be invoked, but did not get invoked.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }
}