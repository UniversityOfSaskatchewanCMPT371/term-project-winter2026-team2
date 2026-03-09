using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.Diagnostics;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

/// <summary>
/// Unit tests for RoomView class.
/// </summary>
public class RoomViewTests
{
    /// <summary>
    /// Init() should succeed when 'controller' is assigned.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' and 'controller' components
        RoomView roomView = go.AddComponent<RoomView>();
        RoomController roomController = go.AddComponent<RoomController>();

        // controller still needs a 'model', so Init() will complain about missing 'model'
        LogAssert.Expect(LogType.Exception, new Regex(".*'modelInstance' field cannot be null.*"));

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;

        // allow Start() to run which invokes Init()
        yield return null;

        // no errors should occur since 'controller' is assigned

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Init() should log an error and assert when 'controller' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator InitMissingController()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // expect exception to occur since 'controller' was not assigned
        LogAssert.Expect(LogType.Exception, new Regex(".*'controllerInstance' field cannot be null.*"));

        // allow Start() to run which invokes Init()
        yield return null;

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// InvokeOnRoomComplete() should call all listeners.
    /// </summary>
    [UnityTest]
    public IEnumerator InvokeOnRoomComplete()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // Init() will still complain about missing controller
        LogAssert.Expect(LogType.Exception, new Regex(".*'controllerInstance' field cannot be null.*"));

        // track if event was called
        bool called = false;
        roomView.onRoomCompleted.AddListener(() => called = true);

        // manually invoke event
        roomView.InvokeOnRoomComplete();

        // verify listener was called
        Assert.IsTrue(called);

        // allow Start() to run
        yield return null;
        
        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// MinigameCompleted() should throw when 'controller' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator MinigameCompletedMissingController()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // Init() will complain about missing controller
        LogAssert.Expect(LogType.Exception, new Regex(".*'controllerInstance' field cannot be null.*"));

        // allow Start() to run which invoked Init()
        yield return null;

        // expect an exception to be thrown since MinigameCompleted() requires 'controller' component
        Assert.Throws<AssertionException>(() =>
        {
            roomView.MinigameCompleted();
        });

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// MinigameCompleted() should call HandleCompleteMinigame() on the 'controller'.
    /// </summary>
    [UnityTest]
    public IEnumerator MinigameCompletedCallsControllerMethod()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view', 'controller', and 'model' components
        RoomView roomView = go.AddComponent<RoomView>();
        RoomController roomController = go.AddComponent<RoomController>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // allow Start() to run which invoked Init()
        yield return null;

        // call method
        roomView.MinigameCompleted();

        // verify 'model' updated
        Assert.IsTrue(roomModel.MinigameCompleted, "'MinigameCompleted' field was not set to true.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// EducationalDialoguesCompleted() should throw when 'controller' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator EducationalDialoguesCompletedMissingController()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' component
        RoomView roomView = go.AddComponent<RoomView>();

        // Init() will complain about missing controller
        LogAssert.Expect(LogType.Exception, new Regex(".*'controllerInstance' field cannot be null.*"));

        // allow Start() to run which invoked Init()
        yield return null;

        // expect an exception to be thrown since EducationDialguesCompleted() requires 'controller' component
        Assert.Throws<AssertionException>(() =>
        {
            roomView.EducationalDialoguesCompleted();
        });

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// EducationalDialoguesCompleted() should call HandleCompleteMinigame()
    /// </summary>
    [UnityTest]
    public IEnumerator EducationalDialoguesCompletedCallsControllerMethod()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view', 'controller', and 'model' components
        RoomView roomView = go.AddComponent<RoomView>();
        RoomController roomController = go.AddComponent<RoomController>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // allow Start() to run which invokes Init()
        yield return null;

        // call method
        roomView.EducationalDialoguesCompleted();

        // verify 'model' updated
        Assert.IsTrue(roomModel.EducationalDialogueCompleted, "'Expected Model's 'EducationalDialogueCompleted' to be True, but got False.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }
}