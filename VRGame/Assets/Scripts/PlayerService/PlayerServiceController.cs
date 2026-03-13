using NUnit.Framework;
using UnityEngine;

/// <summary>
/// A singleton manager that handles player spawn and ensures that only one exists at a time.
/// </summary>
public class PlayerServiceController : Controller<IModel, IView>, IPlayerServiceController
{
    /// <summary>
    /// Reference to the singleton instance of this class. Used to
    /// follow singleton pattern.
    /// </summary>
    private static PlayerServiceController instance;
        
    /// <summary>
    /// XR rig prefab reference. Requires PlayerController component.
    /// </summary>
    [SerializeField]
    private GameObject XRrigPrefab;

    /// <summary>
    /// Optional reference to a game object in which its transform is used by default
    /// to spawn the player. Uses default world origin if not set.
    /// </summary>
    [SerializeField]
    public GameObject defaultSpawnOrigin;

    /// <summary>
    /// Boolean for spawning the player on scene load. Only if 'playerObj' is null.
    /// </summary>
    [SerializeField]
    public bool spawnPlayerOnLoad = true;

    /// <summary>
    /// Reference to the persistent XRrig instantiated. 
    /// Value of this variable depends on the 'XRrigPrefab' variable, and is only
    /// ever set via SpawnPlayer() method
    /// </summary>
    private static GameObject playerObj;

    /// <summary>
    /// Verifies 'XRrigPrefab' variable.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'XRrigPrefab' variable cannot be null.
    /// - 'XRrigPrefab' must contain PlayerController component.
    /// Postconditions:
    /// - Verifies 'XRrigPrefab' variable and ensures that it contains the PlayerController component.
    /// - Logs errors if any of the preconditions are violated.
    /// </remarks>
    private void checkXRrigPrefab()
    {
        // see if 'XRrigPrefab' field was set in inspector
        if (XRrigPrefab == null)
        {
            Debug.LogError("'XRrigPrefab' variable was not set in inspector.");
        }
        Assert.IsNotNull(XRrigPrefab, "'XRrigPrefab' cannot be null."); 
        
        // see if 'XRrigPrefab' contains PlayerController component
        if (XRrigPrefab.TryGetComponent<PlayerController>(out PlayerController component))
        {
            // it does have it
        } else
        {
            Debug.LogError("'XRrigPrefab' does not have PlayerController component attached.");
        }
        Assert.IsNotNull(component, "'XRrigPrefab' must contain PlayerController component.");
    }

    /// <inheritdoc/>
    public void SpawnPlayer(Vector3 position, Quaternion rotation)
    {
        // verifies 'XRrigPrefab' variable
        checkXRrigPrefab();

        // see if an XR rig already exists in the scene
        if (playerObj == null)
        {

            // instantiate the XR rig
            playerObj = Instantiate(XRrigPrefab);

            // keep this XR rig persistent
            DontDestroyOnLoad(playerObj);
        }

        // teleport the player to the specified position after it spawns
        // TODO: Uncomment this once teleportPlayerTo() is implemented
        // playerObj.GetComponent<PlayerController>().teleportPlayerTo(position, rotation);
        
        Debug.Log("Player spawned successfully.");
    }

    /// <inheritdoc/>
    public override void Init()
    {
        // validate 'XRrigPrefab' variable
        checkXRrigPrefab();

        // see if an instance of this component already exists
        if (instance != null && instance != this)
        {
            // destroy this duplicate
            Destroy(gameObject);

            Debug.LogWarning("There can be only one active PlayerServiceController.");
            return;
        } else
        {
            // declare this component as the instance
            instance = this;
        }

        // optionally spawn player on scene load only if its enabled
        if (playerObj == null && spawnPlayerOnLoad)
        {
            // default position and rotation
            Vector3 position = new Vector3(0, 1, 0);
            Quaternion rotation = new Quaternion();

            // see if a default spawn point was given
            if (defaultSpawnOrigin != null)
            {
                // use it instead
                position = defaultSpawnOrigin.transform.position;
                rotation = defaultSpawnOrigin.transform.rotation;
            }

            // spawn the player
            SpawnPlayer(position, rotation);
        }

        Debug.Log("PlayerServiceController initialized successfully.");
    }

    /// <inheritdoc/>
    public void Awake() 
    {
        Init();
    }

    /// <inheritdoc cref="IPlayerServiceController"/>
    public override void Start() {}
}
