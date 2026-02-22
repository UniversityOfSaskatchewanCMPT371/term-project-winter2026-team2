using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BallTest : MonoBehaviour {
    private XRGrabInteractable grabInteractable;

    // Start is called before the first frame update
    void Start(){
        grabInteractable = GetComponent<XRGrabInteractable>();
        if(grabInteractable != null){
            Debug.Log("We're good to go!");
        }
        
    }

    private void OnTrigger(InputAction.CallbackContext context){
        Debug.Log("Bam!");
    }
}
