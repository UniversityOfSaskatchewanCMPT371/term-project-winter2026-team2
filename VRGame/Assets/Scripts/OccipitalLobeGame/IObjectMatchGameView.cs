using ObjectMatchGame;

public interface IObjectMatchGameView
{
    /// <summary>
    /// Called when the collider box of any of the options to guess hit the
    /// guess basket collider. Calls the controller to notify it of the guess.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Notifies the controller that a guess was made
    /// </remarks>
    public void OnGuessCollision();

    /// <summary>
    /// Called when the user grabs an object that was in the guess area, removing it
    /// Notifies the controller so it can update the model
    /// </summary>
    public void removeGuess();
}
