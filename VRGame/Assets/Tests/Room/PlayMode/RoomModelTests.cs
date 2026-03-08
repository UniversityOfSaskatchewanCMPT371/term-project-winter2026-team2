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
    /// Test that Init() fails when the 'roomId' is already taken.
    /// </summary>
    [UnityTest]
    public IEnumerator InitIdAlreadyTaken()
    {
        // create first room
        GameObject go1 = new GameObject();

        // add 'model' component to first room
        RoomModel roomModel1 = go1.AddComponent<RoomModel>();

        // setup first room
        roomModel1.Name = "TestA";
        roomModel1.Id = 1;
        roomModel1.MinigameCompleted = false;
        roomModel1.EducationalDialogueCompleted = false;

        // allow Start() to run which invokes Init()
        yield return null;

        // create second room with same id
        GameObject go2 = new GameObject();
        
        // setup second room WaitHandle same 'roomId'
        RoomModel roomModel2 = go2.AddComponent<RoomModel>();
        roomModel2.Name = "TestB";
        roomModel2.Id = 1;
        roomModel2.MinigameCompleted = false;
        roomModel2.EducationalDialogueCompleted = false;

        // expect error and assertion to occur since two rooms have the same id
        LogAssert.Expect(LogType.Error, "Field 'roomId' is already taken.");
        LogAssert.Expect(LogType.Assert, "Field 'roomId' must be set to a different id.");

        // allow Start() to run which invokes Init()
        yield return null;

        // clean up game objects
        UnityEngine.Object.DestroyImmediate(go1);
        UnityEngine.Object.DestroyImmediate(go2);
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

        roomModel.Name = "   ";

        // expect errors and assertions to occur since 'roomName' field cannot have whitespace as a value
        LogAssert.Expect(LogType.Assert,"'value' cannot be whitespace.");
        LogAssert.Expect(LogType.Error, "Field 'roomName' cannot be exclusively whitespace.");
        LogAssert.Expect(LogType.Assert, "Field 'roomName' must be set to a different name.");

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