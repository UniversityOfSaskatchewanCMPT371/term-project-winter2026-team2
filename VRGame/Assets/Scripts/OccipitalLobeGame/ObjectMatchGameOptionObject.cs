using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectMatchGameOptionObject : MonoBehaviour
{
    
    IObjectMatchGameController controller;

    // Store the initial position and rotation of the option object so it can be reset to
    // its original position
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 guessBoxPosition;
    private Quaternion guessBoxRotation;

    // Store an instance of the grab interactable component so we can listen the object being
    // grabbed and released
    private XRGrabInteractable grabInteractable;

    // Store an instance of the rigidbody component so we can disable physics when the object is grabbed
    private Rigidbody rigidBody;

    /// <summary>
    /// Store an instance of the controller so we can get information about the
    /// current option object
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The controller variable is assigned to an instance of IObjectMatchGameController
    ///   found in the parent hierarchy of this game object
    /// </remarks>
    void Start()
    {
        controller = GetComponentInParent<IObjectMatchGameController>();

        if (controller == null)
        {
            Debug.LogError("OptionObject could not find an instance of IObjectMatchGameController in its parent hierarchy.");
        }

        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("OptionObject could not find an instance of XRGrabInteractable on the same game object.");
        }
        else
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }

        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        guessBoxPosition = transform.parent.parent.Find("GuessBox").position;
        guessBoxRotation = transform.parent.parent.Find("GuessBox").rotation;

    }


    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (rigidBody != null)
        {
            rigidBody.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (controller == null)
        {
            controller = GetComponentInParent<IObjectMatchGameController>();
            if (controller == null)
            {
                Debug.LogError("OptionObject could not find an instance of IObjectMatchGameController in its parent hierarchy.");
                return;
            }
        }

        if (controller.GetCurrentGuessID() != gameObject.name)
        {
            // If the object released is not the current guess, reset it to its original position
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
        else
        {
            // If the object released is the current guess, move it to the guess box
            transform.position = guessBoxPosition;
            transform.rotation = guessBoxRotation;
        }

        if (rigidBody != null)
        {
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
