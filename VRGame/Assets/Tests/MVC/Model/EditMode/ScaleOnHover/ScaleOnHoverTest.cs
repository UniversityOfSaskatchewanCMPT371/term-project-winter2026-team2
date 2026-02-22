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
    /// Simple test for null check in LinkedObjects - should reject null values
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
        LogAssert.Expect(LogType.Error);
        model.LinkedObjects = null;

        /// Assert 
        Assert.AreEqual(validObjects, model.LinkedObjects);
    }

}
