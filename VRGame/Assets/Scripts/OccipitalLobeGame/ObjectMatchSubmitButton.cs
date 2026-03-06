using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectMatchSubmitButton : ClickableCubes
{
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
