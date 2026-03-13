using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Manages the instantiation of XR player rig, ensuring a persistent singleton instance exists across scene changes.
/// </summary>
public class PlayerServiceController : Controller<IModel, IView>, IPlayerServiceController
{
    /// <summary>
    /// Reference to the singleton instance of this class. Used to
    /// follow singleton pattern.
    /// </summary>
    internal static PlayerServiceController instance;
        
    /// <summary>
    /// XR rig prefab reference.
    /// </summary>
    [SerializeField]
    private GameObject XRrigPrefab;

    /// <summary>
    /// Reference to the persistent XRrig instantiated.
    /// </summary>
    private static GameObject playerObj;

    /// <inheritdoc/>
    public void SpawnPlayer(Vector3 position, Quaternion rotation)
    {
        // see if 'XRrigPrefab' field was set in inspector
        if (XRrigPrefab == null)
        {
            Debug.LogError("'XRrigPrefab' variable was not set in inspector.");
        }
        Assert.IsNotNull(XRrigPrefab, "'XRrigPrefab' cannot be null.");

        // see if an XR rig already exists in the scene
        if (playerObj == null)
        {
            // instantiate the XR rig
            playerObj = Instantiate(XRrigPrefab);

            // see if the XR rig has a PlayerController component. This component interacts with it.
            if (playerObj.GetComponent<PlayerController>() == null) {
                Debug.LogError("XR rig prefab does not have PlayerController component attached.");
            }
            Assert.IsTrue(playerObj.GetComponent<PlayerController>(), "XR rig prefab needs PlayerController component.");

            // keep this XR rig persistent
            DontDestroyOnLoad(playerObj);
        }

        // teleport the player to the specified position after it spawns
        playerObj.GetComponent<PlayerController>().teleportPlayerTo(position, rotation);
        
        Debug.Log("Player spawned successfully.");
    }

    /// <inheritdoc/>
    public void Init()
    {
        if (XRrigPrefab == null)
        {
            Debug.LogError("XRrigPrefab field is null.");
            Assert.IsNotNull(XRrigPrefab, "XRrigPrefab field cannot be null.");
        }

        Debug.Log("PlayerServiceController initialized successfully.");
    }

    /// <summary>
    /// Called after all Awake() calls finishes. Built-in by Unity.
    /// Invokes Init() method.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Init() method is implemented.
    /// Postconditions:
    /// - Invokes Init() method.
    /// </remarks>
    void Awake()
    {
        Init();
    }

    /// <summary>
    /// Called after the scene loads calls finishes. Built-in by Unity.
    /// Makes sure that this component follows the singleton pattern.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - 'SingletonAccessor' setter/getter is implemented.
    /// Postconditions:
    /// - If the 'staticSingleton' is already set, then this component gets detroyed, otherwise
    /// set it's value to this component.
    /// </remarks>
    void Awake()
    {
        SingletonAccessor = this;
    }
}
