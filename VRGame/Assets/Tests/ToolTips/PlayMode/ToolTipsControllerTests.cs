using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;
using System.Text.RegularExpressions;


/// <summary>
/// Play mode tests for ToolTipController.
/// </summary>
public class ToolTipsControllerTests
{
    /// <summary>
    /// Verifies that when the controller is created, it disables the interactive element.
    /// </summary>
    [UnityTest]
    public IEnumerator AwakeAndStart_DisablesInteractiveElement()
    {
        // Build a trigger object with a real XRSimpleInteractable
        GameObject triggerGo = new GameObject("Trigger");
        XRSimpleInteractable interactable = triggerGo.AddComponent<XRSimpleInteractable>();
        
        //add ToolTIpTrigger component
        ToolTipTrigger trigger = triggerGo.AddComponent<ToolTipTrigger>();
        trigger.interactable = interactable;

        // Create a view object and link it as the interactive element
        GameObject viewGo = new GameObject("ToolTipView");
        ToolTipView view = viewGo.AddComponent<ToolTipView>();
        viewGo.transform.SetParent(triggerGo.transform);
        trigger.interactiveElement = viewGo;

        // Give it a model so the view doesn't complain about missing data
        ToolTipModel model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
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

    /// <summary>
    /// Verifies that Start asserts if interactiveElement is not assigned
    /// </summary>
    [UnityTest]
    public IEnumerator Start_MissingInteractiveElement_Asserts()
    {
        // Build a trigger object with a real XRSimpleInteractable but no interactive element
        GameObject triggerGo = new GameObject("Trigger");
        XRSimpleInteractable interactable = triggerGo.AddComponent<XRSimpleInteractable>();
        
        //add ToolTipTrigger component but leave interactiveElement unassigned
        ToolTipTrigger trigger = triggerGo.AddComponent<ToolTipTrigger>();
        trigger.interactable = interactable;

        //expect an assertion exceiption about missing interactiveElement
        LogAssert.Expect(LogType.Assert, new Regex(".*interactiveElement must be assigned.*"));

        //wait one frame
        yield return null;

        //clean up
        UnityEngine.Object.DestroyImmediate(triggerGo);
    }

    /// <summary>
    /// Verifies that Awake asserts if XRBaseInteractable is missing.
    /// </summary>
    [UnityTest]
    public IEnumerator Awake_MissingInteractable()
    {
        // create a GameObject for the trigger without an XRBaseInteractable
        GameObject triggerGo = new GameObject("Trigger");
        GameObject dummyViewGo = new GameObject("DummyView");
        dummyViewGo.transform.SetParent(triggerGo.transform);
        
        // add the ToolTipTrigger component - awake will run automatically
        ToolTipTrigger trigger = triggerGo.AddComponent<ToolTipTrigger>();

        trigger.interactiveElement = dummyViewGo; // assign to avoid the interactiveElement null check

        // expect an assertion exception about missing XRBaseInteractable
        LogAssert.Expect(LogType.Assert, new Regex(".*No XRBaseInteractable component found.*"));
        LogAssert.Expect(LogType.Error, new Regex(".*XRBaseInteractable component is missing.*"));

        // wait one frame
        yield return null;

        // clean up
        UnityEngine.Object.DestroyImmediate(triggerGo);
    }

    /// <summary>
    /// Verifies that hover events properly show and hide the interactive element.
    /// </summary>
    [UnityTest]
    public IEnumerator HoverEvents_ShowAndHideElement()
    {
        // create GameObject and add components
        GameObject triggerGo = new GameObject("Trigger");
        XRSimpleInteractable interactable = triggerGo.AddComponent<XRSimpleInteractable>();
        ToolTipTrigger trigger = triggerGo.AddComponent<ToolTipTrigger>();
        trigger.interactable = interactable;

        // Create a view object and set as the interactive element
        GameObject viewGo = new GameObject("ToolTipView");
        ToolTipView view = viewGo.AddComponent<ToolTipView>();
        viewGo.transform.SetParent(triggerGo.transform);
        trigger.interactiveElement = viewGo;

        // create model and assign
        ToolTipModel model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        model.Title = "Test";
        model.Description = "Test";
        view.data = model;

        // wait one frame for Start to run
        yield return null;

        // simulate hover enter
        interactable.hoverEntered.Invoke(new HoverEnterEventArgs());
        Assert.IsTrue(viewGo.activeSelf, "Element should be active after hover enter.");

        // simulate hover exit
        interactable.hoverExited.Invoke(new HoverExitEventArgs());
        Assert.IsFalse(viewGo.activeSelf, "Element should be inactive after hover exit.");

        // clean up
        UnityEngine.Object.DestroyImmediate(triggerGo);
        UnityEngine.Object.DestroyImmediate(model);
    }
}