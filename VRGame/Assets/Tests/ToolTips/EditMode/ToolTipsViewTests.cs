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
        view.title = titleGo.AddComponent<TextMeshProUGUI>();

        var descGo = new GameObject("Description");
        descGo.transform.SetParent(viewGo.transform);
        view.description = descGo.AddComponent<TextMeshProUGUI>();

        // Create a mock model with known return values.
        mockModel = Substitute.For<IToolTipModel>();
        mockModel.Title.Returns("Mock Title");
        mockModel.Description.Returns("Mock Description");
    }

    /// <summary>
    /// Verifies that SetUp correctly initializes the view components.
    /// </summary>
    [Test]
    public void SetUp_InitializesView()
    {
        Assert.IsNotNull(view, "View should be created.");
        Assert.IsNotNull(view.title, "Title component should be assigned.");
        Assert.IsNotNull(view.description, "Description component should be assigned.");
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
        Assert.AreEqual("Mock Title", view.title.text);
        Assert.AreEqual("Mock Description", view.description.text);
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