using System;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.Assertions;

/// <summary>
/// Verify the logic of ToolTipController
/// It listens to hover events and shows/hides the interactive element
/// </summary>
public class ToolTipsControllerTests
{
    private GameObject interactiveElement;
    private IToolTipTrigger mockTrigger;
    private ToolTipController controller;

    /// <summary>
    ///  Set up a fake interactive element and a mock trigger
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        // create a GameObject
        interactiveElement = new GameObject("Interactive");
        mockTrigger = Substitute.For<IToolTipTrigger>();

        // Create the controller, passing the element and the mock trigger
        // The controller will subscribe to the trigger's events in its constructor
        controller = new ToolTipController(interactiveElement, mockTrigger);
    }
    
    /// <summary>
    /// Clean up after each test
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        // Unsubscribe and destroy the element
        controller.Dispose();
        UnityEngine.Object.DestroyImmediate(interactiveElement);
    }

    /// <summary>
    /// When the controller is created, it should hide the interactive element
    /// </summary>
    [Test]
    public void Constructor_DisablesElement()
    {
        // assert that the element starts disabled
        Assert.IsFalse(interactiveElement.activeSelf);
    }

    /// <summary>
    /// When the trigger raises a HoverEntered event, the controller should show the element
    /// </summary>
    [Test]
    public void OnHoverEnter_EnablesElement()
    {
        // Element should become visible
        mockTrigger.HoverEntered += Raise.Event<Action>();
        Assert.IsTrue(interactiveElement.activeSelf);
    }

    /// <summary>
    /// When the trigger raises a HoverExited event, the controller should hide the element
    /// </summary>
    [Test]
    public void OnHoverExit_DisablesElement()
    {
        // First raise HoverEntered to show the element, then HoverExited to hide it
        mockTrigger.HoverEntered += Raise.Event<Action>();
        Assert.IsTrue(interactiveElement.activeSelf);

        //now raise HoverExited and verify it hides the element again
        mockTrigger.HoverExited += Raise.Event<Action>();
        Assert.IsFalse(interactiveElement.activeSelf);
    }

    /// <summary>
    /// After disposing the controller, it should no longer respond to events
    /// Prevents memory leaks if the controller is destroyed while the trigger still exists
    /// </summary>
    [Test]
    public void Dispose_UnsubscribesFromEvents()
    {
        // dispose the controller
        controller.Dispose();

        // Now raise HoverEntered and verify it does NOT show the element (because we unsubscribed)
        mockTrigger.HoverEntered += Raise.Event<Action>();
        Assert.IsFalse(interactiveElement.activeSelf); // Should still be hidden.
    }

}