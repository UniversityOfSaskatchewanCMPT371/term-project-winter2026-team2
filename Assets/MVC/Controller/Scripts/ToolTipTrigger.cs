using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolTipTrigger : MonoBehaviour

{
    public GameObject interactiveElement;
    public XRBaseInteractable interactable;

    public IXRInteractable Interactable
    {
        get; set;
    }


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

        if (Interactable == null)
        {
            //use the adapter
            interactable = GetComponent<XRBaseInteractable>();
            Interactable = new XRInteractableAdapter(interactable);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        interactiveElement.SetActive(false);
        /* interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit); */
        Interactable.HoverEntered += OnHoverEnter;
        Interactable.HoverExited += OnHoverExit;
    }
    // <summary>
    // PreCondition: The hover event must be triggered by an XR controller or pointer.
    /// PostCondition: The interactive element will be shown when the hover event is entered and hidden when the hover event is exited.
    /// <param name="args"></param>
    /// 
    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        // show(true);
        interactiveElement.SetActive(true);
    }
    // <summary>
    // PreCondition: The hover event must be triggered by an XR controller or pointer.
    /// PostCondition: The interactive element will be hidden when the hover event is exited.
    /// <param name="args"></param>
    /// </summary>
    private void OnHoverExit(HoverExitEventArgs args)
    {
        // show(false);
        interactiveElement.SetActive(false);
    }

    void OnDestroy()
    {
        if (Interactable != null)
        {
            Interactable.HoverEntered -= OnHoverEnter;
            Interactable.HoverExited -= OnHoverExit;
        }
       
    }

        //commented this out i was getting Leak Detected idk it disappeared
/*      // Update is called once per frame
    void Update()
    {
        
    }  */
}
