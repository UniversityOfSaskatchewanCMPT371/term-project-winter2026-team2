using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;


///<summary>
/// It verifies that the text fields in the view have been updated with the data from the model
/// </summary>
public class ToolTipsViewTests
{
    /// <summary>
    /// Create a real ToolTipView with TMP components and a real ToolTipModel with data,
    /// After waiting a frame for Start() to run, verifies the text fields were updated from the model.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_SetsTextFromAssignedModel()
    {
        // Build a real view with TMP children
        var go = new GameObject("ToolTipView");
        var view = go.AddComponent<ToolTipView>();

        var titleGo = new GameObject("Title");
        titleGo.transform.SetParent(go.transform);
        view.title = titleGo.AddComponent<TextMeshProUGUI>();

        var descGo = new GameObject("Description");
        descGo.transform.SetParent(go.transform);
        view.description = descGo.AddComponent<TextMeshProUGUI>();

        // Create a real model and assign it
        var model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        model.Title = "Play Title";
        model.Description = "Play Description";
        view.data = model;

        // Wait one frame – Start() will run automatically
        yield return null;

        // Now the text fields should be filled with the model's data
        Assert.AreEqual("Play Title", view.title.text);
        Assert.AreEqual("Play Description", view.description.text);

        // Clean up
        UnityEngine.Object.DestroyImmediate(go);
        UnityEngine.Object.DestroyImmediate(model);
    }
}