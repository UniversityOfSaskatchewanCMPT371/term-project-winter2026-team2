using NUnit.Framework;
using TMPro;
using UnityEngine;
using NSubstitute;

/// <summary>
/// Unit tests for ToolTipView
/// </summary>
public class ToolTipsViewTests
{
    private GameObject viewGo;
    private ToolTipView view;
    private IToolTipModel mockModel;

    /// <summary>
    /// Set up a GameObject with the view component and two TextMeshPro children.
    /// Also create a mock model with known return values for Title and Description.
    /// </summary>
    [SetUp]
    public void SetUp()
    {   
        // Build a view GameObject
        viewGo = new GameObject("ToolTipView");
        view = viewGo.AddComponent<ToolTipView>();

        // create and assign child TextMeshPro objects
        var titleGo = new GameObject("Title");
        titleGo.transform.SetParent(viewGo.transform);
        view.Title = titleGo.AddComponent<TextMeshProUGUI>();

        var descGo = new GameObject("Description");
        descGo.transform.SetParent(viewGo.transform);
        view.Description = descGo.AddComponent<TextMeshProUGUI>();

        // Create a mock model with known return values.
        mockModel = Substitute.For<IToolTipModel>();
        mockModel.Title.Returns("Mock Title");
        mockModel.Description.Returns("Mock Description");
    }

    /// <summary>
    /// clean up after each test to prevent memory leaks
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        // destroy the view GameObject
        Object.DestroyImmediate(viewGo);
    }

    /// <summary>
    /// when we call UpdateContent with the mock, the TMP fields should update
    /// </summary>
    [Test]
    public void UpdateContent_SetsTextFromModel()
    {
        // call UpdateContent with the mock model
        view.UpdateContent(mockModel);
        // verify the text fields are updated
        Assert.AreEqual("Mock Title", view.Title.text);
        Assert.AreEqual("Mock Description", view.Description.text);
    }

    /// <summary>
    /// when we call SetActive, it should toggle the whole view GameObject on and off
    /// </summary>
    [Test]
    public void SetActive_TogglesGameObject()
    {
        // SetActive should turn the whole view on and off
        view.SetActive(true);
        Assert.IsTrue(viewGo.activeSelf);

        view.SetActive(false);
        Assert.IsFalse(viewGo.activeSelf);
    }

}