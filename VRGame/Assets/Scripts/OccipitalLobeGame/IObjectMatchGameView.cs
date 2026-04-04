using ObjectMatchGame;

public interface IObjectMatchGameView: IView
{
    /// <summary>
    /// Initializes the view by deactivating all objects in the game and checking for a 
    /// reference to the controller. Called when the object is instantiated.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Has a valid reference to the controller
    /// Postconditions:
    /// - All options for game levels are hidden
    /// </remarks>
    new public void Init();

    /// <summary>
    /// Takes a list of object IDs corresponding to the objects that are options in
    /// the current level and displays those objects
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - ObjectIDs is a non-empty string array containing the IDs of GameObjects
    /// - ObjectIDs only contains IDs of GameObjects that are in the game scene and can be displayed
    /// - ObjectIDs in not null
    /// Postconditions:
    /// - The objects corresponding to the IDs in ObjectIDs are displayed in the scene
    /// </remarks>
    public void ShowObjects(string[] ObjectIDs);
}
