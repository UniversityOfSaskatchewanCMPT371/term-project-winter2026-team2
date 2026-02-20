using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SoH_EditTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void SoH_EditTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        ScaleOnHover soh = go.AddComponent<ScaleOnHover>();
        soh.init();
        Assert.IsNotNull(soh);
    }

}
