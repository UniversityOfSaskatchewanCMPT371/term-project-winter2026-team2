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
        XRGrabInteractable xrInteractable = go.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
        IScaleOnHoverController mockController = Substitute.For<IScaleOnHoverController>();
        view.controller = mockController;
        // Act
        view.Start();

        // Assert
        Assert.IsNotNull(view.controller, "Controller reference should not be null after Start");
        // Checking that the hover events are set up correctly by invoking them and verifying the
        // controller methods are called
        HoverEnterEventArgs hoverEnterArgs = new HoverEnterEventArgs();
        hoverEnterArgs.interactorObject = go.AddComponent<XRDirectInteractor>();
        xrInteractable.hoverEntered.Invoke(hoverEnterArgs);
        mockController.Received(1).OnHoverEnter();

        HoverExitEventArgs hoverExitArgs = new HoverExitEventArgs();
        hoverExitArgs.interactorObject = go.AddComponent<XRDirectInteractor>();
        xrInteractable.hoverExited.Invoke(new HoverExitEventArgs());
        mockController.Received(1).OnHoverExit();

    }

    /// <summary>
    /// Tests that the Start method causes an Assertion to fail if there is no controller.
    /// </summary>
    [Test]
    public void Start_MissingController()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        ScaleOnHoverView view = go.GetComponent<ScaleOnHoverView>();
        XRGrabInteractable xrInteractable = go.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
        
        // Act
        Assert.Throws<UnityEngine.Assertions.AssertionException>(() => view.Start());

        // Assert
        // Nothing to assert here since Start should fail due to missing controller reference
    }

    /// <summary>
    /// Tests that the Start method causes an Assertion to fail if there is no XRGrabInteractable.
    /// </summary>
    [Test]
    public void Start_MissingXRInteractable()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        ScaleOnHoverView view = go.GetComponent<ScaleOnHoverView>();
        IScaleOnHoverController mockController = Substitute.For<IScaleOnHoverController>();
        view.controller = mockController;

        // Act
        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, "XRBaseInteractable component is required for XR hover events to work");
        view.Start();

        // Assert
        // Nothing to assert here since Start should fail due to missing XRGrabInteractable component and log an error
    }

    /// <summary>
    /// Tests that the OnHoverEnter method calls the controller's OnHoverEnter method when the controller reference is valid.
    /// </summary>
    [Test]
    public void OnHoverEnter_ControllerExists_CallsControllerMethod()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        ScaleOnHoverView view = go.GetComponent<ScaleOnHoverView>();

        IScaleOnHoverController mockController = Substitute.For<IScaleOnHoverController>();
        view.controller = mockController;

        // Act
        view.OnHoverEnter();

        // Assert
        mockController.Received(1).OnHoverEnter();
    }

    /// <summary>
    ///  Tests that the OnHoverEnter method logs an error and does not call the controller's OnHoverEnter method when the controller reference is null.
    /// </summary>
    [Test]
    public void OnHoverEnter_ControllerIsNull_LogsErrorAndDoesNotCallController()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        ScaleOnHoverView view = go.GetComponent<ScaleOnHoverView>();

        view.controller = null;

        // Act
        LogAssert.Expect(LogType.Error, "ScaleOnHoverController reference cannot be null");
        view.OnHoverEnter();

        // Assert
    }

    /// <summary>
    /// Tests that the OnHoverEnter method calls the controller's OnHoverEnter method when the controller reference is valid.
    /// </summary>
    [Test]
    public void OnHoverExit_ControllerExists_CallsControllerMethod()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        ScaleOnHoverView view = go.GetComponent<ScaleOnHoverView>();

        IScaleOnHoverController mockController = Substitute.For<IScaleOnHoverController>();
        view.controller = mockController;

        // Act
        view.OnHoverExit();

        // Assert
        mockController.Received(1).OnHoverExit();
    }

    /// <summary>
    /// Tests that the OnHoverExit method logs an error and does not call the controller's OnHoverExit method when the controller reference is null.
    /// </summary>
    [Test]
    public void OnHoverExit_ControllerIsNull_LogsErrorAndDoesNotCallController()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        ScaleOnHoverView view = go.GetComponent<ScaleOnHoverView>();

        view.controller = null;

        // Act
        LogAssert.Expect(LogType.Error, "ScaleOnHoverController reference is null in OnHoverExit");
        view.OnHoverExit();

        // Assert
    }


    /// <summary>
    /// Tests that the OnXRHoverEnter method calls the controller's OnHoverEnter method 
    /// </summary>
    [Test]
    public void OnXRHoverEnter_ControllerExists_TriggersOnHoverEnter()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        ScaleOnHoverView view = go.GetComponent<ScaleOnHoverView>();

        IScaleOnHoverController mockController = Substitute.For<IScaleOnHoverController>();
        view.controller = mockController;

        HoverEnterEventArgs hoverEnterArgs = new HoverEnterEventArgs();
        hoverEnterArgs.interactorObject = go.AddComponent<XRDirectInteractor>();

        // Act
        view.OnXRHoverEnter(hoverEnterArgs);

        // Assert
        mockController.Received(1).OnHoverEnter();
    }

    /// <summary>
    /// Make sure that if the controller reference is null, an error is logged and the controller's OnHoverEnter method is not called when OnXRHoverEnter is triggered.
    /// </summary>
    [Test]
    public void OnXRHoverEnter_ControllerIsNull_LogsErrorAndDoesNotCallController()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        var view = go.GetComponent<ScaleOnHoverView>();

        view.controller = null;

        HoverEnterEventArgs hoverEnterArgs = new HoverEnterEventArgs();
        hoverEnterArgs.interactorObject = go.AddComponent<XRDirectInteractor>();

        // Act
        LogAssert.Expect(LogType.Error, "ScaleOnHoverController reference cannot be null in OnXRHoverEnter");
        view.OnXRHoverEnter(hoverEnterArgs);
        // Assert
        // LogAssert validates the assertion failure
    }


    /// <summary>
    /// Check that the OnXRHoverExit method calls the controller's OnHoverExit method when the controller reference is valid.
    /// </summary>
    [Test]
    public void OnXRHoverExit_ControllerExists_TriggersOnHoverExit()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        var view = go.GetComponent<ScaleOnHoverView>();

        var mockController = Substitute.For<IScaleOnHoverController>();
        view.controller = mockController;

        var args = new HoverExitEventArgs();

        // Act
        view.OnXRHoverExit(args);

        // Assert
        mockController.Received(1).OnHoverExit();
    }

    /// <summary>
    /// Test that if the controller reference is null, an error is logged and the controller's OnHoverExit method is not called when OnXRHoverExit is triggered.
    /// </summary>
    [Test]
    public void OnXRHoverExit_ControllerIsNull_LogsErrorAndDoesNotCallController()
    {
        // Arrange
        GameObject go = GameObject.Find("TestObject");
        var view = go.GetComponent<ScaleOnHoverView>();

        view.controller = null;

        var args = new HoverExitEventArgs();

        // Act
        Assert.Throws<UnityEngine.Assertions.AssertionException>(() => view.OnXRHoverExit(args));
        

        // Assert
        // LogAssert validates the assertion failure
    }

}
