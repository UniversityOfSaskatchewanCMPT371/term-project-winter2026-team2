using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using NSubstitute;


/// <summary>
/// Edit Mode tests for ToolTipView.
/// Verifies that the ToolTipView correctly updates 
/// its text and toggles active state
/// </summary>
public class ToolTipViewTests
{
    private GameObject toolTipGameObject;
    private ToolTipView toolTipView;
    private IToolTipModel toolTipModel;

    [SetUp]
    public void SetUp()
    {
        //root gameobject for the view
        toolTipGameObject = new GameObject("ToolTip");
        toolTipView = toolTipGameObject.AddComponent<ToolTipView>();
        
        //child gameobjects for title and description
        var titleGo = new GameObject("Title");
        titleGo.transform.SetParent(toolTipGameObject.transform);
        toolTipView.title = titleGo.AddComponent<TextMeshProUGUI>();

        //description child gameobject
        var descriptionGo = new GameObject("Description");
        descriptionGo.transform.SetParent(toolTipGameObject.transform);
        toolTipView.description = descriptionGo.AddComponent<TextMeshProUGUI>();

        //mock of model interface
        toolTipModel = Substitute.For<IToolTipModel>();
        toolTipModel.Title.Returns("Test Title");
        toolTipModel.Description.Returns("Test Description");
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(toolTipGameObject);
    }

    /// <summary>
    /// verifies that UpdateContent() correctly sets the text of the title and description
    /// </summary>
    [Test]
    public void UpdateContent_SetsTextFromModel()
    {
        toolTipView.UpdateContent(toolTipModel);
        Assert.AreEqual("Test Title", toolTipView.title.text);
        Assert.AreEqual("Test Description", toolTipView.description.text);
    }

    /// <summary>
    /// verifies that SetActive() toggles the active state of the tooltip gameobject
    /// </summary>
    [Test]
    public void SetActive_MakesGameObjectActiveOrInactive()
    {
        toolTipView.setActive(true);
        Assert.IsTrue(toolTipGameObject.activeSelf);

        toolTipView.setActive(false);
        Assert.IsFalse(toolTipGameObject.activeSelf);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
/*     [UnityTest]
    public IEnumerator ToolTipViewTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    } */
}
