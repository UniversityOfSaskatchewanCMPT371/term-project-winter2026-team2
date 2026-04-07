using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System;
using UnityEngine.AI;

using UnityEngine.XR.Interaction.Toolkit;
using Microsoft.FSharp.Core;
using JetBrains.Annotations;
using FsCheck.Fluent;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using FsCheck;

public class LogicGameModelTest
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;

    /// <summary>
    /// The component that is being tested.
    /// TODO : Replace the type to the class you are testing.
    /// </summary>
    LogicGameModel lgm;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        lgm = go.AddComponent<LogicGameModel>(); // TODO : Replace generic with component you are testing
    }

    // helper func, creates basic 3*3 grid for testing
    public void setup_valid_3x3()
    {
        Panel p;
        go.AddComponent<LogicGameController>();

        // each panel must be in individual child component
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                GameObject childGo = new GameObject();
                childGo.transform.SetParent(go.transform);
                // xr interactable component
                childGo.AddComponent<XRSimpleInteractable>();
                childGo.AddComponent<MeshRenderer>();
                p = childGo.AddComponent<Panel>();
                p.GridX = x;
                p.GridY = y;
                p.panelColour = PanelColour.Red;

                if (x == 0 && y == 0) {p.attribute = PanelAttribute.Start; }
                else if (x == 2 && y == 2) {p.attribute = PanelAttribute.Exit;}
                else {p.attribute = PanelAttribute.Normal; }

                childGo.AddComponent<PanelTextureManager>().Init();

                p.Init();
            }
        }
        
        lgm.Init();
    }

    public void fill_grid()
    {
        lgm.panelGrid[0,0].exitDirection = Direction.Left;

        lgm.panelGrid[1,0].entryDirection = Direction.Left;
        lgm.panelGrid[1,0].exitDirection = Direction.Right;

        lgm.panelGrid[2,0].entryDirection = Direction.Left;
        lgm.panelGrid[2,0].exitDirection = Direction.Down;

        lgm.panelGrid[2,1].entryDirection = Direction.Up;
        lgm.panelGrid[2,1].exitDirection = Direction.Left;

        lgm.panelGrid[1,1].entryDirection = Direction.Right;
        lgm.panelGrid[1,1].exitDirection = Direction.Left;

        lgm.panelGrid[0,1].entryDirection = Direction.Right;
        lgm.panelGrid[0,1].exitDirection = Direction.Down;

        lgm.panelGrid[0,2].entryDirection = Direction.Up;
        lgm.panelGrid[0,2].exitDirection = Direction.Right;

        lgm.panelGrid[1,2].entryDirection = Direction.Left;
        lgm.panelGrid[1,2].exitDirection = Direction.Right;

        lgm.panelGrid[2,2].entryDirection = Direction.Left;

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

    [Test]
    public void Init_valid()
    {
        // add single child with panel object
        GameObject childGo = new GameObject();
        childGo.AddComponent<Panel>();
        childGo.transform.SetParent(go.transform);

        lgm.Init();
    }

    [Test]
    public void Init_no_panels_in_children()
    {
        LogAssert.Expect(LogType.Error, "Could not find a panel script attached to one of my children!");
        // create child gameobject which does not contain a panel
        // - this should cause init to fail
        GameObject childGo = new GameObject();
        // box collider is most basic component - doesn't contain panel
        childGo.AddComponent<BoxCollider>();
        childGo.transform.SetParent(go.transform);

        try {
            lgm.Init();
            Assert.Fail("Init with child without panel should have failed");
        }
        catch {}
    }
    [Test]
    public void Init_one_child_without_panel()
    {
        LogAssert.Expect(LogType.Error, "Could not find a panel script attached to one of my children!");
        // create child gameobject which does not contain a panel
        // - this should cause init to fail
        GameObject childGo1 = new GameObject();
        // box collider is most basic component - doesn't contain panel
        childGo1.AddComponent<BoxCollider>();
        childGo1.transform.SetParent(go.transform);

        // second child does have panel
        GameObject childGo2 = new GameObject();
        childGo2.AddComponent<Panel>();
        childGo2.transform.SetParent(go.transform);

        try {
            lgm.Init();
            Assert.Fail("Init with child without panel should have failed");
        }
        catch {}
    }


    [Test]
    public void Init_full_grid_3x3()
    {
        setup_valid_3x3();
    }


    [Test]
    public void get_panel()
    {

        setup_valid_3x3();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                // if no assertion triggered, success
                lgm.GetPanel(x, y);
            }
        }

    }

    [Test]
    public void IsGridFilled_no()
    {
        setup_valid_3x3();
        Assert.IsFalse(lgm.IsGridFilled());

    }

    [Test]
    public void IsGridFilled_yes()
    {
        setup_valid_3x3();
        // now manually fill each panel in the grid
        fill_grid();

        Assert.IsTrue(lgm.IsGridFilled());

    }

    [Test]
    public void clearGrid_PBT()
    {

        setup_valid_3x3();
        fill_grid();
        // clearGrid should be idempotent
        Prop.ForAll<NonNegativeInt>(count =>
        {
            lgm.ClearGrid();
            bool res = lgm.IsGridFilled();
            bool res2 = false;
            for (int i = 0; i < count.Get+1; i++)
            {
                lgm.ClearGrid();
                res2 = lgm.IsGridFilled();
            }
            return res == res2;
        }).QuickCheckThrowOnFailure();
    }
}