using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SoH_EditTests
{
    [Test]
    public void SoH_EditTestsSimplePasses()
    {
        GameObject go = new GameObject();
        ScaleOnHoverModel soh = go.AddComponent<ScaleOnHoverModel>();
        Assert.IsNotNull(soh);
    }

    [Test]
    public void SoH_EditTestsInitialization()
    {
        GameObject go = new GameObject();
        GameObject linkedGo = new GameObject();
        ScaleOnHoverModel soh = go.AddComponent<ScaleOnHoverModel>();
        
        Transform[] linkedObjects = new Transform[] { linkedGo.transform };
        soh.Initialize(linkedObjects, 1.25f, 10f);
        
        Assert.IsNotNull(soh.getLinkedObjects());
        Assert.AreEqual(1, soh.getLinkedObjects().Length);
    }

    [Test]
    public void SoH_EditTestsScaleSpeed()
    {
        GameObject go = new GameObject();
        GameObject linkedGo = new GameObject();
        ScaleOnHoverModel soh = go.AddComponent<ScaleOnHoverModel>();
        
        Transform[] linkedObjects = new Transform[] { linkedGo.transform };
        float expectedScaleSpeed = 10f;
        soh.Initialize(linkedObjects, 1.25f, expectedScaleSpeed);
        
        Assert.AreEqual(expectedScaleSpeed, soh.getScaleSpeed());
    }
}
