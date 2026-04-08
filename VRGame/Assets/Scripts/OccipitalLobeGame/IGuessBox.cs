
using UnityEngine;

public interface IGuessBox : IView
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
    new void Init();

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
    public void OnTriggerEnter(Collider other);

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
    public void OnTriggerExit(Collider other);
}
