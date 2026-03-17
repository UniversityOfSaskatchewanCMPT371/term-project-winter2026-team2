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
        trigger.Interactable = interactable;

        // Create a view object and link it as the interactive element
        GameObject viewGo = new GameObject("ToolTipView");
        ToolTipView view = viewGo.AddComponent<ToolTipView>();
        viewGo.transform.SetParent(triggerGo.transform);
        trigger.InteractiveElement = viewGo;

        // Give it a model so the view doesn't complain about missing data
        ToolTipModel model = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        model.Title = "Test";
        model.Description = "Test";
        view.Data = model;

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
        trigger.Interactable = interactable;

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

        trigger.InteractiveElement = dummyViewGo; // assign to avoid the interactiveElement null check

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
        // create left controller parent
        GameObject controllerGo = new GameObject("Left Controller");
        controllerGo.AddComponent<ActionBasedController>();

        // Create interactor child
        GameObject interactorGo = new GameObject("Ray Interactor");
        interactorGo.transform.SetParent(controllerGo.transform);
        XRRayInteractor rayInteractor = interactorGo.AddComponent<XRRayInteractor>();

        // create interactable then add tooltiptrigger
        GameObject triggerGo = new GameObject("Trigger");
        XRSimpleInteractable interactable = triggerGo.AddComponent<XRSimpleInteractable>();
         ToolTipTrigger trigger = triggerGo.AddComponent<ToolTipTrigger>();

        // Create view object and set as the interactive element
        GameObject viewGo = new GameObject("ToolTipView");
        ToolTipView view = viewGo.AddComponent<ToolTipView>();
        viewGo.transform.SetParent(triggerGo.transform);

        trigger.InteractiveElement = viewGo;

        // create model and assign
        ToolTipModel model = ScriptableObject.CreateInstance<ToolTipModel>();
        model.Title = "Test";
        model.Description = "Test";
        view.Data = model;

        // wait one frame for Start to run
        yield return null;

        // simulate HoverEnter event with left controller interactor
        interactable.hoverEntered.Invoke(new HoverEnterEventArgs
        {
            interactorObject = rayInteractor
        });
        Assert.IsTrue(viewGo.activeSelf, "Element should be active after hover enter.");

        // simulate HoverExit
        interactable.hoverExited.Invoke(new HoverExitEventArgs
        {
            interactorObject = rayInteractor
        });
        Assert.IsFalse(viewGo.activeSelf, "Element should be inactive after hover exit.");

        // clean up
        Object.DestroyImmediate(triggerGo);
        Object.DestroyImmediate(controllerGo);
        Object.DestroyImmediate(model);
    }
}