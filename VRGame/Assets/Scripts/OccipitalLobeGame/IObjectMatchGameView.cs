using ObjectMatchGame;

public interface IObjectMatchGameView: IView
{
    /// <summary>
    /// Takes a list of object IDs corresponding to the objects that are options in
    /// the current level and displays those objects
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - ObjectIDs is a non-empty string array containing the IDs of GameObjects
    /// Postconditions:
    /// - The objects corresponding to the IDs in ObjectIDs are displayed in the scene
    /// </remarks>
    public void ShowObjects(string[] ObjectIDs);

    /// <summary>
    /// Clears all objects from the scene, making them invisible and non-interactable.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - All objects in the scene are invisible and non-interactable
    /// </remarks>
    public void ClearAllObjects();

    /// <summary>
    /// Prepares the game for displaying a level by enabling the guess box and submit button and
    /// disabling the start level and tutorial buttons.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The guess box and submit button are enabled and the start level and tutorial buttons are disabled
    /// </remarks>
    public void EnterLevel();

    /// <summary>
    /// Prepares the game for exiting a level by disabling the guess box and submit button and enabling
    /// the start level and tutorial buttons
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - The guess box and submit button are disabled and the start level and tutorial buttons are enabled
    /// </remarks>
    public void ExitLevel();
}
