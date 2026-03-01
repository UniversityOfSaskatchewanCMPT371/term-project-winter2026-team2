using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This test runs the whole ToolTipTrigger in a real scene with an XRSimpleInteractable.
/// It ensures that Awake and Start run without errors, and that the interactive element starts disabled
/// </summary>
public class ToolTipsControllerTests
{
    /// <summary>
    /// Create a real ToolTipTrigger with XRSimpleInteractable and ToolTipView as the interactive element.
    /// After Awake and Start run, verifies the interactive element is disabled (correct initial state).
    /// </summary>
    [UnityTest]
    public IEnumerator AwakeAndStart_DisablesInteractiveElement()
    {
        // Build a trigger object with a real XRSimpleInteractable
        var triggerGo = new GameObject("Trigger");
        var interactable = triggerGo.AddComponent<XRSimpleInteractable>();
        var trigger = triggerGo.AddComponent<ToolTipTrigger>();
        trigger.interactable = interactable;

        // Create a view object and link it as the interactive element
        var viewGo = new GameObject("ToolTipView");
        var view = viewGo.AddComponent<ToolTipView>();
        viewGo.transform.SetParent(triggerGo.transform);
        trigger.interactiveElement = viewGo;

        // Give it a model so the view doesn't complain about missing data
        var model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        model.Title = "Test";
        model.Description = "Test";
        view.data = model;

        // Wait a frame – Awake and Start run automatically
        yield return null;

        // After Start, the interactive element should be disabled (because no hover yet)
        Assert.IsFalse(viewGo.activeSelf);

        // Clean up
        UnityEngine.Object.DestroyImmediate(triggerGo);
        UnityEngine.Object.DestroyImmediate(model);
    }
}