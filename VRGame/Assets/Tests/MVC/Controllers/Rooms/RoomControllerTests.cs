using System.Collections;
using System.Collections.Generic;
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
    /// Test the initialization of RoomController.
    /// </summary>
    [Test]
    public void Instantiation()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomController roomController = null;

        // confirm that roomController is not null
        Assert.NotNull(roomController, $"roomController cannot be null. Got {roomController}");

        // initialize the component. expected to throw assertion errors 
        // because of missing layer references.
        // the assertion thrown must have the exact same string
        // as these otherwise it wont detect it.
        LogAssert.Expect(LogType.Assert, "RoomController requires reference to RoomModel.");
        LogAssert.Expect(LogType.Assert, "RoomController requires reference RoomView.");
        roomController.Init();

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test the initialization of RoomController.
    /// </summary>
    [Test]
    public void HandleCompletion()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomController roomController = null;

        roomController.Init();

        // expected to throw assertion errors because of missing layer references.
        // the assertion thrown must have the exact same string
        // as these otherwise it wont detect it.
        LogAssert.Expect(LogType.Assert, "RoomController requires reference to RoomModel.");
        LogAssert.Expect(LogType.Assert, "RoomController requires reference RoomView.");
        roomController.HandleCompletion();

        // free up memory
        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator Initialization()
    {
        /// TODO: I should make this once all the layers are done.
        yield return null;
    }
}