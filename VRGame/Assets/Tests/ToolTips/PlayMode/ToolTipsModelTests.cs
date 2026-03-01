using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// A quick test to ensure we can create a ToolTipModel while the game is running
/// </summary>
public class ToolTipsModelTests
{
    [UnityTest]
    public IEnumerator ToolTipModel_CanBeCreatedInPlayMode()
    {
        var model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        Assert.IsNotNull(model);
        yield return null;
        UnityEngine.Object.DestroyImmediate(model);
    }
}