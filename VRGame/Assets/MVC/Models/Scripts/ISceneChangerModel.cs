
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Model portion of the scene changer service. A persistent singleton
/// </summary>
/// <remarks>
/// Only one SceneChangerModel can be instantiated at a time. `Awake()` ensures this is the case
/// </remarks>
public interface ISceneChangerModel {

    /// <summary>
    /// Public accessor for the scene path collection
    /// </summary>
    public List<string> ScenePaths
    {
        /// <summary>
        /// Access the SceneChanger's collection of scene paths
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// - SceneChangerModel's path collection instance variable is returned. The retreiver
        /// may then add or remove elements from it.
        /// </remarks>
        get;
    }


    /// <summary>
    /// Initializes the SceneChangerModel. Called by the game within the MonoBehavior function
    /// `Awake()` (executes once when the game starts) - Separated from `Start()` as this makes unit
    /// testing easier.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - If a SceneChangerModel already exists, nothing is done, the function returns. If a 
    /// SceneChangerModel doesn't exist, a single instance of it is created and a collection
    /// for holding Scenes is allocated
    /// It is ensured that this single instance will not be destroyed when a new scene is loaded.
    /// </remarks>
    public void Init();


}