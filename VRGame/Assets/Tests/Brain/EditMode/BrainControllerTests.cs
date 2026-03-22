using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using FsCheck;
using FsCheck.Fluent;

public class BrainControllerTests
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;

    /// <summary>
    /// The component that is being tested.
    /// </summary>
    BrainController controller;

    IBrainModel mockModel;
    IBrainView mockView;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        controller = go.AddComponent<BrainController>();
        mockModel = Substitute.For<IBrainModel>();
        mockView = Substitute.For<IBrainView>();

        controller.ModelMock = mockModel;
        controller.ViewMock = mockView;

        
    }

    /// <summary>
    /// Called after each tests. Handles the clean up
    /// of game object.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Property based test to verify that OnHoverEnter() make hoverCount increment by 1.
    /// Property checked: n=hoverCount; OnHoverEnter(); hoverCount == n + 1
    /// </summary>
    [Test]
    public void OnHoverEnterIncrements()
    {
        Prop.ForAll<NonNegativeInt>(count =>
        {
            controller.hoverCount = count.Get;
            controller.OnHoverEnter();
            return controller.hoverCount == count.Get + 1;
        }).QuickCheckThrowOnFailure();
    }

    /// <summary>
    /// Property based test to verify that OnHoverExit() make hoverCount decrement by 1 unless hoverCount is already 0.
    /// Property checked: n=hoverCount; OnHoverExit(); if n == 0 then hoverCount == 0 else hoverCount == n - 1
    /// </summary>
    [Test]
    public void OnHoverExitDecrementsUnlessZero()
    {
        Prop.ForAll<NonNegativeInt>(count =>
        {
            controller.hoverCount = count.Get;
            controller.OnHoverExit();

            if (count.Get == 0)
            {
                return controller.hoverCount == 0;
            }
            return controller.hoverCount == count.Get - 1;
        }).QuickCheckThrowOnFailure();
    }


    [Test]
    public void ExitThenEnterChangesNothing()
    {
        Prop.ForAll<PositiveInt>(count =>
        {
            controller.hoverCount = count.Get;
            controller.OnHoverExit();
            controller.OnHoverEnter();
            return controller.hoverCount == count.Get;
        }).QuickCheckThrowOnFailure();
    }

    /// <summary>
    /// Property based test to verify that OnHoverEnter() followed by OnHoverExit() does not change hoverCount.
    /// </summary>
    [Test]
    public void EnterThenExitChangesNothing()
    {
        Prop.ForAll<PositiveInt>(count =>
        {
            controller.hoverCount = count.Get;
            controller.OnHoverEnter();
            controller.OnHoverExit();
            return controller.hoverCount == count.Get;
        }).QuickCheckThrowOnFailure();
    }
}