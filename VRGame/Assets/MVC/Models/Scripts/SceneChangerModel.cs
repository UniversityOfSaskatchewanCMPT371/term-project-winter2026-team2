

using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


/// <summary>
/// Model portion of the scene changer service. A persistent singleton
/// </summary>
/// <remarks>
/// Only one SceneChangerModel can be instantiated at a time. `Awake()` ensures this is the case
/// </remarks>
public class SceneChangerModel : MonoBehaviour, ISceneChangerModel
{
    /// <summary>
    /// Singleton instance of the SceneChanger's model
    /// </summary>
    private static SceneChangerModel instance;


    /// <summary>
    /// Collection of paths to scenes held by the scenechanger
    /// </summary>
    private List<string> scenePaths;

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
        get
        {
            return scenePaths;
        }
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
    /// 
    /// </remarks>
    public void Init()
    {

        Assert.IsTrue(instance == null, "Cannot create second instance");
        if (instance != null & instance != this)
        {
            // In the spikeprototype implementation the gameObject this script is attached
            // to is destroyed if there's already a SceneChanger. That seems extreme but I
            // may be wrong
            //Destroy(gameObject);
            return;
        }
        scenePaths = new List<string>();
        instance = this;
    }

    /// <summary>
    /// A `MonoBehaviour` function, called once when the game starts
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// PostConditions:
    /// - `Init()` is called, changes to state from that function are made. Additionaly
    /// ensures that this object won't be destroyed when a new scene is loaded. It is persistent
    /// </remarks>
    private void Awake()
    {
        Init();
        DontDestroyOnLoad(instance);
    }


}