using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class PlayerServiceControllerTests
{
    GameObject go;
    PlayerServiceController controller;

    /// <summary>
    /// Called once before every test functions. 
    /// Handles setting up the test environment.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        go = new GameObject();
        controller = go.AddComponent<PlayerServiceController>();
    }

    /// <summary>
    /// Called once after every test functions.
    /// Handles cleanning up the test environment.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        controller.ResetStatic();
        UnityEngine.Object.DestroyImmediate(go);
    }

    [Test]
    public void Instantiation()
    {
        // expect errors to occur since 'XRrigPrefab' variable is null
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' variable was not set in inspector.");
        Assert.Throws<AssertionException>(() => controller.Init(), "Expected exception to be thrown.");
    }

    [Test]
    public void SetXRPrefabNull()
    {
        // mock the xr rig WITHOUT player controller component
        GameObject XRrigMock = new GameObject();

        // expect errors to occur since xr rig does not contain player controller component
        LogAssert.Expect(LogType.Error, "'value' is null.");
        Assert.Throws<AssertionException>(() => {controller.MockXRrigPrefab = null;}, "Expected exception to be thrown.");
    }

    [Test]
    public void SetXRPrefab()
    {
        // mock the xr rig WITHOUT player controller component
        GameObject XRrigMock = new GameObject();

        // expect errors to occur since xr rig does not contain player controller component
        LogAssert.Expect(LogType.Error, "'XRrigPrefab' does not have PlayerController component attached.");
        Assert.Throws<AssertionException>(() => {controller.MockXRrigPrefab = XRrigMock;}, "Expected exception to be thrown.");

    }

    [Test]
    public void SetXRPrefabWithPlayerController()
    {
        // mock the xr rig WITHOUT player controller component
        GameObject XRrigMock = new GameObject();
        XRrigMock.AddComponent<PlayerController>();
        Assert.DoesNotThrow(() => {controller.MockXRrigPrefab = XRrigMock;}, "No exception expected.");

        // destroy the xr rig
        UnityEngine.Object.DestroyImmediate(XRrigMock);
    }

    [Test]
    public void GetXRPrefab()
    {
        // expect null since it was not set
        Assert.IsNull(controller.MockXRrigPrefab, "Expect value to be null since it was not set.");
    }

    [Test]
    public void SpawnPlayer()
    {
        // mock the xr rig with player controller component
        GameObject XRrigMock = new GameObject();
        XRrigMock.AddComponent<PlayerController>();
        Assert.DoesNotThrow(() => {controller.MockXRrigPrefab = XRrigMock;}, "No exception expected.");

        // spawn the player.
        // expect an exception to occur since OnLoadDontDestroy() only works at runtime
        Assert.Throws<InvalidOperationException>(() => controller.SpawnPlayer(Vector3.zero, new Quaternion()), "Expected exception to be thrown.");

        // destroy the xr rig
        UnityEngine.Object.DestroyImmediate(XRrigMock);
    }

    [Test]
    public void SpawnPlayerOnLoadEnabled()
    {
        // mock the xr rig with player controller component
        GameObject XRrigMock = new GameObject();
        XRrigMock.AddComponent<PlayerController>();
        Assert.DoesNotThrow(() => {controller.MockXRrigPrefab = XRrigMock;}, "No exception expected.");

        // expect an exception to occur since OnLoadDontDestroy() only works at runtime
        Assert.Throws<InvalidOperationException>(() => controller.Init(), "Expected exception to be thrown.");

        Assert.IsNotNull(controller.playerObjAccessor, "Player did not spawn.");
    }

    [Test]
    public void SpawnPlayerOnLoadDisabled()
    {
        // mock the xr rig with player controller component
        GameObject XRrigMock = new GameObject();
        XRrigMock.AddComponent<PlayerController>();
        Assert.DoesNotThrow(() => {controller.MockXRrigPrefab = XRrigMock;}, "No exception expected.");

        // don't spawn the player when this component is initialized
        controller.spawnPlayerOnLoad = false;

        // expect an exception to occur since OnLoadDontDestroy() only works at runtime
        Assert.DoesNotThrow(() => controller.Init(), "No exception expected.");

        // there should be no rig spawned
        Assert.IsNull(controller.playerObjAccessor, "Player spawned.");
    }
}