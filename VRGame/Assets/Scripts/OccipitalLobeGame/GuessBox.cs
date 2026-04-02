using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script only exists to be attached to the guess box in the Object Match
/// game. All it does is detect when one of the options is placed inside the
/// guess box and notify the controller 
/// </summary>
public class GuessBox : MonoBehaviour
{
    // Store an instance of the controller so we can notify it when an option
    // enters the guess box
    IObjectMatchGameController controller;

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
    void Start()
    {
        controller = GetComponentInParent<IObjectMatchGameController>();

        if (controller == null)
        {
            Debug.LogError("GuessBox could not find an instance of IObjectMatchGameController in its parent hierarchy.");
        }
    }

    /// <summary>
    /// When an option is placed in the guess box, notify the controller which object
    /// was placed in the box so it can evaluate whether the guess was correct or not
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The controller variable is assigned to an instance of IObjectMatchGameController
    /// - grabInteractable is assigned to an instance of XRGrabInteractable on the game object
    ///   corresponding to the option that entered the box
    /// Postconditions:
    /// - The controller's checkGuess method is called with the name of the game object that
    ///   was placed in the guess box as an argument
    /// </remarks>
    private void OnTriggerEnter(Collider other)
    {
        if (controller == null)
        {
            controller = GetComponentInParent<IObjectMatchGameController>();
            if (controller == null)
            {
                Debug.LogError("GuessBox could not find an instance of IObjectMatchGameController in its parent hierarchy.");
                return;
            }
        }
        if (controller.GetCurrentGuessID() != "")
        {
            Debug.Log("Cannot have two guess at once. Remove the current guess before" +
                "placing another object in the box.");
            return;
        }
        controller.PotentialGuess(other.gameObject.name);
    }

    /// <summary>
    /// Notify the controller when the object is removed from the guess box.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The controller variable is assigned to an instance of IObjectMatchGameController
    /// - grabInteractable is assigned to an instance of XRGrabInteractable on the game object
    ///   corresponding to the option that exited the box
    /// Postconditions:
    /// - If the item that exited was the current guess, tell the model to remove it. Else, do nothing
    /// </remarks>
    private void OnTriggerExit(Collider other)
    {
        if (controller == null)
        {
            controller = GetComponentInParent<IObjectMatchGameController>();
            if (controller == null)
            {
                Debug.LogError("GuessBox could not find an instance of IObjectMatchGameController in its parent hierarchy.");
                return;
            }
        }

        if (other.gameObject.name == controller.GetCurrentGuessID())
        {
            controller.RemovePotentialGuess();
            Debug.Log("Removed " + other.gameObject.name + " as the current guess because it was removed from the guess box.");
        }
        else
        {
            Debug.LogWarning("An object with name " + other.gameObject.name + " exited the guess box, but that object was not registered as the current guess.");
        }
    }
}
