using UnityEngine;

/// <summary>
/// Abstraction for scene loading service.
/// This allows scene loading to be mocked in unit tests.
/// </summary>
public interface ISceneChanger
{
    /// <summary>
    /// Loads the requested scene asynchronously.
    /// </summary>
    /// <param name="scene">The destination scene enum.</param>
    /// <returns>AsyncOperation representing the load request.</returns>
    AsyncOperation LoadScene(Scenes scene);
}