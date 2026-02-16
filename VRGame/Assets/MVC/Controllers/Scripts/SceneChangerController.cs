
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
    /// Reference to associated SceneChangerModel
    /// </summary>
    private ISceneChangerModel sceneChangerModel;

    /// <summary>
    /// Public accessor for the sceneChangerModel reference. For testing purposes
    /// </summary>
    public ISceneChangerModel SceneChangerModel
    {
        /// <summary>
        /// Set the reference of the associated model portion of SceneChanger
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - value must not be null
        /// </remarks>
        /// Postconditions:
        /// SceneChangerController's sceneChangerModel instance variable set to `value`
        /// <remarks>
        set
        {
            Assert.IsNotNull(value);
            sceneChangerModel = value;
        }
        /// <summary>
        /// Get the sceneChangerModel reference instance variable
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None
        /// Postconditions:
        /// SceneChangerController's sceneChangerModel instance variable is returned
        /// 
        get
        {
            return sceneChangerModel;
        }
    }

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
            return null;
        }
        loadDebounce = true;

        // load scene from model using key
        string scenePath = sceneChangerModel.ScenePaths[sceneKey];
        Assert.IsNotNull(scenePath);

        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(scenePath);

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
    /// - A sceneChangerController must not already exist. If this is the first one being instantiated, it must already have
    /// a `SceneChangerModel` assigned to it. The field cannot be null.
    /// Postconditions: 
    /// If a SceneChangerModel doesn't exist, a single instance of it is created
    /// </remarks>
    public void Init()
    {
        Assert.IsNotNull(sceneChangerModel, "sceneChangerModel must not be null");
        Assert.IsNull(instance, "static var instance should be null, only one sceneChangerController may exist at a time");
        
        instance = this;
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