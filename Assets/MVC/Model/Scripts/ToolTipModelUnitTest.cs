using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSubstitute;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Unit tests for the ToolTipModel ScriptableObject.
/// Tests data integrity, initialization, and property behavior.
/// Runs automatically when attached to a GameObject or called manually.
/// You can run it by Seeing Tests Window, adn Press Run ToolTipModel Tests 
/// </summary>
public class ToolTipModelUnitTest : MonoBehaviour
{
    private ToolTipModel _toolTipModel;
    private int _totalTests = 0;
    private int _passedTests = 0;
    private int _failedTests = 0;

    void Start()
    {
        RunAllTests();
    }

    /// <summary>
    /// Runs all unit tests and logs results to console.
    /// </summary>
    public void RunAllTests()
    {
        Debug.Log("=== Starting ToolTipModel Unit Tests ===");
        _totalTests = 0;
        _passedTests = 0;
        _failedTests = 0;

        ToolTipModel_ShouldBeCreated_WhenInstantiated();
        Title_ShouldBeNull_WhenInitialized();
        Title_ShouldBeSet_WhenAssignedValidString();
        Title_ShouldAcceptEmptyString();
        Description_ShouldBeNull_WhenInitialized();
        Description_ShouldBeSet_WhenAssignedValidString();
        Description_ShouldAcceptMultilineText();
        Description_ShouldAcceptEmptyString();
        ToolTipModel_ShouldMaintainBothProperties_WhenSetSimultaneously();
        Title_ShouldBeOverwritten_WhenReassigned();
        Description_ShouldBeOverwritten_WhenReassigned();
        Title_ShouldAcceptSpecialCharacters();
        Description_ShouldAcceptLongText();
        ToolTipModel_ShouldAllowNullAssignment_ForTitle();
        ToolTipModel_ShouldAllowNullAssignment_ForDescription();

        Debug.Log($"=== Test Results: {_passedTests}/{_totalTests} Passed, {_failedTests}/{_totalTests} Failed ===");
    }

    private void SetUp()
    {
        _toolTipModel = ScriptableObject.CreateInstance<ToolTipModel>();
    }

    private void TearDown()
    {
        if (_toolTipModel != null)
        {
            DestroyImmediate(_toolTipModel);
        }
    }

    private void AssertIsNotNull(object obj, string message = "")
    {
        if (obj == null)
        {
            throw new System.Exception($"Assert failed: Object is null. {message}");
        }
    }

    private void AssertIsNull(object obj, string message = "")
    {
        if (obj != null)
        {
            throw new System.Exception($"Assert failed: Object is not null. {message}");
        }
    }

