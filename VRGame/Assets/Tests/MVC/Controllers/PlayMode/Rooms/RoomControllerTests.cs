using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

/// <summary>
/// Unit tests for RoomController class.
/// </summary>
public class RoomControllerTests
{
    /// <summary>
    /// Init() should succeed when both RoomView and RoomModel are asigned.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomView roomView = go.AddComponent<RoomView>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        roomView.RoomController = roomController;

        // assign real components
        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        // allow Start() to run
        yield return null;

        // no errors should occur
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that Init() logs an error and asserts when RoomView is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator InitMissingRoomView()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign only model
        roomController.RoomModel = roomModel;

        // expect error + assert
        LogAssert.Expect(LogType.Assert, "One of roomView or roomViewMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomView.");
        LogAssert.Expect(LogType.Assert, "One of roomView or roomViewMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomView cannot be null.");

        // allow Start() to run
        yield return null;

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that Init() logs an error and asserts when RoomModel is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator InitMissingRoomModel()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomView roomView = go.AddComponent<RoomView>();

        roomView.RoomController = roomController;

        // assign real components
        roomController.RoomView = roomView;

        // expect error + assert
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomModel.");
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomModel cannot be null.");

        // allow Start() to run
        yield return null;

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompleteMinigame() throws when RoomModel is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompleteMinigameMissingModel()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomView roomView = go.AddComponent<RoomView>();

        roomView.RoomController = roomController;

        // assign real components
        roomController.RoomView = roomView;

        // expect error + assert
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomModel.");
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomModel cannot be null.");
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");

        yield return null;

        Assert.Throws<MissingFieldException>(() =>
        {
            roomController.HandleCompleteMinigame();
        }, "Expected exception, but no exception was thrown.");

        
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompleteMinigame() sets MinigameCompleted to true.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompleteMinigameValidLayers()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomView roomView = go.AddComponent<RoomView>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        roomView.RoomController = roomController;

        // assign real components
        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        yield return null;

        // call method
        roomController.HandleCompleteMinigame();

        // verify flag updated
        Assert.That(roomModel.MinigameCompleted, Is.True);

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompleteEducationalDialogue() throws when RoomModel is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompleteEducationalDialogueMissingModel()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomView roomView = go.AddComponent<RoomView>();

        roomView.RoomController = roomController;

        // assign real components
        roomController.RoomView = roomView;

        // expect error + assert
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomModel.");
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomModel cannot be null.");
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        
        yield return null;

        Assert.Throws<MissingFieldException>(() =>
        {
            roomController.HandleCompleteEducationalDialogue();
        }, "Expected exception, but no exception was thrown.");

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompleteEducationalDialogue() sets EducationalDialogueCompleted to true.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompleteEducationalDialogueValidLayers()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomView roomView = go.AddComponent<RoomView>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        roomView.RoomController = roomController;
        // assign real components
        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        yield return null;

        // call method
        roomController.HandleCompleteEducationalDialogue();

        // verify flag updated
        Assert.IsTrue(roomModel.EducationalDialogueCompleted);

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompletion() throws when RoomModel is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompletionMissingModel()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomView roomView = go.AddComponent<RoomView>();

        roomView.RoomController = roomController;

        // assign real components
        roomController.RoomView = roomView;

        // expect error + assert
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomModel.");
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomModel cannot be null.");
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");

        yield return null;

        // expect MissingFieldException
        Assert.Throws<MissingFieldException>(() =>
        {
            roomController.HandleCompletion();
        }, "Expected exception, but no exception was thrown.");

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompletion() throws when RoomView is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompletionMissingView()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign real components
        roomController.RoomModel = roomModel;

        // expect error + assert
        LogAssert.Expect(LogType.Assert, "One of roomView or roomViewMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomView.");
        LogAssert.Expect(LogType.Assert, "One of roomView or roomViewMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomView cannot be null.");
        LogAssert.Expect(LogType.Assert, "One of roomView or roomViewMock fields cannot be null.");

        yield return null;

        // expect MissingFieldException
        Assert.Throws<MissingFieldException>(() =>
        {
            roomController.HandleCompletion();
        }, "Expected exception, but no exception was thrown.");

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompletion() triggers the onRoomCompleted event when the room is fully done.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompletionInvokesEvent()
    {
        // create GameObject and add controller
        GameObject go = new GameObject();
        RoomController roomController = go.AddComponent<RoomController>();

        RoomView roomView = go.AddComponent<RoomView>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        roomView.RoomController = roomController;

        // assign real components
        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        yield return null;

        // mark model as complete
        roomModel.MinigameCompleted = true;
        roomModel.EducationalDialogueCompleted = true;

        var result = false;
        roomView.onRoomCompleted.AddListener(() =>
        {
            result = true;
        });

        // call method
        roomController.HandleCompletion();

        Assert.IsTrue(result, "Expected listeners to be invoked, but something went wrong.");

        UnityEngine.Object.DestroyImmediate(go);
    }
}
