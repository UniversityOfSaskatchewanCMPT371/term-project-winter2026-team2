
using System;
using UnityEngine;
using UnityEngine.Assertions;


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
    /// Wrapper for unity's sceneManager. Created to make unit testing easier. Will
    /// be used to asynchronously load new scenes
    /// </summary>
    private ISceneManagerWrapper sceneManagerWrapper;

    /// <summary>
    /// Public accessor for sceneManagerWrapper
    /// </summary>
    internal ISceneManagerWrapper SceneManagerWrapper
    {
        /// <summary>
        /// Set SceneChangerController's sceneManagerWrapper
        /// </summary>
        /// <remarks>
        /// Preconditions
        /// - value must not be null
        /// Postconditions:
        /// - SceneChangerController's sceneManagerWrapper set to value
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.LogError("SceneManagerWrapper is null");
            }
            Assert.IsNotNull(value, "sceneManagerWrapper cannot be null");

            sceneManagerWrapper = value;
        }
    }

    /// <summary>
    /// Prevents multiple scene loads from being triggered at once
    /// </summary>
    private static bool loadDebounce;

    /// <inheritdoc/>
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
        get
        {
            return loadDebounce;
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

    /// <inheritdoc/>
    public IAsyncOperationWrapper LoadScene(int sceneKey)
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

        // get Load new scene with sceneManager through wrapper
        IAsyncOperationWrapper loadingSceneWrapper = sceneManagerWrapper.LoadSceneAsync(sceneKey);

        // reset debounce when the scene finishes loading
        loadingSceneWrapper.Completed += (o) => resetDebounce();

        return loadingSceneWrapper;
    }


    /// <inheritdoc/>
    public void Init()
    {

        if (instance != null)
        {
            Debug.LogError("SceneChangerController instance already exists");
        }
        Assert.IsNull(instance, "static var instance should be null, only one sceneChangerController may exist at a time");

        if (sceneManagerWrapper == null)
        {
            Debug.LogError("SceneManagerWrapper is null");
        }
        Assert.IsNotNull(sceneManagerWrapper, "SceneManagerWrapper must not be null");

        loadDebounce = false;

        instance = this;
        Debug.Log("SceneChangerController initialized");
    }

    /// <inheritdoc/>
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
