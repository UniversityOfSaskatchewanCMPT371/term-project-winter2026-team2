using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class ToolTipTrigger : MonoBehaviour, IToolTipTrigger

{
    public GameObject interactiveElement;
    public XRBaseInteractable interactable;

    public event Action HoverEntered;
    public event Action HoverExited;

    private ToolTipController toolTipController;


    //<summary>
    // PreCondition: The interactiveElement GameObject must be assigned in the Unity Editor.
    ///
    ///PostCondition: The interactive element will be shown or hidden based on the provided state.
    /// 
    /// <param name="state"></param>
    // Shows or hides the interactive element based on the provided state.
    // </summary>
/*     public void show(bool state)
    {
        interactiveElement.SetActive(state);
    } */
    void Awake()
    {
         interactable = GetComponent<XRBaseInteractable>();
 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //forward XR events to our own events
        interactable.hoverEntered.AddListener(_=> HoverEntered?.Invoke());
        interactable.hoverExited.AddListener(_=> HoverExited?.Invoke());

        //create controller and pass in the interactive element and this trigger
        toolTipController = new ToolTipController(interactiveElement, this);
        
    }

    void OnDestroy()
    {
        toolTipController?.Dispose();
       
    }

}
