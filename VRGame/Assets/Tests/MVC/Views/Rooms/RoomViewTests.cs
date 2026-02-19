using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;

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
        IRoomView roomView = null;

        // confirm that roomView is not null
        Assert.NotNull(roomView, $"roomView cannot be null. Got {roomView}");

        // initialize the component, no errors should occur
        roomView.Init();

        // free up memory
        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator PlayModeTest()
    {
        yield return null;
    }
}