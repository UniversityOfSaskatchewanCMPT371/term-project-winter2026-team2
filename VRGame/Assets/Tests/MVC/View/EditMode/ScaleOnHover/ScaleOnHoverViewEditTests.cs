using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleOnHoverViewEditTests
{
    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject("TestObject");
        ScaleOnHoverView view = go.AddComponent<ScaleOnHoverView>();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject go = GameObject.Find("TestObject");
        if (go != null)
        {
            Object.DestroyImmediate(go);
        }
    }

    /// <summary>
    /// Tests that the Start method initializes the controller reference and sets up the hover events correctly.
    /// </summary>
    [Test]
    public void Start_InitializesViewAndValidatesController()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        ScaleOnHoverView view = go.GetComponent<ScaleOnHoverView>();
        var xrInteractable = go.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
        var mockController = Substitute.For<IScaleOnHoverController>();
        view.controller = mockController;
        // Act
        view.Start();

        // Assert
        Assert.IsNotNull(view.controller, "Controller reference should not be null after Start");
        // Checking that the hover events are set up correctly by invoking them and verifying the
        // controller methods are called
        xrInteractable.hoverEntered.Invoke(new HoverEnterEventArgs());
        mockController.Received(1).OnHoverEnter();

        xrInteractable.hoverExited.Invoke(new HoverExitEventArgs());
        mockController.Received(1).OnHoverExit();

    }
}
