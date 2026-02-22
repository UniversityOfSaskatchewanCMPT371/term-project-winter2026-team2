using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class ToolTipModelTests
{
    [UnityTest]
    public IEnumerator ToolTipModel_CanBeCreatedInPlayMode()
    {
        // Create an instance of the ToolTipModel ScriptableObject
        var toolTipModel = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));

        Assert.IsNotNull(toolTipModel);
        yield return null;
        Object.DestroyImmediate(toolTipModel);
    }
}
