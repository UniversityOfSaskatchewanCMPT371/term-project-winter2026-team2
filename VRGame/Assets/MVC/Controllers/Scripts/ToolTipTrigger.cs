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
        /*interactiveElement.SetActive(false);
          interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit); */

        //forward XR events to our own events
        interactable.hoverEntered.AddListener(_=> HoverEntered?.Invoke());
        interactable.hoverExited.AddListener(_=> HoverExited?.Invoke());

        //create controller and pass in the interactive element and this trigger
        toolTipController = new ToolTipController(interactiveElement, this);
        
    }
    // <summary>
    // PreCondition: The hover event must be triggered by an XR controller or pointer.
    /// PostCondition: The interactive element will be shown when the hover event is entered and hidden when the hover event is exited.
    /// <param name="args"></param>
    /// 
/*     private void OnHoverEnter(HoverEnterEventArgs args)
    {
        // show(true);
        interactiveElement.SetActive(true);
    } */
    // <summary>
    // PreCondition: The hover event must be triggered by an XR controller or pointer.
    /// PostCondition: The interactive element will be hidden when the hover event is exited.
    /// <param name="args"></param>
    /// </summary>
 /*    private void OnHoverExit(HoverExitEventArgs args)
    {
        // show(false);
        interactiveElement.SetActive(false);
    }
 */
    void OnDestroy()
    {
        toolTipController?.Dispose();
       
    }

        //commented this out i was getting Leak Detected idk it disappeared
     // Update is called once per frame
/*     void Update()
    {
        
    }   */
}
