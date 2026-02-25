using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System;

/// <summary>
/// Edit Mode tests for ToolTipController. 
/// To verify that the controller correctly enables and 
/// disables the interactive element based on hover events, 
/// and that it properly unsubscribes from events when disposed. 
/// </summary>
public class ToolTipControllerTests
{

    private GameObject interactiveElement;
    private IToolTipTrigger trigger;
    private ToolTipController toolTipController;

    [SetUp]
    public void SetUp()
    {
        interactiveElement = new GameObject("InteractiveElement");
        trigger = Substitute.For<IToolTipTrigger>();
        toolTipController = new ToolTipController(interactiveElement, trigger);
    }

    [TearDown]
    public void TearDown()
    {
        toolTipController.Dispose();
        UnityEngine.Object.DestroyImmediate(interactiveElement);
    }

    /// <summary>
    /// Tests that the constructor of ToolTipController 
    /// correctly disables the interactive element at start
    /// </summary>
    [Test]
    public void Constructor_DisablesInteractiveElement()
    {
        Assert.IsFalse(interactiveElement.activeSelf);
    }

    /// <summary>
    /// Tests that when the HoverEntered event is raised on the trigger,
    /// the interactive element is enabled.
    [Test]
    public void OnHoverEnter_EnablesInteractiveElement()
    {
        //raise hoverentered event on the mock
        trigger.HoverEntered += Raise.Event<Action>();
        Assert.IsTrue(interactiveElement.activeSelf);
    }

    /// <summary> 
    /// Tests that when the HoverExited event is raised on the trigger,
    /// the interactive element is disabled.
    /// </summary>
    [Test]
    public void OnHoverExit_DisablesInteractiveElement()
    {
        trigger.HoverEntered += Raise.Event<Action>(); //first enable it
        Assert.IsTrue(interactiveElement.activeSelf); //check it's enabled

        trigger.HoverExited += Raise.Event<Action>(); //then raise hover exit
        Assert.IsFalse(interactiveElement.activeSelf); //check it's disabled
    }

    /// <summary>
    /// Tests that when the controller is disposed, 
    /// it unsubscribes from the trigger's events,
    /// ensuring that subsequent events do not affect the state 
    /// of the interactive element.
    /// </summary>
    [Test]
    public void Dispose_UnsubscribesFromEvents()
    {
        toolTipController.Dispose();

        //after disposing, raising events should not change the state
        trigger.HoverEntered += Raise.Event<Action>();
        Assert.IsFalse(interactiveElement.activeSelf);

        trigger.HoverExited += Raise.Event<Action>();
        Assert.IsFalse(interactiveElement.activeSelf);
    }
    

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
/*     [UnityTest]
    public IEnumerator ToolTipControllerTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    } */
}
