using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;


///<summary>
/// Play mode tests for ToolTipView.
/// </summary>
public class ToolTipsViewTests
{
    /// <summary>
    /// Verifies that Start sets the text from the assigned model.
    /// </summary>
    [UnityTest]
    public IEnumerator Start_SetsTextFromAssignedModel()
    {
        // Build a real view with TMP children
        GameObject go = new GameObject("ToolTipView");
        ToolTipView view = go.AddComponent<ToolTipView>();

        // create and assign child TextMeshPro objects
        GameObject titleGo = new GameObject("Title");
        titleGo.transform.SetParent(go.transform);
        view.Title = titleGo.AddComponent<TextMeshProUGUI>();

        GameObject descGo = new GameObject("Description");
        descGo.transform.SetParent(go.transform);
        view.Description = descGo.AddComponent<TextMeshProUGUI>();

        // Create a real model and assign it
        ToolTipModel model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        model.Title = "Play Title";
        model.Description = "Play Description";
        view.Data = model;

        // Wait one frame – Start() will run automatically
        yield return null;

        // Now the text fields should be filled with the model's data
        Assert.AreEqual("Play Title", view.Title.text);
        Assert.AreEqual("Play Description", view.Description.text);

        // Clean up
        UnityEngine.Object.DestroyImmediate(go);
        UnityEngine.Object.DestroyImmediate(model);
    }
}