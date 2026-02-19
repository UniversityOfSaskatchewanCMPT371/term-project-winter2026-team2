using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;
using System;

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
        IRoomView roomView = go.AddComponent<RoomView>();

        // confirm that roomView is not null
        Assert.NotNull(roomView, $"roomView cannot be null. Got {roomView}");

        // initialize the component, no errors should occur
        roomView.Init();

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test to see if MinigameCompleted() will throw an error
    /// with missing controller reference.
    /// </summary>
    [Test]
    public void MinigameCompleted()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomView roomView = go.AddComponent<RoomView>();

        // initialize the component, no errors should occur
        roomView.Init();

        // should throw an exception since reference to controller is missing.
        Assert.Throws<MissingFieldException>(() => roomView.MinigameCompleted(), "Missing RoomController field.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test to see if EducationalDialoguesCompleted() will throw an error
    /// with missing controller reference.
    /// </summary>
    [Test]
    public void EducationalDialoguesCompleted()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomView roomView = go.AddComponent<RoomView>();

        // initialize the component, no errors should occur
        roomView.Init();

        // should throw an exception since reference to controller is missing.
        Assert.Throws<MissingFieldException>(() => roomView.EducationalDialoguesCompleted(), "Missing RoomController field.");

        // free up memory
        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator Initialization()
    {
        /// TODO: I should make this once all the layers are done.
        yield return null;
    }
}