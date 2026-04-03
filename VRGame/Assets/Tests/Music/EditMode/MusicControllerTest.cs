using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System;

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
        LogAssert.Expect(LogType.Error, "musicClip is not assigned");
        try
        {
            controller.Init();
        }
        catch {}

        GameObject go2 = new GameObject();
        MusicManagerController duplicate = go2.AddComponent<MusicManagerController>();

        LogAssert.Expect(LogType.Log, "MusicManagerController duplicate destroyed");
        duplicate.Init();

        UnityEngine.Object.DestroyImmediate(go2);
    }
}