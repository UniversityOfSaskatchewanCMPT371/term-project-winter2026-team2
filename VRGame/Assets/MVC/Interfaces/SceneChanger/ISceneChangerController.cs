
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller portion of scene changer service. A persistent singleton
/// </summary>
/// <remarks>
/// Only one SceneChanger can be instantiated at a time
public interface ISceneChangerController
{



    /// <summary>
    /// Public readonly accessor for loadDebounce
    /// </summary>
    public bool LoadDebounce
    {
        /// <summary>
        /// View current status of loadDebounce
        /// </summary> 
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - loadDebounce is returned 
        get;
    }


    /// <summary>
    /// Loads a scene based on the the key in the collection held by SceneChangerModel
    /// </summary>
    /// <param name="sceneKey">key, should be associated with value in SceneEnum </param>
    /// <returns>The scene to loead based on the provided key</returns>
    /// <remarks>
    /// Preconditions:
    /// - sceneKey must exist in SceneEnum
    /// Postconditions:
    /// - loadDebounce will be set to true while scene is asynchronously loaded, disallowing
    /// multiple scenes to be loaded at a time. Returns async loadScene operation within a wrapper class
    /// </remarks>
    public IAsyncOperationWrapper LoadScene(int sceneKey);


    /// <summary>
    /// Resets static singleton instance of Scenechanger. Used for unit testing purposes
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Static instance of SceneChangerController set to null
    public void ResetInstance();

    /// <summary>
    /// Initializes the SceneChangerController. Called by the game within the MonoBehavior function
    /// `Start()` (executes once when the game starts) - Separated from `Start()` as this makes unit
    /// testing easier. IMPORTANT that this is called in Start() instead of Awake(), as it depends on other components already existing
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - A sceneChangerController must not already exist.
    /// Postconditions: 
    /// If a SceneChangerModel doesn't exist, a single instance of it is created
    /// </remarks>
    public void Init();
}
