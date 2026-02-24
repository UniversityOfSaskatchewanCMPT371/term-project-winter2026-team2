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
    /// Test the initialization of RoomModel with valid presets.
    /// </summary>
    [UnityTest]
    public IEnumerator Instantiation()
    {
        // create GameObject and add component
        GameObject go = new GameObject();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        // assign valid values
        roomModel.Name = "Test";
        roomModel.Id = 1;
        roomModel.MinigameCompleted = false;
        roomModel.EducationalDialogueCompleted = false;        

        // skip one frame to allow Init() to run
        yield return null;

        // no errors should occur
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test that Init() fails when the room ID is already taken.
    /// </summary>
    [UnityTest]
    public IEnumerator InitIdAlreadyTaken()
    {
        // create first room
        GameObject go1 = new GameObject();
        RoomModel roomModel1 = go1.AddComponent<RoomModel>();
        roomModel1.Name = "TestA";
        roomModel1.Id = 1;
        roomModel1.MinigameCompleted = false;
        roomModel1.EducationalDialogueCompleted = false;

        // allow Init() to run
        yield return null;

        // create second room with same id
        GameObject go2 = new GameObject();
        RoomModel roomModel2 = go2.AddComponent<RoomModel>();
        roomModel2.Name = "TestB";
        roomModel2.Id = 1;
        roomModel2.MinigameCompleted = false;
        roomModel2.EducationalDialogueCompleted = false;

        // expect error log + assert log
        LogAssert.Expect(LogType.Error, "Field roomId is already taken.");
        LogAssert.Expect(LogType.Assert, "Field roomId must be set to a different id.");

        // allow Init() to run
        yield return null;

        // cleanup
        UnityEngine.Object.DestroyImmediate(go1);
        UnityEngine.Object.DestroyImmediate(go2);
    }

    /// <summary>
    /// Test that Init() fails when the room name is whitespace.
    /// </summary>
    [UnityTest]
    public IEnumerator InitNameIsWhitespace()
    {
        // create room with whitespace name
        GameObject go = new GameObject();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        roomModel.Name = "   ";
        roomModel.Id = 1;
        roomModel.MinigameCompleted = false;
        roomModel.EducationalDialogueCompleted = false;

        // expect error log + assert log
        LogAssert.Expect(LogType.Assert,"Value cannot be whitespace.");
        LogAssert.Expect(LogType.Error, "Field roomName cannot be whitespace.");
        LogAssert.Expect(LogType.Assert, "Field roomName must be set to a different name.");

        // allow Init() to run
        yield return null;

        // cleanup
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test that Init() fails when minigameCompleted starts as true.
    /// </summary>
    [UnityTest]
    public IEnumerator InitMinigameCompletedStartsTrue()
    {
        // create room with invalid minigameCompleted value
        GameObject go = new GameObject();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        roomModel.Name = "Test";
        roomModel.Id = 1;
        roomModel.MinigameCompleted = true;
        roomModel.EducationalDialogueCompleted = false;

        // expect error log + assert log
        LogAssert.Expect(LogType.Error, "Field minigameCompleted must start as false.");
        LogAssert.Expect(LogType.Assert, "Field minigameCompleted must be set to false.");

        // allow Init() to run
        yield return null;

        // cleanup
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test that Init() fails when educationalDialogueCompleted starts as true.
    /// </summary>
    [UnityTest]
    public IEnumerator InitEducationalDialogueCompletedStartsTrue()
    {
        // create room with invalid educationalDialogueCompleted value
        GameObject go = new GameObject();
        RoomModel roomModel = go.AddComponent<RoomModel>();

        roomModel.Name = "Test";
        roomModel.Id = 1;
        roomModel.MinigameCompleted = false;
        roomModel.EducationalDialogueCompleted = true;

        // expect error log + assert log
        LogAssert.Expect(LogType.Error, "Field eductionalDialogueCompleted must start as false.");
        LogAssert.Expect(LogType.Assert, "Field eductionalDialogueCompleted must be set to false.");

        // allow Init() to run
        yield return null;

        // cleanup
        UnityEngine.Object.DestroyImmediate(go);
    }
}