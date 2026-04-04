using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script only exists to be attached to the guess box in the Object Match
/// game. All it does is detect when one of the options is placed inside the
/// guess box and notify the controller 
/// </summary>
public class GuessBox : View<IController>, IGuessBox
{
    // Store an instance of the controller so we can notify it when an option
    // enters the guess box
    IObjectMatchGameController controller;

    /// </inheritdoc>
    public override void Init()
    {
        controller = GetComponentInParent<IObjectMatchGameController>();

        if (controller == null)
        {
            Debug.LogError("GuessBox could not find an instance of IObjectMatchGameController in its parent hierarchy.");
        }
    }

    /// </inheritdoc>
    public void OnTriggerEnter(Collider other)
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

    /// </inheritdoc>
    public void OnTriggerExit(Collider other)
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
