using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System;

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

    // A Test behaves as an ordinary method
    [Test]
    public void Constructor_DisablesInteractiveElement()
    {
        Assert.IsFalse(interactiveElement.activeSelf);
    }

    [Test]
    public void OnHoverEnter_EnablesInteractiveElement()
    {
        //raise hoverentered event on the mock
        trigger.HoverEntered += Raise.Event<Action>();
        Assert.IsTrue(interactiveElement.activeSelf);
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
