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
}
