using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class ToolTipModelTests
{

    private ToolTipModel toolTipModel;

    [SetUp]
    public void SetUp()
    {
        toolTipModel = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(toolTipModel);
    }

    /// <summary>
    /// Tests that the Title property of the ToolTipModel is null 
    /// when the model is initialized.
    /// </summary>
    [Test]
    public void Title_ShouldBeNull_WhenInitialized()
    {
        Assert.IsNull(toolTipModel.Title);
    }

    /// <summary>
    /// Tests that the Description property of the ToolTipModel is null 
    /// when the model is initialized.
    /// </summary>
    [Test]
    public void Description_ShouldBeNull_WhenInitialized()
    {
        Assert.IsNull(toolTipModel.Description);
    }

    /// <summary>
    /// Tests that the Title property of the ToolTipModel is set correctly
    /// when a value is assigned to it.
    /// </summary>
    [Test]
    public void Title_ShouldBeSet_WhenAssigned()
    {
        toolTipModel.Title = "Test Title";
        Assert.AreEqual("Test Title", toolTipModel.Title);
    }

    /// <summary>
    /// Tests that the Description property of the ToolTipModel is set correctly
    /// when a value is assigned to it.
    /// </summary>
    [Test]
    public void Description_ShouldBeSet_WhenAssigned()
    {
        toolTipModel.Description = "Test Description";
        Assert.AreEqual("Test Description", toolTipModel.Description);
    }

}
