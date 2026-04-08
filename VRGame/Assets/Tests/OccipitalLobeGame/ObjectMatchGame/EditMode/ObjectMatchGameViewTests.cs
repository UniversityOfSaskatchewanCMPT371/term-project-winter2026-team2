using System;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;
using TMPro;

/// <summary>
/// Unit tests for ObjectMatchGameView class following the isolated Arrange-Act-Assert style.
/// </summary>
public class ObjectMatchGameViewTests
{
    /// <summary>
    /// Test the basic instantiation and reference assignment.
    /// </summary>
    [Test]
    public void Instantiation()
    {
        // Arrange
        GameObject go = new GameObject();

        // Act
        ObjectMatchGameView view = go.AddComponent<ObjectMatchGameView>();
        IObjectMatchGameController mockController = Substitute.For<IObjectMatchGameController>();
        view.ControllerMock = mockController;

        // Assert
        Assert.NotNull(view, "View component should be successfully added to GameObject.");

        // Cleanup
        UnityEngine.Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test that Init() deactivates the option objects and UI elements.
    /// </summary>
    [Test]
    public void Init_ValidPreconditions()
{
    // Arrange
    GameObject go = new GameObject();
    ObjectMatchGameView view = go.AddComponent<ObjectMatchGameView>();
    
    // 1. Injected UI Dummies
    GameObject dummyBox = new GameObject("Box");
    GameObject dummyBtn = new GameObject("Btn");
    view.GuessBox = dummyBox;
    view.SubmitButton = dummyBtn;
    view.StartLevelButton = dummyBtn;
    view.StartTutorialButton = dummyBtn;
    view.LeaveTutorialButton = dummyBtn;
    view.InLevelDisplay = dummyBtn.AddComponent<TextMeshProUGUI>();
    view.OutOfLevelDisplay = dummyBtn.GetComponent<TextMeshProUGUI>();

        // 2. Setup Objects
        GameObject obj = new GameObject("TestObj");
    obj.SetActive(true);
    view.AllObjects = new GameObject[] { obj };

    // 3. Setup Controller Mock
    IObjectMatchGameController mockController = Substitute.For<IObjectMatchGameController>();
    view.ControllerMock = mockController;

    // Act
    view.Init();

    // Assert
    Assert.IsFalse(obj.activeSelf, "Expected assigned game objects to be deactivated on Init.");
    
    // Cleanup
    UnityEngine.Object.DestroyImmediate(obj);
    UnityEngine.Object.DestroyImmediate(dummyBox);
    UnityEngine.Object.DestroyImmediate(dummyBtn);
    UnityEngine.Object.DestroyImmediate(go);
}

    /// <summary>
    /// Test that ShowObjects activates the correct objects matching the ID list.
    /// </summary>
    [Test]
    public void ShowObjects_ValidLayersRef()
{
    // Arrange
    GameObject go = new GameObject();
    ObjectMatchGameView view = go.AddComponent<ObjectMatchGameView>();
    
    // 1. Injected UI Dummies
    GameObject dummyBox = new GameObject("Box");
    GameObject dummyBtn = new GameObject("Btn");
    view.GuessBox = dummyBox;
    view.SubmitButton = dummyBtn;

    // 2. Setup Objects
    GameObject target = new GameObject("TargetID");
    GameObject decoy = new GameObject("DecoyID");
    target.SetActive(false);
    decoy.SetActive(true);
    view.AllObjects = new GameObject[] { target, decoy };

    // 3. Setup Controller Mock
    view.ControllerMock = Substitute.For<IObjectMatchGameController>();

    // Act
    view.ShowObjects(new string[] { "TargetID" });

    // Assert
    Assert.IsTrue(target.activeSelf, "The target object matching the ID should be activated.");
    Assert.IsTrue(dummyBox.activeSelf, "The guess box should be activated.");

    // Cleanup
    UnityEngine.Object.DestroyImmediate(target);
    UnityEngine.Object.DestroyImmediate(decoy);
    UnityEngine.Object.DestroyImmediate(dummyBox);
    UnityEngine.Object.DestroyImmediate(dummyBtn);
    UnityEngine.Object.DestroyImmediate(go);
}


    /// <summary>
    /// Test to see if Init() will log a warning when the controller is missing.
    /// </summary>
    [Test]
    public void Init_MissingController_LogsWarning()
{
    // Arrange
    GameObject go = new GameObject();
    ObjectMatchGameView view = go.AddComponent<ObjectMatchGameView>();

    // Provide dummies to avoid NullReferenceException
    view.AllObjects = new GameObject[0];
    GameObject dummyBox = new GameObject();
    GameObject dummyBtn = new GameObject();
    view.GuessBox = dummyBox;
    view.SubmitButton = dummyBtn;

    // Act & Assert
    // 1. Expect the specific warning log from the base class
    LogAssert.Expect(LogType.Warning, new Regex(".*No matching component.*"));

    // 2. Wrap in Assert.Throws because the base class calls Assert.IsNotNull
    Assert.Throws<NUnit.Framework.AssertionException>(() =>
    {
        view.Init();
    });

    // Cleanup
    UnityEngine.Object.DestroyImmediate(dummyBox);
    UnityEngine.Object.DestroyImmediate(dummyBtn);
    UnityEngine.Object.DestroyImmediate(go);
}

    /// <summary>
    /// Test ShowObjects when an empty ID list is provided.
    /// </summary>
   [Test]
    public void ShowObjects_EmptyArray_LeavesAsIs()
{
    // Arrange
    GameObject go = new GameObject();
    ObjectMatchGameView view = go.AddComponent<ObjectMatchGameView>();
    
    // Create dummy UI objects so the View doesn't crash on NullReference
    GameObject dummyGuessBox = new GameObject("GuessBox");
    GameObject dummySubmitBtn = new GameObject("SubmitBtn");
    view.GuessBox = dummyGuessBox;
    view.SubmitButton = dummySubmitBtn;

    GameObject obj1 = new GameObject("A");
    obj1.SetActive(true);
    view.AllObjects = new GameObject[] { obj1 };
    view.ControllerMock = Substitute.For<IObjectMatchGameController>();

    // Act
    view.ShowObjects(new string[] { });

    // Assert
    Assert.IsTrue(obj1.activeSelf, "Object should be deactivated when not in the ID list.");
    Assert.IsTrue(dummyGuessBox.activeSelf, "Guess box should be enabled even if list is empty.");

    // Cleanup
    UnityEngine.Object.DestroyImmediate(obj1);
    UnityEngine.Object.DestroyImmediate(dummyGuessBox);
    UnityEngine.Object.DestroyImmediate(dummySubmitBtn);
    UnityEngine.Object.DestroyImmediate(go);
}
}