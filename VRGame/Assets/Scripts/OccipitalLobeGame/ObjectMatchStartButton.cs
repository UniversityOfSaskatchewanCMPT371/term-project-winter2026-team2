using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; 

public class ObjectMatchStartButton : MonoBehaviour
{
    // Store an instance of the controller so we can notify it
    // when the start button is grabbed
    private IObjectMatchGameController controller;

    // Store an instance of the grab interactable component so we can listen for when the start
    // button is grabbed
    private XRGrabInteractable grabInteractable;

    /// <summary>
    /// Store an instance of the controller so we can notify it when an option
    /// is placed in the guess box
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The controller variable is assigned to an instance of IObjectMatchGameController
    ///   found in the parent hierarchy of this game object
    /// </remarks>
    private void Start()
    {
        controller = GetComponentInParent<IObjectMatchGameController>();
        if (controller == null)
        {
            Debug.LogError("ObjectMatchStartButton could not find an instance of IObjectMatchGameController in its parent hierarchy.");
        }
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("ObjectMatchStartButton could not find an instance of XRGrabInteractable on the same game object.");
        }
        else
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }

    /// <summary>
    /// Notifies the game controller that the start button has been grabbed so it can
    /// start the game
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - controller is assigned to an instance of IObjectMatchGameController
    /// - grabInteractable is assigned to an instance of XRGrabInteractable
    /// Postconditions:
    /// - The controller's InitializeLevel method is called
    /// </remarks>
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (controller == null)
        {
            controller = GetComponentInParent<IObjectMatchGameController>();
            if (controller == null)
            {
                Debug.LogError("ObjectMatchStartButton could not find an instance of IObjectMatchGameController in its parent hierarchy.");
                return;
            }
        }
        controller.InitializeLevel();
        gameObject.SetActive(false);
    }
}