    private void AssertAreEqual<T>(T expected, T actual, string message = "")
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new System.Exception($"Assert failed: Expected '{expected}', but got '{actual}'. {message}");
        }
    }

    private void AssertAreNotEqual<T>(T expected, T actual, string message = "")
    {
        if (EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new System.Exception($"Assert failed: Values should not be equal. {message}");
        }
    }

    private void AssertIsTrue(bool condition, string message = "")
    {
        if (!condition)
        {
            throw new System.Exception($"Assert failed: Condition is false. {message}");
        }
    }

    private void AssertIsEmpty(string str, string message = "")
    {
        if (str == null || str.Length > 0)
        {
            throw new System.Exception($"Assert failed: String is not empty. {message}");
        }
    }

    private void RunTest(System.Action testMethod, string testName)
    {
        _totalTests++;
        SetUp();
        
        try
        {
            testMethod.Invoke();
            _passedTests++;
            Debug.Log("PASS:</color> {testName}");
        }
        catch (System.Exception ex)
        {
            _failedTests++;
            Debug.LogError($"FAIL:{testName}\n{ex.Message}");
        }
        finally
        {
            TearDown();
        }
    }

    public void ToolTipModel_ShouldBeCreated_WhenInstantiated()
    {
        RunTest(() =>
        {
            // Assert
            AssertIsNotNull(_toolTipModel);
            AssertIsTrue(_toolTipModel is ToolTipModel);
            AssertIsTrue(_toolTipModel is ScriptableObject);
        }, "ToolTipModel_ShouldBeCreated_WhenInstantiated");
    }

    public void Title_ShouldBeNull_WhenInitialized()
    {
        RunTest(() =>
        {
            // Assert
            AssertIsNull(_toolTipModel.title);
        }, "Title_ShouldBeNull_WhenInitialized");
    }

    public void Title_ShouldBeSet_WhenAssignedValidString()
    {
        RunTest(() =>
        {
            // Arrange
            string expectedTitle = "VR Interaction Tooltip";

            // Act
            _toolTipModel.title = expectedTitle;

            // Assert
            AssertAreEqual(expectedTitle, _toolTipModel.title);
        }, "Title_ShouldBeSet_WhenAssignedValidString");
    }

    public void Title_ShouldAcceptEmptyString()
    {
        RunTest(() =>
        {
            // Arrange
            string emptyTitle = "";

            // Act
            _toolTipModel.title = emptyTitle;

            // Assert
            AssertAreEqual(emptyTitle, _toolTipModel.title);
            AssertIsEmpty(_toolTipModel.title);
        }, "Title_ShouldAcceptEmptyString");
    }

    public void Description_ShouldBeNull_WhenInitialized()
    {
        RunTest(() =>
        {
            // Assert
            AssertIsNull(_toolTipModel.description);
        }, "Description_ShouldBeNull_WhenInitialized");
    }

    public void Description_ShouldBeSet_WhenAssignedValidString()
    {
        RunTest(() =>
        {
            // Arrange
            string expectedDescription = "This tooltip provides information about VR interactions.";

            // Act
            _toolTipModel.description = expectedDescription;

            // Assert
            AssertAreEqual(expectedDescription, _toolTipModel.description);
        }, "Description_ShouldBeSet_WhenAssignedValidString");
    }

    public void Description_ShouldAcceptMultilineText()
    {
        RunTest(() =>
        {
            // Arrange
            string multilineDescription = "Line 1: Introduction\nLine 2: Details\nLine 3: Instructions";

            // Act
            _toolTipModel.description = multilineDescription;

            // Assert
            AssertAreEqual(multilineDescription, _toolTipModel.description);
            AssertIsTrue(_toolTipModel.description.Contains("\n"));
        }, "Description_ShouldAcceptMultilineText");
    }

    public void Description_ShouldAcceptEmptyString()
    {
        RunTest(() =>
        {
            // Arrange
            string emptyDescription = "";

            // Act
            _toolTipModel.description = emptyDescription;

            // Assert
            AssertAreEqual(emptyDescription, _toolTipModel.description);
            AssertIsEmpty(_toolTipModel.description);
        }, "Description_ShouldAcceptEmptyString");
    }

    public void ToolTipModel_ShouldMaintainBothProperties_WhenSetSimultaneously()
    {
        RunTest(() =>
        {
            // Arrange
            string expectedTitle = "Grab Object";
            string expectedDescription = "Use the trigger button to grab and release this object.";

            // Act
            _toolTipModel.title = expectedTitle;
            _toolTipModel.description = expectedDescription;

            // Assert
            AssertAreEqual(expectedTitle, _toolTipModel.title);
            AssertAreEqual(expectedDescription, _toolTipModel.description);
        }, "ToolTipModel_ShouldMaintainBothProperties_WhenSetSimultaneously");
    }

    public void Title_ShouldBeOverwritten_WhenReassigned()
    {
        RunTest(() =>
        {
            // Arrange
            string initialTitle = "Initial Title";
            string newTitle = "Updated Title";

            // Act
            _toolTipModel.title = initialTitle;
            _toolTipModel.title = newTitle;

            // Assert
            AssertAreEqual(newTitle, _toolTipModel.title);
            AssertAreNotEqual(initialTitle, _toolTipModel.title);
        }, "Title_ShouldBeOverwritten_WhenReassigned");
    }

    public void Description_ShouldBeOverwritten_WhenReassigned()
    {
        RunTest(() =>
        {
            // Arrange
            string initialDescription = "Initial description text.";
            string newDescription = "Updated description text.";

            // Act
            _toolTipModel.description = initialDescription;
            _toolTipModel.description = newDescription;

            // Assert
            AssertAreEqual(newDescription, _toolTipModel.description);
            AssertAreNotEqual(initialDescription, _toolTipModel.description);
        }, "Description_ShouldBeOverwritten_WhenReassigned");
    }

    public void Title_ShouldAcceptSpecialCharacters()
    {
        RunTest(() =>
        {
            // Arrange
            string specialTitle = "VR: Grab & Release!";

            // Act
            _toolTipModel.title = specialTitle;

            // Assert
            AssertAreEqual(specialTitle, _toolTipModel.title);
        }, "Title_ShouldAcceptSpecialCharacters");
    }

    public void Description_ShouldAcceptLongText()
    {
        RunTest(() =>
        {
            // Arrange
            string longDescription = new string('A', 1000);

            // Act
            _toolTipModel.description = longDescription;

            // Assert
            AssertAreEqual(longDescription, _toolTipModel.description);
            AssertAreEqual(1000, _toolTipModel.description.Length);
        }, "Description_ShouldAcceptLongText");
    }

    public void ToolTipModel_ShouldAllowNullAssignment_ForTitle()
    {
        RunTest(() =>
        {
            // Arrange
            _toolTipModel.title = "Some Title";

            // Act
            _toolTipModel.title = null;

            // Assert
            AssertIsNull(_toolTipModel.title);
        }, "ToolTipModel_ShouldAllowNullAssignment_ForTitle");
    }

    public void ToolTipModel_ShouldAllowNullAssignment_ForDescription()
    {
        RunTest(() =>
        {
            // Arrange
            _toolTipModel.description = "Some Description";

            // Act
            _toolTipModel.description = null;

            // Assert
            AssertIsNull(_toolTipModel.description);
        }, "ToolTipModel_ShouldAllowNullAssignment_ForDescription");
    }

#if UNITY_EDITOR
    [MenuItem("Tests/Run ToolTipModel Tests")]
    public static void RunTestsFromMenu()
    {
        var go = new GameObject("TestRunner");
        var tester = go.AddComponent<ToolTipModelUnitTest>();
        tester.RunAllTests();
        DestroyImmediate(go);
    }
#endif
}
