using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Play mode tests for ToolTipModel.
/// </summary>
public class ToolTipsModelTests
{
    /// <summary>
    /// Verifies that a ToolTipModel can be created in play mode without errors.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator ToolTipModel_CanBeCreatedInPlayMode()
    {
        // create a ToolTipModel instance
        ToolTipModel model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        //verify not null
        Assert.IsNotNull(model);
        //wait  one fram
        yield return null;
        //clean up
        UnityEngine.Object.DestroyImmediate(model);
    }

    /// <summary>
    /// Verifies that Title can be set and retrieved.
    /// </summary>
    [UnityTest]
    public IEnumerator SetAndGetTitle()
    {
        // create model and set title
        ToolTipModel model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        model.Title = "Play Title";

        // wait one frame
        yield return null;

        // verify
        Assert.AreEqual("Play Title", model.Title, "Title should be retrievable.");

        // clean up
        UnityEngine.Object.DestroyImmediate(model);
    }

    /// <summary>
    /// Verifies that Description can be set and retrieved.
    /// </summary>
    [UnityTest]
    public IEnumerator SetAndGetDescription()
    {
        // create model and set desc
        ToolTipModel model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        model.Description = "Play Description";
        
        // wait one frame
        yield return null;
        // verify
        Assert.AreEqual("Play Description", model.Description, "Description should be retrievable.");
        // clean up
        UnityEngine.Object.DestroyImmediate(model);
    }
}