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


}

