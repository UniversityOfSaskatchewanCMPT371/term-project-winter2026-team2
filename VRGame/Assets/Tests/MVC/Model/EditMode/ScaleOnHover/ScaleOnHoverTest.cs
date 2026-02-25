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
        // Arrange - create a ScaleOnHoverModel and some test objects to assign to LinkedObjects
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        GameObject obj1 = new GameObject("TestObject1");
        GameObject obj2 = new GameObject("TestObject2");

        Transform[] testObjects = new Transform[]
        {
            obj1.transform,
            obj2.transform
        };

        // Act - set LinkedObjects to test objects
        model.LinkedObjects = testObjects;

        // Assert
        Assert.AreEqual(testObjects, model.LinkedObjects);
    }

    /// <summary>
    /// Simple test for length checks in LinkedObjects 
    /// </summary>
    [Test]
    public void SoH_EditTestsLinkedObjects_lengthCheck()
    {
        // Arrange - create a ScaleOnHoverModel and some test objects to assign to LinkedObjects
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        GameObject obj1 = new GameObject("TestObject1");
        GameObject obj2 = new GameObject("TestObject2");

        Transform[] testObjects = new Transform[]
        {
            obj1.transform,
            obj2.transform
        };

        // Act - set LinkedObjects to test objects
        model.LinkedObjects = testObjects;

        // Assert - length of LinkedObjects should match length of assigned array
        Assert.AreEqual(testObjects.Length, model.LinkedObjects.Length, "Length of LinkedObjects should match length of assigned array");
    }


    /// <summary>
    /// Simple test for hoverScaleMultiplier getter method
    /// </summary>
    [Test]
    public void SoH_EditTestsHoverScaleMultiplier_get()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        // Act - get hoverScaleMultiplier
        float multiplier = model.HoverScaleMultiplier;

        // Assert - default value should be 1.25f
        Assert.AreEqual(1.25f, multiplier);
    }

    /// <summary>
    /// Simple test for hoverScaleMultiplier setter method
    /// </summary>
    [Test]
    public void SoH_EditTestsHoverScaleMultiplier_set()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();   
        float newValue = 1.5f;
        
        // Act - set hoverScaleMultiplier
        model.HoverScaleMultiplier = newValue;

        // Assert - value should be set correctly
        Assert.AreEqual(newValue, model.HoverScaleMultiplier);
    }


    /// <summary>
    /// Simple test for ScaleSpeed getter method
    /// </summary>
    [Test]
    public void SoH_EditTestsScaleSpeed_get()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        // Act - get ScaleSpeed
        float speed = model.ScaleSpeed;

        // Assert - default value should be 10f
        Assert.AreEqual(10f, speed);
    }


    /// <summary>
    /// Simple test for ScaleSpeed setter method
    /// </summary>
    [Test]
    public void SoH_EditTestsScaleSpeed_set()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();
        float newValue = 15f;

        // Act - set ScaleSpeed
        model.ScaleSpeed = newValue;

        // Assert - value should be set correctly
        Assert.AreEqual(newValue, model.ScaleSpeed);
    }


    /// <summary>
    /// Simple test for NormalScales getter method
    /// </summary>
    [Test]
    public void SoH_EditTestsNormalScale_get()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();
        // create a test object to assign to LinkedObjects
        GameObject obj1 = new GameObject("TestObject1");
        // set the test object's scale to default value (Vector3.one)
        obj1.transform.localScale = Vector3.one;

        model.LinkedObjects = new Transform[]
        {
            obj1.transform
        };

        // Act - initialize scales
        model.InitializeScales();
        Vector3[] normalScale = model.NormalScales;

        // Assert - NormalScales should be initialized based on linked objects' scales
        Assert.AreEqual(Vector3.one, normalScale[0], "NormalScales[0] should match the object's scale");
    }


    /// <summary>
    /// Simple test for TargetScale getter method
    /// </summary>
    [Test]
    public void SoH_EditTestsTargetScale_get()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        // create a test object to assign to LinkedObjects
        GameObject obj1 = new GameObject("TestObject1");
        // set the test object's scale to default value (Vector3.one)
        obj1.transform.localScale = Vector3.one;

        // assign the test object to LinkedObjects
        model.LinkedObjects = new Transform[]
        {
            obj1.transform
        };

        // Act - initialize scales
        model.InitializeScales();
        // get TargetScales
        Vector3[] targetScales = model.TargetScales;

        // Assert - TargetScales should be initialized to NormalScales (not multiplied yet, that happens on hover)
        Assert.AreEqual(Vector3.one, targetScales[0], "TargetScales[0] should initially equal NormalScales[0]");
    }


    /// <summary>
    /// Simple test for TargetScale setter method
    /// </summary>
    [Test]
    public void SoH_EditTestsTargetScale_set()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();
        // create a test object to assign to LinkedObjects
        GameObject obj1 = new GameObject("TestObject1");
        // set the test object's scale to default value (Vector3.one)
        obj1.transform.localScale = Vector3.one;
        // assign the test object to LinkedObjects
        model.LinkedObjects = new Transform[]
        {
            obj1.transform
        }; 
        // initialize scales
        model.InitializeScales();

        // define new target scales to set (we try double cause why not)
        Vector3[] newTargetScales = new Vector3[]
        {
            Vector3.one * 2f
        };
        // Act - set TargetScales
        model.TargetScales = newTargetScales; 

        // Assert - TargetScales should be updated to the new values
        Assert.AreEqual(newTargetScales, model.TargetScales);
    }


    /// <summary>
    /// Simple test for IsHovering getter
    /// </summary>
    [Test]
    public void SoH_EditTestsIsHovering_get()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        // Act - get IsHovering
        // Note: Should be false to begin with
        bool isHovering = model.IsHovering;

        // Assert - IsHovering should be false on initialization
        Assert.IsFalse(isHovering, "IsHovering should be false on initialization");
    }


    /// <summary>
    /// Simple test for Initalize()
    /// </summary>
    [Test]
    public void SoH_EditTestsInitialize()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        Transform[] linkedObjects = new Transform[]
        {
            new GameObject("TestObject1").transform,
            new GameObject("TestObject2").transform
        };

        float hoverScaleMultiplier = 1.5f;
        float scaleSpeed = 20f;

        // Act - Initialize data
        model.Initialize(linkedObjects, hoverScaleMultiplier, scaleSpeed);

        // Assert - check input parameters
        Assert.IsNotNull(model.LinkedObjects, "LinkedObjects should not be null after initialization");
        Assert.AreEqual(1.5f, model.HoverScaleMultiplier, "HoverScaleMultiplier should be 1.5f");
        Assert.AreEqual(20f, model.ScaleSpeed, "ScaleSpeed should be 20f");
        
    }
    
    
    /// <summary>
    /// Simple test for InitializeScales() - normal equals target on initialization
    /// </summary>
    [Test]
    public void SoH_EditTestsInitializeScales()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        Transform[] linkedObjects = new Transform[]
        {
            new GameObject("TestObject1").transform,
            new GameObject("TestObject2").transform
        };

        model.LinkedObjects = linkedObjects;

        // Act - Initialize scales
        model.InitializeScales();

        // Assert - NormalScales and TargetScales should be initialized based on linked objects' scales
        Assert.AreEqual(model.NormalScales[0], model.TargetScales[0], "NormalScales[0] should match TargetScales[0]");
        Assert.AreEqual(model.NormalScales[1], model.TargetScales[1], "NormalScales[1] should match TargetScales[1]");
    }


    /// <summary>
    /// Simple test for OnHoverEnter()
    /// </summary>
    [Test]
    public void SoH_EditTestsOnHoverEnter() 
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        Transform obj = new GameObject("TestObject1").transform;
        Transform[] linkedObjects = new Transform[] { obj };

        model.Initialize(linkedObjects, 2.0f, 3.0f);

        // manually define target scale
        Vector3 target = model.NormalScales[0] * 2.0f;

        // Act - call OnHoverEnter
        model.OnHoverEnter();

        // Assert
        Assert.AreEqual(target, model.TargetScales[0]);
        Assert.IsTrue(model.IsHovering, "IsHovering should be true after OnHoverEnter");
    }

    
    /// <summary>
    /// Simple test for OnHoverExit()
    /// </summary>
    [Test]
    public void SoH_EditTestsOnHoverExit() 
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        Transform obj = new GameObject("TestObject1").transform;
        Transform[] linkedObjects = new Transform[] { obj };

        model.Initialize(linkedObjects, 2.0f, 3.0f);

        // manually define normal scale 
        Vector3 normal = model.NormalScales[0];

        // call OnHoverEnter first
        model.OnHoverEnter();

        // Act - then call OnHoverExit
        model.OnHoverExit();

        // Assert
        Assert.AreEqual(normal, model.TargetScales[0]);
        Assert.IsFalse(model.IsHovering, "IsHovering should be true after OnHoverEnter");
    }


    /// <summary>
    /// Simple test for Awake
    /// </summary>
    [Test]
    public void SoH_EditTestsAwake()
    {
        // Arrange
        ScaleOnHoverModel model = new GameObject().AddComponent<ScaleOnHoverModel>();

        // create 2 game objects
        GameObject obj1 = new GameObject("TestObject1");
        GameObject obj2 = new GameObject("TestObject2");
        
        Transform[] linkedObjects = new Transform[]
        {
            obj1.transform,
            obj2.transform
        };

        model.LinkedObjects = linkedObjects;

        // Act - call Awake
        model.Awake();

        // Assert - validate all scales (since we initialize them on Awake)
        Assert.IsNotNull(model.NormalScales, "NormalScales should be initialized in Awake");
        Assert.IsNotNull(model.TargetScales, "TargetScales should be initialized in Awake");

        Assert.AreEqual(2, model.NormalScales.Length, "NormalScales should be of length 2 on Awake");
        Assert.AreEqual(2, model.TargetScales.Length, "TargetScales should be of length 2 on Awake");

        Assert.AreEqual(obj1.transform.localScale, model.NormalScales[0], "obj1 local scale should match normalscale on Awake");
        Assert.AreEqual(obj2.transform.localScale, model.NormalScales[1], "obj2 local scale should match normalscale on Awake");
    }
}

