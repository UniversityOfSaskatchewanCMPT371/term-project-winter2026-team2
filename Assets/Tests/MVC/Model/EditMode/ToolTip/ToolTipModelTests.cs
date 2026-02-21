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

    // A Test behaves as an ordinary method
    [Test]
    public void Title_ShouldBeNull_WhenInitialized()
    {
        Assert.IsNull(toolTipModel.Title);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
/*     [UnityTest]
    public IEnumerator ToolTipModelTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    } */
}
