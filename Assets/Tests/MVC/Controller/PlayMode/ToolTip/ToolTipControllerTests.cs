using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolTipControllerTests
{
    private GameObject triggerObject;
    private ToolTipTrigger toolTipTrigger;
    private XRSimpleInteractable interactable;
    private GameObject toolTipGameObject;
    private ToolTipView toolTipView;
    private ToolTipModel toolTipModel;

    [UnityTest]
    public IEnumerator AwakeAndStart_DisablesInteractiveElement()
    {
        // Create a GameObject to act as the trigger for the tooltip
        triggerObject = new GameObject("Trigger");
        toolTipTrigger = triggerObject.AddComponent<ToolTipTrigger>();
        interactable = triggerObject.AddComponent<XRSimpleInteractable>();
        toolTipTrigger.interactable = interactable;

        // Create a GameObject to act as the tooltip view
        toolTipGameObject = new GameObject("ToolTip");
        toolTipView = toolTipGameObject.AddComponent<ToolTipView>();

        
        var titleObject = new GameObject("Title");
        titleObject.transform.SetParent(toolTipGameObject.transform);
        toolTipView.title = titleObject.AddComponent<TMPro.TextMeshProUGUI>();

        var descriptionObject = new GameObject("Description");
        descriptionObject.transform.SetParent(toolTipGameObject.transform);
        toolTipView.description = descriptionObject.AddComponent<TMPro.TextMeshProUGUI>();

        //assign gameobject as interactive of trigger
        toolTipTrigger.interactiveElement = toolTipGameObject;

        //create real model and assign to view data
        toolTipModel = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        toolTipModel.Title = "PlayMode Title";
        toolTipModel.Description = "PlayMode Description";
        toolTipView.data = toolTipModel;

        // Wait a frame for Awake and Start to execute
        yield return null;

        //assert that tooltip is disabled by trigger start
        Assert.IsFalse(toolTipGameObject.activeSelf, "Tooltip should be disabled on start");

        // Cleanup
        
        Object.DestroyImmediate(triggerObject);
        Object.DestroyImmediate(toolTipGameObject);
        Object.DestroyImmediate(toolTipModel);
    }
}
