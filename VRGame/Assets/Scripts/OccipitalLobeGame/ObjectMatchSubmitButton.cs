using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Script which is attachd to a physical GameObject. When the GameObject is grabbed,
/// it notifies the game controller that the player has submitted their guess and the
/// model should check it for correctness and update the game state accordingly.
/// </summary>
public class ObjectMatchSubmitButton : ClickableCubes
{

    /// <summary>
    /// Called when the player grabs the GameObject this script is attached to. 
    /// It notifies the game controller that the player has submitted their guess and the
    /// model should check it for correctness and update the game state accordingly.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - The GameObject this script is attached to must have a GrabInteractable component.
    /// - There must be an instance of IObjectMatchGameController in the parent hierarchy of this GameObject.
    /// - There must be a valid guess to submit when this method is called
    /// Postconditions:
    /// - The model is updated to reflect that the player has submitted their guess, and the 
    ///   game state is updated accordingly (e.g., checking if the guess is correct, updating score,
    ///   progressing to the next level, etc.)
    /// </remarks>
    public override void OnGrabbed(SelectEnterEventArgs args)
    {
        if (controller == null)
        {
            controller = GetComponentInParent<IObjectMatchGameController>();
            if (controller == null)
            {
                Debug.LogError("ObjectMatchSubmitButton could not find an instance of IObjectMatchGameController in its parent hierarchy.");
                return;
            }
        }

        grabInteractable.interactionManager.SelectExit(args.interactorObject, args.interactableObject);

        controller.SubmitGuess();
    }
}
