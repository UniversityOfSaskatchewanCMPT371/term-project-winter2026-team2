
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
//using NSubstitute;
using System;
using UnityEngine.XR.Interaction.Toolkit;
using FsCheck;
using FsCheck.Fluent;

public class PanelTest
{
    /// <summary>
    /// The game object the component being test will be
    /// attached to.
    /// </summary>
    GameObject go;
    GameObject parent;

    /// <summary>
    /// The component that is being tested.
    /// TODO : Replace the type to the class you are testing.
    /// </summary>
    Panel p;

    /// <summary>
    /// Called before each tests. Handles the setup for
    /// game object and component being tested
    /// </summary>
    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        p = go.AddComponent<Panel>(); // TODO : Replace generic with component you are testing
    }

    // helper func
    public void valid_init()
    {
        // add single child with panel object
        p.GridX = 3;
        p.GridY = 3;

        p.attribute = PanelAttribute.Block;
        // can't add mocks to gameobject

        // panelTextureManager needs a renderer
        go.AddComponent<MeshRenderer>();
        PanelTextureManager ptm = go.AddComponent<PanelTextureManager>();
        ptm.Init();

        // xr interactable component
        go.AddComponent<XRSimpleInteractable>();

        //parent object which contains a LogicGameController

        parent = new GameObject();
        parent.AddComponent<LogicGameController>();

        go.transform.SetParent(parent.transform);

        p.Init();
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
    public void InitValid()
    {
        // add single child with panel object
        p.GridX = 3;
        p.GridY = 3;

        p.attribute = PanelAttribute.Block;
        // can't add mocks to gameobject

        // panelTextureManager needs a renderer
        go.AddComponent<MeshRenderer>();
        PanelTextureManager ptm = go.AddComponent<PanelTextureManager>();
        ptm.Init();

        // xr interactable component
        go.AddComponent<XRSimpleInteractable>();

        //parent object which contains a LogicGameController

        parent = new GameObject();
        parent.AddComponent<LogicGameController>();

        go.transform.SetParent(parent.transform);

        p.Init();
    }

    [Test]
    public void Init_no_xr_componenent()
    {
        LogAssert.Expect(LogType.Error, "Could not find the XR interactable!");
        // add single child with panel object
        p.GridX = 3;
        p.GridY = 3;

        p.attribute = PanelAttribute.Block;
        // can't add mocks to gameobject

        // panelTextureManager needs a renderer
        go.AddComponent<MeshRenderer>();
        PanelTextureManager ptm = go.AddComponent<PanelTextureManager>();
        ptm.Init();

        // xr interactable component
        //go.AddComponent<XRSimpleInteractable>();

        //parent object which contains a LogicGameController

        parent = new GameObject();
        parent.AddComponent<LogicGameController>();

        go.transform.SetParent(parent.transform);

        try{
            p.Init();
            Assert.Fail("Panel init without xr simple interactable should fail");
        }
        catch {}
    }

    [Test]
    public void Init_no_xr_component_in_parent()
    {
        LogAssert.Expect(LogType.Error, "Could not find the parent's LogicGameController!");
        // add single child with panel object
        p.GridX = 3;
        p.GridY = 3;

        p.attribute = PanelAttribute.Block;
        // can't add mocks to gameobject

        // panelTextureManager needs a renderer
        go.AddComponent<MeshRenderer>();
        PanelTextureManager ptm = go.AddComponent<PanelTextureManager>();
        ptm.Init();

        // xr interactable component
        go.AddComponent<XRSimpleInteractable>();

        //parent object which contains a LogicGameController

        GameObject parent = new GameObject();
        //parent.AddComponent<LogicGameController>();

        go.transform.SetParent(parent.transform);

        try{
            p.Init();
            Assert.Fail("Panel init without LogicGameController in parent should fail");
        }
        catch {}
    }

    [Test]
    public void Init_no_texture_manager()
    {
        LogAssert.Expect(LogType.Error, "Could not find texture manager!");
        // add single child with panel object
        p.GridX = 3;
        p.GridY = 3;

        p.attribute = PanelAttribute.Block;
        // can't add mocks to gameobject

        // panelTextureManager needs a renderer
        go.AddComponent<MeshRenderer>();
        //PanelTextureManager ptm = go.AddComponent<PanelTextureManager>();
        //ptm.Init();

        // xr interactable component
        go.AddComponent<XRSimpleInteractable>();

        //parent object which contains a LogicGameController

        parent = new GameObject();
        parent.AddComponent<LogicGameController>();

        go.transform.SetParent(parent.transform);

        try{
            p.Init();
            Assert.Fail("Panel init without texture manager should fail");
        }
        catch {}
    }

    [Test]
    public void Get_Set_EntryDirection_PBT()
    {
        valid_init();
        Prop.ForAll<Direction>(dir =>
        {
            p.SetEntryDirection(dir);
            return p.GetEntryDirection() == dir;

        }).QuickCheckThrowOnFailure();
    }

    [Test]
    public void Get_Set_ExitDirection_PBT()
    {
        valid_init();
        Prop.ForAll<Direction>(dir =>
        {
            p.SetExitDirection(dir);
            return p.GetExitDirection() == dir;

        }).QuickCheckThrowOnFailure();
    }

    [Test] 
    public void equals()
    {
        valid_init();
        // create second valid panel
        Panel p2 = go.AddComponent<Panel>();

        p2.GridX = 3;
        p2.GridY = 3;

        p2.attribute = PanelAttribute.Block;

        p2.Init();

        Assert.IsTrue(p.Equals(p2), "p2 should equal p");
    }

    [Test] 
    public void nequals()
    {
        valid_init();
        // create second valid panel
        Panel p2 = go.AddComponent<Panel>();

        // dif x coordinate
        p2.GridX = 4;
        p2.GridY = 3;

        p2.attribute = PanelAttribute.Block;

        p2.Init();

        Assert.IsFalse(p.Equals(p2), "p2 shouldn't equal p");
    }
}
