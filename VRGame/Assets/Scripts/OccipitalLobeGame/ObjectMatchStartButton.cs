using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;
using ObjectMatchGame;

public class ObjectMatchStartButton : ClickableCubes
{

    [SerializeField] private StartButtonType buttonType;
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

        if (buttonType == StartButtonType.level)
        {
            controller.InitializeLevel();
        }
        else if (buttonType == StartButtonType.tutorial)
        {
            controller.InitializeTutorial();
        }
        else if (buttonType == StartButtonType.leaveTutorial)
        {
            controller.LeaveTutorial();
        }
    }
}
