

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
    private Dictionary<int, string> scenePaths;

    /// <summary>
    /// Public accessor for the scene path collection
    /// </summary>
    public Dictionary<int, string> ScenePaths
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
    /// Retreive pathname from scenePaths collection from associated key
    /// </summary>
    /// <param name="key"> Key associated with value in scenePaths </param>
    /// <returns> Pathname associated with passed in key in scenePaths </returns>
    /// <remarks>
    /// Preconditions:
    /// - key must exist in scenePaths
    /// Postconditions:
    /// - value associated with key is returned. ScenePaths in unmodified
    public string GetStringPath(int key)
    {
        if (!scenePaths.ContainsKey(key))
        {
            Debug.LogError("key does not exist in scenePaths dict");
        }
        Assert.IsTrue(scenePaths.ContainsKey(key));

        string pathName = scenePaths[key];
        Assert.IsNotNull(pathName);

        return pathName;
    }


    /// <summary>
    /// Initializes the SceneChangerModel. Called by the game within the MonoBehavior function
    /// `Awake()` (executes once when the game starts) - Separated from `Awake()` as this makes unit
    /// testing easier.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Another sceneChangerModel must not exist
    /// Postconditions:
    /// If a SceneChangerModel doesn't exist, a single instance of it is created and a collection
    /// for holding Scenes is allocated
    /// 
    /// </remarks>
    public void Init()
    {

        if (instance != null)
        {
            Debug.LogError("SceneChangerModel instance already exists");
        }
        Assert.IsTrue(instance == null, "Cannot create second instance");
        //if (instance != null & instance != this)
        //{
            // In the spikeprototype implementation the gameObject this script is attached
            // to is destroyed if there's already a SceneChanger. That seems extreme but I
            // may be wrong
            //Destroy(gameObject);
            //return;
        //}

        scenePaths = new Dictionary<int, string>();
        instance = this;

        Debug.Log("SceneChangerModel initialized");
    }

    /// <summary>
    /// A `MonoBehaviour` function, called once when the game starts
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - All that is required for Init() to properly execute
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