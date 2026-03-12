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
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // confirm that roomView is not null
        Assert.NotNull(roomView, $"roomView cannot be null. Got {roomView}");

        // substitute mocks
        IRoomController roomController = Substitute.For<IRoomController>();

        // assign mock
        roomView.ControllerMock = roomController;

        // initialize the component
        roomView.Init();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test to see if MinigameCompleted() will throw an error
    /// with valid 'controller' component.
    /// </summary>
    [Test]
    public void MinigameCompletedValidLayerRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // substitute mocks
        IRoomController roomController = Substitute.For<IRoomController>();

        // assign mock
        roomView.ControllerMock = roomController;

        // expect no exception to be thrown since 'controller' is assigned
        Assert.DoesNotThrow(() => roomView.MinigameCompleted(), "Expected no exception, but an exception was thrown on valid layers.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test to see if MinigameCompleted() will throw an error
    /// with missing 'controller' component.
    /// </summary>
    [Test]
    public void MinigameCompletedMissingLayerRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // expect a warning since 'controller' component is missing
        LogAssert.Expect(LogType.Warning, "Controller component not initialized.");

        roomView.MinigameCompleted();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }


    /// <summary>
    /// Test to see if EducationalDialoguesCompleted() will throw an error
    /// with valid 'controller' component.
    /// </summary>
    [Test]
    public void EducationalDialoguesCompletedValidLayerRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // substitute mocks
        IRoomController roomController = Substitute.For<IRoomController>();

        // aassign mock
        roomView.ControllerMock = roomController;

        // expect no exception to be thrown since 'controller' is assigned
        Assert.DoesNotThrow(() => roomView.EducationalDialoguesCompleted(), "Expected no exception, but an exception was thrown on valid layers.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test to see if EducationalDialoguesCompleted() will throw an error
    /// with missing 'controller' component.
    /// </summary>
    [Test]
    public void EducationalDialoguesCompletedMissingLayerRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // expect a warning since 'controller' component is missing
        LogAssert.Expect(LogType.Warning, "Controller component not initialized.");
        
        roomView.EducationalDialoguesCompleted();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test if InvokeOnRoomComplete() will invoke listeners.
    /// </summary>
    [Test]
    public void InvokeOnRoomComplete()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // add a listener that can be invoked to verify that it works
        var result = false;
        roomView.onRoomCompleted.AddListener(() =>
        {
            result = true;
        });

        // invoke event
        roomView.InvokeOnRoomComplete();

        // verify flag updated
        Assert.IsTrue(result, "Expected listeners to be invoked, but did not get invoked.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }
}