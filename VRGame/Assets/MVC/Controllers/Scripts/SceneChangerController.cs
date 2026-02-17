
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller portion of scene changer service. A persistent singleton
/// </summary>
/// <remarks>
/// Only one SceneChanger can be instantiated at a time
public class SceneChangerController : MonoBehaviour, ISceneChangerController
{

    /// <summary>
    /// Singleton instance of the SceneChanger's controller
    /// </summary>
    private static SceneChangerController instance;

    /// <summary>
    /// Prevents multiple scene loads from being triggered at once
    /// </summary>
    private static bool loadDebounce;



    /// <summary>
    /// Resets the static debounce value
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// loadDebounce instance variable set to false
    /// </remarks>
    private void resetDebounce()
    {
        loadDebounce = false;
    }

    /// <summary>
    /// Loads a scene based on the the key in the collection held by SceneChangerModel
    /// </summary>
    /// <param name="sceneKey">key, should be associated with value in SceneChagnerModel's collection </param>
    /// <returns>The scene to loead based on the provided key</returns>
    /// <remarks>
    /// Preconditions:
    /// - sceneKey must exist in sceneChangerModel's collection
    /// Postconditions:
    /// - loadDebounce will be set to true while scene is asynchronously loaded, disallowing
    /// multiple scenes to be loaded at a time
    /// </remarks>
    public AsyncOperation LoadScene(int sceneKey)
    {
        if (loadDebounce)
        {
            Debug.Log("Enter triggered again");
            return null;
        }
        loadDebounce = true;

        // check if sceneKey is legal in enum
        if (!Enum.IsDefined(typeof(SceneEnum), sceneKey))
        {
            Debug.LogError("Invalid sceneKey passed to LoadScene. Not in enum");
        }
        Assert.IsTrue(Enum.IsDefined(typeof(SceneEnum), sceneKey));

        // load scene using build index model
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneKey);

        // reset debounce when the scene finishes loading
        loadingScene.completed += (o) => resetDebounce();

        return loadingScene;
    }


    /// <summary>
    /// Initializes the SceneChangerController. Called by the game within the MonoBehavior function
    /// `Start()` (executes once when the game starts) - Separated from `Start()` as this makes unit
    /// testing easier. Important that this is called in Start() instead of Awake(), as it depends on other components already existing
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - A sceneChangerController must not already exist.
    /// Postconditions: 
    /// If a SceneChangerModel doesn't exist, a single instance of it is created
    /// </remarks>
    public void Init()
    {

        if (instance != null)
        {
            Debug.LogError("SceneChangerController instance already exists");
        }
        Assert.IsNull(instance, "static var instance should be null, only one sceneChangerController may exist at a time");
        
        instance = this;
        Debug.Log("SceneChangerController initialized");
    }

    /// <summary>
    /// Resets static singleton instance of Scenechanger. Used for unit testing purposes
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - None
    /// Postconditions:
    /// - Static instance of SceneChangerController set to null
    public void ResetInstance()
    {
        instance = null;
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
    private void Start()
    {
        Init();
        DontDestroyOnLoad(instance);
    }
}