using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System;
using System.ComponentModel.DataAnnotations;

public class Music
{
    
    GameObject go;
    MusicManagerController controller;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        controller = go.AddComponent<MusicManagerController>();
    }

    /// <summary>
    /// Called after each tests. Handles the clean up
    /// of game object.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        controller.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]
    public void SingletonTest()
    {
        // Initializing the first controller
        LogAssert.Expect(LogType.Error, "musicClip is not assigned");
        try
        {
            controller.Init();
        }
        catch {}

        // Creating a second controller and attempting to initialize it
        // It should log the duplicate message and destroy itself before reaching the check
        GameObject go2 = new GameObject();
        MusicManagerController duplicate = go2.AddComponent<MusicManagerController>();

        LogAssert.Expect(LogType.Log, "MusicManagerController duplicate destroyed");
        duplicate.Init();

        UnityEngine.Object.DestroyImmediate(go2);
    }

    [Test]
    public void NullClipTest()
    {
        // Calling Init() without assigning a music clip should log an error
        LogAssert.Expect(LogType.Error, "musicClip is not assigned");
        try
        {
            controller.Init();
            Assert.Fail("Null music clip should've triggered assertion");
        }
        catch
        { }
    }

    [Test]
    public void ResetInstanceTest()
    {
        // Initialize the first controller
        LogAssert.Expect(LogType.Error, "musicClip is not assigned");
        try
        {
            controller.Init();
        }
        catch { }

        // Reset the instance so a new controller can be initialized
        controller.ResetInstance();

        // Create a second controller and initialize it
        // ResetInstance should allow this to initialize without the duplicate log
        GameObject go2 = new GameObject();
        MusicManagerController newController = go2.AddComponent<MusicManagerController>();

        LogAssert.Expect(LogType.Error, "musicClip is not assigned");
        try
        {
            newController.Init();
        }
        catch { }

        newController.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go2);
    }
}