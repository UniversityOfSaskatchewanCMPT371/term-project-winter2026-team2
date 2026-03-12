using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Manages the instantiation of XR player rig, ensuring a persistent singleton instance exists across scene changes.
/// </summary>
public class PlayerServiceController : MonoBehaviour, IPlayerServiceController
{
    /// <summary>
    /// The static instance of the controller used to enforce the singleton pattern.
    /// </summary>
    private static PlayerServiceController singleton;

    /// <summary>
    /// Getter/Setter for 'singleton' field.
    /// </summary>
    private PlayerServiceController SingletonAccessor
    {
        /// <summary>
        /// Retrieves the current static value of 'singleton' field.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - None.
        /// Postconditions:
        /// - Returns the current static value of 'singleton' field.
        /// </remarks>
        get => singleton;

        /// <summary>
        /// Attempts to set the current static value of 'singleton' field if it has not been set.
        /// Otherwise, it destroys the calling object if a singleton already exists.
        /// </summary>
        /// <remarks>
        /// Preconditions:
        /// - 'value' is not null.
        /// Postconditions:
        /// - If 'singleton' is currently null, it is assigned the provided 'value'.
        /// - If 'singleton' is already assigned and different from 'this', 
        /// the duplicate   is destroyed to enforce the singleton pattern.
        /// </remarks>
        set
        {
            if (value == null)
            {
                Debug.LogError("'value' is null.");
                Assert.IsNotNull(value,"'value' cannot be null.");
            } else if (singleton != null && singleton != this)
            {
                Destroy(this);
                Debug.Log("A duplicate of PlayerServiceController singleton has been deleted.");
            } else
            {
                singleton = value;
            }
        }
    }
        
    /// <summary>
    /// The XR rig prefab that the player controls.
    /// </summary>
    [SerializeField]
    private GameObject XRrigPrefab;

    /// <summary>
    /// Reference to the singleton persistent xr rig the player controls.
    /// </summary>
    public static GameObject player;

    /// <inheritdoc/>
    public void SpawnPlayer(Vector3 position, Quaternion rotation)
    {
        if (XRrigPrefab == null)
        {
            Debug.LogError("XRrigPrefab field is null.");
            Assert.IsNotNull(XRrigPrefab, "XRrigPrefab field cannot be null.");
        }

        if (player != null)
        {
            // requires teleportPlayerTo() to be implemented in PlayerController.cs
            Debug.Log("Player rig already exists, teleporting rig instead.");
        } else
        {
            player = Instantiate(XRrigPrefab);

            // keep the player rig persistent across scene changes
            DontDestroyOnLoad(player);

            Debug.Log("Player rig instantiated.");
        }

        player.GetComponent<PlayerController>().teleportPlayerTo(position, rotation);
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
    void Start()
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
