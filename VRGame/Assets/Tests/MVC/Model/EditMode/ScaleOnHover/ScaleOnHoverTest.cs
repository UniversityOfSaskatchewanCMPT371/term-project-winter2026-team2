using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Edit mode tests for the ScaleOnHoverModel class to ensure it initializes and functions correctly in edit mode
/// </summary>


public class SoH_EditTests
{
    /// <summary>
    /// Simple test for LinkedObjects getter method
    /// </summary>
    [Test]
    public void SoH_EditTestsLinkedObjects_get()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        // Act - get LinkedObjects
        Transform[] linkedObjects = model.LinkedObjects;

        // Assert - linkedObjects should be null on initialization
        Assert.IsNull(linkedObjects, "LinkedObjects should be null when not initialized");

    }

    /// <summary>
    /// Simple test for LinkedObjects setter method
    /// </summary>
    [Test]
    public void SoH_EditTestsLinkedObjects_set()
    {
        /// Arrange - create a ScaleOnHoverModel and some test objects to assign to LinkedObjects
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        GameObject obj1 = new GameObject("TestObject1");
        GameObject obj2 = new GameObject("TestObject2");

        Transform[] testObjects = new Transform[]
        {
            obj1.transform,
            obj2.transform
        };

        /// Act - set LinkedObjects to test objects
        model.LinkedObjects = testObjects;

        /// Assert
        Assert.AreEqual(testObjects, model.LinkedObjects);
    }

    /// <summary>
    /// Simple test for null check in LinkedObjects 
    /// This should reject null values
    /// </summary>
    [Test]
    public void SoH_EditTestsLinkedObjects_null_check()
    {
        /// Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();
        
        /// Set up a valid array first
        GameObject obj1 = new GameObject("TestObject1");
        Transform[] validObjects = new Transform[] { obj1.transform };
        model.LinkedObjects = validObjects;

        /// Act - attempt to set LinkedObjects to null 
        /// Note: This should be rejected since we prevent null assignments in the setter, 
        ///         but we need to check if it logs an error as expected, so we use LogAssert to verify the error log
        LogAssert.Expect(LogType.Error,  "Can't set linked objects to null or empty array");
        model.LinkedObjects = null;

        // Assert - LinkedObjects should remain unchanged
        Assert.AreEqual(validObjects, model.LinkedObjects, "LinkedObjects should not change when set to null");
    }


    /// <summary>
    /// Simple test for hoverScaleMultiplier getter method
    /// </summary>
    [Test]
    public void SoH_EditTestsHoverScaleMultiplier_get()
    {
        /// Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        /// Act - get hoverScaleMultiplier
        float multiplier = model.HoverScaleMultiplier;

        /// Assert - default value should be 1.25f
        Assert.AreEqual(1.25f, multiplier);
    }

    /// <summary>
    /// Simple test for hoverScaleMultiplier setter method
    /// </summary>
    [Test]
    public void SoH_EditTestsHoverScaleMultiplier_set()
    {
        /// Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();   
        float newValue = 1.5f;
        
        /// Act - set hoverScaleMultiplier
        model.HoverScaleMultiplier = newValue;

        /// Assert - value should be set correctly
        Assert.AreEqual(newValue, model.HoverScaleMultiplier);
    }


    /// <summary>
    /// Simple test for hoverScaleMultiplier setter method to check for negative values 
    /// This should reject negative values
    /// </summary>
    [Test]
    public void SoH_EditTestsHoverScaleMultiplier_negativeMultiplier()
    {        
        /// Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        /// Act - attempt to set hoverScaleMultiplier to a negative value
        LogAssert.Expect(LogType.Error, "Hover scale multiplier must be non-negative");
        model.HoverScaleMultiplier = -0.5f;

        /// Assert - value should remain unchanged since we prevent negative assignments in the setter, 
        ///     so it should still be the default value of 1.25f
        Assert.AreEqual(1.25f, model.HoverScaleMultiplier);
    }


    /// <summary>
    /// Simple test for ScaleSpeed getter method
    /// </summary>
    [Test]
    public void SoH_EditTestsScaleSpeed_get()
    {
        /// Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        /// Act - get ScaleSpeed
        float speed = model.ScaleSpeed;

        /// Assert - default value should be 10f
        Assert.AreEqual(10f, speed);
    }


    /// <summary>
    /// Simple test for ScaleSpeed setter method
    /// </summary>
    [Test]
    public void SoH_EditTestsScaleSpeed_set()
    {
        /// Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();
        float newValue = 15f;

        /// Act - set ScaleSpeed
        model.ScaleSpeed = newValue;

        /// Assert - value should be set correctly
        Assert.AreEqual(newValue, model.ScaleSpeed);
    }


    /// <summary>
    /// Simple test for ScaleSpeed setter method to check for negative values 
    /// Thisshould reject negative values
    /// </summary>
    [Test]
    public void SoH_EditTestsScaleSpeed_negativeValue()
    {
        /// Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        /// Act - attempt to set ScaleSpeed to a negative value
        LogAssert.Expect(LogType.Error, "Scale speed must be zero or positive");
        model.ScaleSpeed = -5f;

        /// Assert - value should remain unchanged since we prevent negative assignments in the setter, 
        ///     so it should still be the default value of 10f
        Assert.AreEqual(10f, model.ScaleSpeed);
    }
    
}

