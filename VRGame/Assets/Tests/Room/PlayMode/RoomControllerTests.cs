using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;

/// <summary>
/// Unit tests for RoomController class.
/// </summary>
public class RoomControllerTests
{
    /// <summary>
    /// Init() should succeed when both 'view' and 'model' are assigned.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view','model' and 'controller' components
        RoomController roomController = go.AddComponent<RoomController>();
        RoomView roomView = go.AddComponent<RoomView>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // allow Start() to run which invokes Init()
        yield return null;

        // no errors should occur since 'view' and 'model' are assigned

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that Init() logs an error and asserts when 'view' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator InitMissingRoomView()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' and 'controller' component
        RoomModel roomModel = go.AddComponent<RoomModel>();
        RoomController roomController = go.AddComponent<RoomController>();

        // assign real components
        roomController.ModelMock = roomModel;

        // expect exception to occur since 'view' was not assigned
        LogAssert.Expect(LogType.Exception, new Regex(".*'viewInstance' field cannot be null.*"));

        // allow Start() to run which invoked Init()
        yield return null;

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that Init() logs an error and asserts when 'model' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator InitMissingRoomModel()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' and 'controller' component
        RoomView roomView = go.AddComponent<RoomView>();
        RoomController roomController = go.AddComponent<RoomController>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;

        // expect exception to occur since 'model' was not assigned
        LogAssert.Expect(LogType.Exception, new Regex(".*'modelInstance' field cannot be null.*"));

        // allow Start() to run which invokes Init()
        yield return null;

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompleteMinigame() throws when 'model' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompleteMinigameMissingModel()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' and 'controller' component
        RoomController roomController = go.AddComponent<RoomController>();
        RoomView roomView = go.AddComponent<RoomView>();

        // assign real component only to 'view'
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;

        // expect exception to occur since 'model' was not assigned
        LogAssert.Expect(LogType.Exception, new Regex(".*'modelInstance' field cannot be null.*"));

        // allow Start() to run which invokes Init()
        yield return null;

        // expect a warning since 'model' component was not initialized
        LogAssert.Expect(LogType.Warning, "Model component not initialized.");

        roomController.HandleCompleteMinigame();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompleteMinigame() sets 'MinigameCompleted' field from 'model' component to true.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompleteMinigameValidLayers()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model', 'view', and 'controller' components
        RoomController roomController = go.AddComponent<RoomController>();
        RoomView roomView = go.AddComponent<RoomView>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // allow Start() to run which invokes Init()
        yield return null;

        // call method
        roomController.HandleCompleteMinigame();

        // verify 'model' updated
        Assert.That(roomModel.MinigameCompleted, Is.True);

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompleteEducationalDialogue() throws when 'model' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompleteEducationalDialogueMissingModel()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'controller' and 'view' components
        RoomController roomController = go.AddComponent<RoomController>();
        RoomView roomView = go.AddComponent<RoomView>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;

        // expect exception to occur since 'model' was not assigned
        LogAssert.Expect(LogType.Exception, new Regex(".*'modelInstance' field cannot be null.*"));
        
        // allow Start() to run which invokes Init()
        yield return null;

        // expect a warning since 'model' component was not initialized
        LogAssert.Expect(LogType.Warning, "Model component not initialized.");

        roomController.HandleCompleteEducationalDialogue();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompleteEducationalDialogue() sets 'EducationalDialogueCompleted' field from 'model' component to true.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompleteEducationalDialogueValidLayers()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view', 'model', and 'controller' components
        RoomController roomController = go.AddComponent<RoomController>();
        RoomView roomView = go.AddComponent<RoomView>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // allow Start() to run which invokes Init()
        yield return null;

        // call method
        roomController.HandleCompleteEducationalDialogue();

        // verify 'model' updated
        Assert.IsTrue(roomModel.EducationalDialogueCompleted, "'EducationalDialogueComplete' field was not set to true.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompletion() throws when 'model' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompletionMissingModel()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'view' and 'controller' components
        RoomController roomController = go.AddComponent<RoomController>();
        RoomView roomView = go.AddComponent<RoomView>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;

        // expect exception to occur since 'model' was not assigned
        LogAssert.Expect(LogType.Exception, new Regex(".*'modelInstance' field cannot be null.*"));

        // allow Start() to run which invokes Init()
        yield return null;

        // expect a warning since 'model' component was not initialized
        LogAssert.Expect(LogType.Warning, "Model component not initialized.");
        
        roomController.HandleCompletion();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompletion() throws when 'view' is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompletionMissingView()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' and 'controller' components
        RoomController roomController = go.AddComponent<RoomController>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign real components
        roomController.ModelMock = roomModel;

        // expect exception to occur since 'view' was not assigned
        LogAssert.Expect(LogType.Exception, new Regex(".*'viewInstance' field cannot be null.*"));

        // allow Start() to run which invokes Init()
        yield return null;

        // expect a warning since 'view' component was not initialized
        LogAssert.Expect(LogType.Warning, "View component not initialized.");

        roomController.HandleCompletion();

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Checks that HandleCompletion() triggers the 'onRoomCompleted' event when the room is fully done.
    /// </summary>
    [UnityTest]
    public IEnumerator HandleCompletionInvokesEvent()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model', 'view', and 'controller' components
        RoomController roomController = go.AddComponent<RoomController>();
        RoomView roomView = go.AddComponent<RoomView>();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign real components
        roomView.ControllerMock = roomController;
        roomController.ViewMock = roomView;
        roomController.ModelMock = roomModel;

        // allow Start() to run which invokes Init()
        yield return null;

        // mark 'model' as complete
        roomModel.MinigameCompleted = true;
        roomModel.EducationalDialogueCompleted = true;

        // add a listener that can be invoked to verify that it works
        var result = false;
        roomView.onRoomCompleted.AddListener(() =>
        {
            // update flag if invoked
            result = true;
        });

        // call method
        roomController.HandleCompletion();

        // verify flag updated
        Assert.IsTrue(result, "Expected listeners to be invoked, but something went wrong.");

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }
}
