using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Edit mode tests for the ScaleOnHoverModel class to ensure it initializes and functions correctly in edit mode
/// </summary>

/// Note: These tests are focused on the initialization and basic functionality of the ScaleOnHoverModel in edit mode

public class SoH_EditTests
{
    /// <summary>
    /// Simple test for LinkedObjects accessor method
    [Test]
    public void SoH_EditTestsLinkedObjects()
    {
        /// Create a new GameObject and add the ScaleOnHoverModel component to it
        GameObject go = new GameObject();
        GameObject linkedGo = new GameObject();
        ScaleOnHoverModel soh = go.AddComponent<ScaleOnHoverModel>();
        
        /// Set the LinkedObjects property
        Transform[] linkedObjects = new Transform[] { linkedGo.transform };
        soh.LinkedObjects = linkedObjects;
        
        /// Assert that the LinkedObjects property returns the correct value
        Assert.AreEqual(linkedObjects, soh.LinkedObjects);
    }

    /// <summary>
    /// Test to check if the ScaleOnHoverModel initializes correctly with given parameters
    /// </summary>
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

    /// <summary>
    /// Test to check if the hover scale multiplier is set correctly during initialization
    /// </summary>  
    [Test]
    public void SoH_EditTestsGetScaleSpeed()
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
