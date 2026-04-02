/// <summary>
/// Interface for the MusicManager component. Manages persistent music accross scenes
/// </summary>
public interface IMusicManagerController
{
    /// <summary>
    /// Initializes the music manager, sets up the AudioSource
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///  - A MusicManager instance must not already exist in the scene
    /// </pre-condition>
    /// <post-condition>
    ///  - A MusicManager instance is created and initialized in the scene
    /// </post-condition>
    /// </remarks>
    new void Init();

    /// <summary>
    /// Called once after the scene loads
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///  - None
    /// </pre-condition>
    /// <post-condition>
    ///  - MusicManager is set to not be destroyed on load, allowing it to persist through scenes
    /// </post-condition>
    /// </remarks>
    new void Awake();
}
