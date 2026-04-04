using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit; 

public class ObjectMatchStartButton : ClickableCubes
{
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
    public override void OnGrabbed(SelectEnterEventArgs args)
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
