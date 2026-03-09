using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using System;

/// <summary>
/// Unit tests for RoomModel class.
/// </summary>
public class RoomModelTests
{
    /// <summary>
    /// Test the initialization of 'model' with default preset.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // allow Start() to run which invokes Init()
        yield return null;

        // no errors should occur

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test that Init() fails when the room name is whitespace.
    /// </summary>
    [UnityTest]
    public IEnumerator InitNameIsWhitespace()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // expect errors and assertions to occur since 'roomName' field cannot have whitespace as a value
        LogAssert.Expect(LogType.Error, "'value' is exclusively whitespace.");
        LogAssert.Expect(LogType.Assert,"'value' cannot be exclusively whitespace.");
        LogAssert.Expect(LogType.Error, "Field 'roomName' is exclusively whitespace.");
        LogAssert.Expect(LogType.Assert, "Field 'roomName' cannot be exclusively whitespace.");

        // set 'roomName' field value to only whitespace
        roomModel.Name = "   ";

        // allow Start() to run which invokes Init()
        yield return null;

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test that Init() fails when 'minigameCompleted' starts as true.
    /// </summary>
    [UnityTest]
    public IEnumerator InitMinigameCompletedStartsTrue()
    {
        // creat GameObject
        GameObject go = new GameObject();

        // add 'model' component
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // initialize 'MinigameCompleted' field with true
        roomModel.MinigameCompleted = true;

        // expect error and assertion to occur since 'MinigameCompleted' field cannot be initialized to true

        LogAssert.Expect(LogType.Error, "Field 'minigameCompleted' must start as false.");
        LogAssert.Expect(LogType.Assert, "Field 'minigameCompleted' must be set to false.");

        // allow Start() to run which invokes Init()
        yield return null;

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test that Init() fails when 'educationalDialogueCompleted' starts as true.
    /// </summary>
    [UnityTest]
    public IEnumerator InitEducationalDialogueCompletedStartsTrue()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // initialize 'EducationalDialogueCompleted' field with true
        roomModel.EducationalDialogueCompleted = true;

        // expect error and assertion to occur since 'eductionalDialogueCompleted' field cannot be initialized to true
        LogAssert.Expect(LogType.Error, "Field 'eductionalDialogueCompleted' must start as false.");
        LogAssert.Expect(LogType.Assert, "Field 'eductionalDialogueCompleted' must be set to false.");

        // allow Start() to run which invoked Init()
        yield return null;

        // clean up game object
        UnityEngine.Object.DestroyImmediate(go);
    }
}