using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.Diagnostics;
using UnityEngine;
using System;


/// <summary>
/// Unit tests for RoomView class.
/// </summary>
public class RoomViewTests
{
    /// <summary>
    /// Init() should succeed when RoomController is assigned.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        // create GameObject and add components
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();
        RoomController roomController = go.AddComponent<RoomController>();

        // controller still needs a model, so Init() will complain about missing model
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomModel.");
        LogAssert.Expect(LogType.Assert, "One of roomModel or roomModelMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomModel cannot be null.");


        // assign controller to view
        roomView.RoomController = roomController;
        roomController.RoomView = roomView;

        // allow Start() to run
        yield return null;

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Init() should log an error and assert when RoomController is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator InitMissingController()
    {
        // create GameObject and add view only
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        // expect missing controller errors
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomController.");
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomController cannot be null.");

        // allow Start() to run
        yield return null;

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// InvokeOnRoomComplete() should call all listeners.
    /// </summary>
    [UnityTest]
    public IEnumerator InvokeOnRoomComplete()
    {
        // create GameObject and add view
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        // Init() will still complain about missing controller
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomController.");
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomController cannot be null.");

        // track if event was called
        bool called = false;
        roomView.onRoomCompleted.AddListener(() => called = true);

        // manually invoke event
        roomView.InvokeOnRoomComplete();

        // verify listener was called
        Assert.IsTrue(called);

        yield return null;
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// MinigameCompleted() should throw when RoomController is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator MinigameCompletedMissingController()
    {
        // create GameObject and add view
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        // Init() will complain about missing controller
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomController.");
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomController cannot be null.");
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");

        yield return null;

        // method should throw
        Assert.Throws<MissingFieldException>(() =>
        {
            roomView.MinigameCompleted();
        });

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// MinigameCompleted() should call HandleCompleteMinigame() on the controller.
    /// </summary>
    [UnityTest]
    public IEnumerator MinigameCompletedCallsControllerMethod()
    {
        // create GameObject and add components
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();
        RoomController roomController = go.AddComponent<RoomController>();

        // assign controller to view
        roomView.RoomController = roomController;

        // controller needs model + view to avoid its own Init() errors
        RoomModel roomModel = go.AddComponent<RoomModel>();
        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        yield return null;

        // call method
        roomView.MinigameCompleted();

        // verify model updated
        Assert.IsTrue(roomModel.MinigameCompleted);

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// EducationalDialoguesCompleted() should throw when RoomController is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator EducationalDialoguesCompletedMissingController()
    {
        // create GameObject and add view
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();

        // Init() will complain about missing controller
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");
        LogAssert.Expect(LogType.Error, "Missing field roomController.");
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");
        LogAssert.Expect(LogType.Assert, "Field roomController cannot be null.");
        LogAssert.Expect(LogType.Assert, "One of roomController or roomControllerMock fields cannot be null.");

        yield return null;

        // method should throw
        Assert.Throws<MissingFieldException>(() =>
        {
            roomView.EducationalDialoguesCompleted();
        });

        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// EducationalDialoguesCompleted() should call HandleCompleteMinigame()
    /// </summary>
    [UnityTest]
    public IEnumerator EducationalDialoguesCompletedCallsControllerMethod()
    {
        // create GameObject and add components
        GameObject go = new GameObject();
        RoomView roomView = go.AddComponent<RoomView>();
        RoomController roomController = go.AddComponent<RoomController>();

        // assign controller to view
        roomView.RoomController = roomController;

        // controller needs model + view
        RoomModel roomModel = go.AddComponent<RoomModel>();
        roomController.RoomView = roomView;
        roomController.RoomModel = roomModel;

        yield return null;

        // call method
        roomView.EducationalDialoguesCompleted();

        // verify model updated
        Assert.IsTrue(roomModel.MinigameCompleted);

        UnityEngine.Object.DestroyImmediate(go);
    }
}