using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class ClickableCubes : MonoBehaviour
{
    // Store an instance of the controller so we can notify it
    // when the start button is grabbed
    public IObjectMatchGameController controller;

    // Store an instance of the grab interactable component so we can listen for when the start
    // button is grabbed
    public XRGrabInteractable grabInteractable;

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

    abstract public void OnGrabbed(SelectEnterEventArgs args);
}
