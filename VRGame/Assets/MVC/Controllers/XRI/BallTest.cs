using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BallTest : MonoBehaviour {
    private XRGrabInteractable grabInteractable;
    private XRIInputActions inputActions;

    private void Awake(){
        inputActions = new XRIInputActions();
    }

    void Start(){
        grabInteractable = GetComponent<XRGrabInteractable>();
        if(grabInteractable != null){
            Debug.Log("We're good to go!");
        }
    }

    private void OnEnable(){
        inputActions.Enable();
        inputActions.XRIRightInteraction.UIPress.performed += OnTrigger;
    }


    private void OnDisable(){
        inputActions.XRIRightInteraction.UIPress.performed -= OnTrigger;
        inputActions.Disable();
    }

    private void OnTrigger(InputAction.CallbackContext context){
        if(IsHeld()){
            Debug.Log("Bam!");
        }
    }

    public bool IsHeld(){
        return grabInteractable.interactorsSelecting.Count > 0;
    }
}
