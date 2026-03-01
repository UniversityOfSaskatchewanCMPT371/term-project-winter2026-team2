using NUnit.Framework;
using UnityEngine;


/// <summary>
/// Verify the logic of ToolTipModel, which is just a simple data container
/// (Test getters and setters to ensure they work as expected)
/// </summary>
public class ToolTipsModelTests
{
    private ToolTipModel model;

    /// <summary>
    /// Set up a fresh ToolTipModel before each test
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
    }

    /// <summary>
    /// Clean up after each tests to prevent memory leaks
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(model);
    }

    /// <summary>
    /// When a ToolTipModel is first created, its Title should be null
    /// </summary>
    [Test]
    public void Title_ShouldBeNull_WhenInitialized()
    {
        // Brand new model should have no title.
        Assert.IsNull(model.Title);
    }

    /// <summary>
    /// When we assign a Title to the model, we should be able to read it back correctly
    /// </summary>
    [Test]
    public void Title_ShouldBeSet_WhenAssigned()
    {
        model.Title = "Test Title";
        Assert.AreEqual("Test Title", model.Title);
    }

    /// <summary>
    /// When a ToolTipModel is first created, its Description should be null
    /// </summary>
    [Test]
    public void Description_ShouldBeNull_WhenInitialized()
    {
        // Same for description – starts null.
        Assert.IsNull(model.Description);
    }

    /// <summary>
    /// When we assign a Description to the model, we should be able to read it back correctly
    /// </summary>
    [Test]
    public void Description_ShouldBeSet_WhenAssigned()
    {
        model.Description = "Test Description";
        Assert.AreEqual("Test Description", model.Description);
    }
}