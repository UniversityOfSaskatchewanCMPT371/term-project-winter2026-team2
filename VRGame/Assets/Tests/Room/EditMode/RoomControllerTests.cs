using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using System;
using UnityEngine.TestTools;

/// <summary>
/// Unit tests for RoomController class.
/// </summary>
public class RoomControllerTests
{
    /// <summary>
    /// Test the initialization of 'controller'.
    /// </summary>
    [Test]
    public void Instantiation()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'controller' component
        RoomController roomController = go.AddComponent<RoomController>();

        // confirm that 'controller' is not null
        Assert.NotNull(roomController, $"roomController cannot be null. Got {roomController}");

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        // assign mocks
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // initialize the component
        roomController.Init();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteEducationalDialogue() with missing 'model'
    /// </summary>
    [Test]
    public void HandleCompleteEducationalDialogueMissingLayersRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'controller' component
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();

        // assign mock
        roomController.ViewMock = roomView;

        // expect a warning since 'model' component is missing
        LogAssert.Expect(LogType.Warning, "Model component not initialized.");

        roomController.HandleCompleteEducationalDialogue();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteEducationalDialogue() with 'model' component
    /// </summary>
    [Test]
    public void HandleCompleteEducationalDialogueValidLayersRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'controller' component
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        // assign mocks
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // expect no exception to be thrown since 'view' and 'model' components were assigned
        Assert.DoesNotThrow(() => roomController.HandleCompleteEducationalDialogue(), "Expected no exceptions, but an exception was thrown on valid layers.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with missing 'model' component
    /// </summary>
    [Test]
    public void HandleCompleteMinigameMissingLayersRef()
    {
        // create Gameobject
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();

        // assign mock
        roomController.ViewMock = roomView;

        // expect a warning since 'model' component is missing
        LogAssert.Expect(LogType.Warning, "Model component not initialized.");

        roomController.HandleCompleteMinigame();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with 'model' component
    /// </summary>
    [Test]
    public void HandleCompleteMinigameValidLayersRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'controller' component
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        // assign mocks
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // expect no exception to be thrown since 'model' component is assigned
        Assert.DoesNotThrow(() => roomController.HandleCompleteMinigame(), "Expected no exceptions, but an exception was thrown on valid layers.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompleteMinigame() with 'model' and 'view' components
    /// </summary>
    [Test]
    public void HandleCompletionValidLayersRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'controller' component
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        // assign mocks
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // expect no exception to be thrown since 'view' and 'model' components were assigned
        Assert.DoesNotThrow(() => roomController.HandleCompletion(), "Expected no exceptions, but an exceptionw as thrown on valid layers.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompletion() with missing 'model' and 'view' components
    /// </summary>
    [Test]
    public void HandleCompletionMissingLayersRef()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'controller' component
        RoomController roomController = go.AddComponent<RoomController>();

        // expect a warning since 'model' component is missing
        LogAssert.Expect(LogType.Warning, "Model component not initialized.");

        roomController.HandleCompletion();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling HandleCompletion() with 'model' and 'view' components
    /// </summary>
    [Test]
    public void HandleCompletionEdgeCases()
    {
        // test if HandleCompletion() will work if only 'view' is assigned

        // create GameObject
        GameObject go = new GameObject();

        // add 'controller' component
        RoomController roomController = go.AddComponent<RoomController>();

        // substitute mocks
        IRoomView roomView = Substitute.For<IRoomView>();
        IRoomModel roomModel = Substitute.For<IRoomModel>();

        // assign view mock
        roomController.ViewMock = roomView;

        // expect a warning since 'model' component is missing
        LogAssert.Expect(LogType.Warning, "Model component not initialized.");

        roomController.HandleCompletion();

        // test if HandleCompletion() will work if only 'model' is assigned

        // destroy controller since 'ViewMock' cannot be assigned null
        UnityEngine.Object.DestroyImmediate(roomController);

        // add new 'controller' component
        roomController = go.AddComponent<RoomController>();

        // assign model mock
        roomController.ModelMock = roomModel;
        
        // expect a warning since 'view' component is missing
        LogAssert.Expect(LogType.Warning, "View component not initialized.");

        roomController.HandleCompletion();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }
}