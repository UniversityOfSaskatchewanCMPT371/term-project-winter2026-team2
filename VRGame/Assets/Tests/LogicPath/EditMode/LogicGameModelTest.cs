using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System;

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
}