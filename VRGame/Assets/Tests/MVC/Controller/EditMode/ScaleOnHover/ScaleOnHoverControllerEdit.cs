using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ScaleOnHoverControllerTests
{
    /// <summary>
    /// Simple test to ensure Start() calls Init() properly, and model and view are assigned.
    /// </summary>
    [Test]
    public void StartInitializesLayers()
    {
        /// Arrange
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        /// Attach the model and controller to the GameObject (because this does not use
        /// any actual implementation of the model and view, it is ok to not use 
        /// substitutes here. In addition, it needs AddComponent to be tested, which needs
        /// a monobehaviour class)
        go.AddComponent<ScaleOnHoverModel>();
        go.AddComponent<ScaleOnHoverView>();

        /// Act
        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, "Model or View Layer does not exist");
        controller.Start();

        // Assert
        Assert.IsNotNull(controller.model, "Model should have been assigned.");
        Assert.IsNotNull(controller.view, "View should have been assigned.");

        /// Clean up
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Tests that Start() throws an exception if the model is missing
    /// </summary>
    [Test]
    public void StartExceptionThrownMissingModel()
    {
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();
        go.AddComponent<ScaleOnHoverView>();

        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, "Model or View Layer does not exist");
        Assert.Throws<UnityEngine.Assertions.AssertionException>(() => controller.Start());
    }

    /// <summary>
    /// Tests that Start() throws an exception if the view is missing
    /// </summary>
    [Test]
    public void StartExceptionThrownMissingView()
    {
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();
        go.AddComponent<ScaleOnHoverModel>();

        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, "Model or View Layer does not exist");
        Assert.Throws<UnityEngine.Assertions.AssertionException>(() => controller.Start());
    }


    /// <summary>
    /// Test that retrieveLinkedObjects() returns the linked objects from the model layer when they exist
    /// </summary>
    [Test]
    public void retrieveLinkedObjects_ObjectsExist()
    {
        // Arrange
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        Transform[] expectedTransforms = new Transform[] { new GameObject().transform, new GameObject().transform };

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        mockModel.LinkedObjects.Returns(expectedTransforms);

        controller.model = mockModel;

        // Act
        Transform[] result = controller.retrieveLinkedObjects();

        // Assert
        Assert.AreEqual(expectedTransforms, result);

        // Clean up
        Object.DestroyImmediate(go);
        Object.DestroyImmediate(expectedTransforms[0].gameObject);
        Object.DestroyImmediate(expectedTransforms[1].gameObject);
    }

    [Test]
    public void retrieveLinkedObjects_ObjectsDontExist()
    {
        // Arrange
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        Transform[] expectedTransforms = null;

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        mockModel.LinkedObjects.Returns(expectedTransforms);

        controller.model = mockModel;

        // Act
        Transform[] result = controller.retrieveLinkedObjects();

        // Assert
        Assert.AreEqual(expectedTransforms, result);

        // Clean up
        Object.DestroyImmediate(go);
    }


    [Test]
    public void retrieveTargetScale_ScalesExist()
    {
        // Arrange 
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        Vector3[] expectedScales = new Vector3[]
        {
           new Vector3(1.2f, 1.2f, 1.2f),
           new Vector3(0.8f, 0.8f, 0.8f)
        };

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        mockModel.TargetScales.Returns(expectedScales);

        controller.model = mockModel;

        // Act 
        Vector3[] result = controller.retrieveTargetScale();

        // Assert 
        Assert.AreEqual(expectedScales, result);

        // Clean up 
        Object.DestroyImmediate(go);
    }

    [Test]
    public void retrieveTargetScale_ScalesDoNotExist()
    {
        // Arrange 
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        Vector3[] expectedScales = null;

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        mockModel.TargetScales.Returns(expectedScales);

        controller.model = mockModel;

        // Act 
        Vector3[] result = controller.retrieveTargetScale();

        // Assert 
        Assert.AreEqual(expectedScales, result);

        // Clean up 
        Object.DestroyImmediate(go);
    }

    [Test]
    // NOTE: Because the model starts with this value set to 10f and it cannot be set to an
    // invalid value, we can just test that the value is properly retrieved from the model
    // without needing to test edge cases for invalid values
    public void retrieveScaleSpeed_SpeedSet()
    {
        // Arrange 
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        float expectedSpeed = 10f;

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        mockModel.ScaleSpeed.Returns(10f);

        controller.model = mockModel;

        // Act 
        float result = controller.retrieveScaleSpeed();

        // Assert 
        Assert.AreEqual(expectedSpeed, result);

        // Clean up 
        Object.DestroyImmediate(go);
    }


    [Test]
    public void IsHovering_ModelIsHovering_ReturnsTrue()
    {
        // Arrange
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        bool expectedState = true;

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        mockModel.IsHovering.Returns(expectedState);

        controller.model = mockModel;

        // Act
        bool result = controller.IsHovering();

        // Assert
        Assert.AreEqual(expectedState, result);

        // Clean up
        Object.DestroyImmediate(go);
    }

    [Test]
    public void IsHovering_ModelIsNotHovering_ReturnsFalse()
    {
        // Arrange
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        bool expectedState = false;

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        mockModel.IsHovering.Returns(expectedState);

        controller.model = mockModel;

        // Act
        bool result = controller.IsHovering();

        // Assert
        Assert.AreEqual(expectedState, result);

        // Clean up
        Object.DestroyImmediate(go);
    }

    [Test]
    public void OnHoverEnter_CallsModelOnHoverEnter()
    {
        // Arrange
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        controller.model = mockModel;

        // Act
        controller.OnHoverEnter();

        // Assert
        mockModel.Received(1).OnHoverEnter();

        // Clean up
        Object.DestroyImmediate(go);
    }

    [Test]
    public void OnHoverExit_CallsModelOnHoverExit()
    {
        // Arrange
        GameObject go = new GameObject();
        ScaleOnHoverController controller = go.AddComponent<ScaleOnHoverController>();

        IScaleOnHoverModel mockModel = Substitute.For<IScaleOnHoverModel>();
        controller.model = mockModel;

        // Act
        controller.OnHoverExit();

        // Assert
        mockModel.Received(1).OnHoverExit();

        // Clean up
        Object.DestroyImmediate(go);
    }

}

