using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;


public class RoomModelTests
{
    /// <summary>
    /// Test the initialization of this component.
    /// </summary>
    [Test]
    public void Instantiation()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // confirm that roomModel is not null
        Assert.NotNull(roomModel);

        // initialize the component, no errors should occur
        roomModel.Init();

        // free up memory
        Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator PlayModeTest()
    {
        yield return null;
    }
}