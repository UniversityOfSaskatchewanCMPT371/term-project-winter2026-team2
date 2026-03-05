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
    IObjectMatchGameController controller;
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
        controller.checkGuess(other.gameObject.name);
    }
}
