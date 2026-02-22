using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(DoorData))]
/// <summary>
/// Handles all logic related to door interaction:
/// - Validates required components
/// - Retrieves or receives a scene‑changing service
/// - Responds to player entry events
/// - Loads the destination scene
/// - Teleports the player to the corresponding target door
///
/// This class is intentionally lightweight and delegates:
/// - Scene loading to <see cref="ISceneChanger"/>
/// - Door metadata to <see cref="DoorData"/>
/// - Player movement to <see cref="PlayerLogic"/>
///
/// The class supports dependency injection for testing.
/// </summary>
public class DoorLogic : MonoBehaviour
{
    /// <summary>
    /// Data describing this door's destination scene and target door.
    /// Must be assigned in the inspector.
    /// </summary>
    [SerializeField] private DoorData doorData;

    /// <summary>
    /// Abstract scene‑changing service.
    /// Retrieved automatically or injected for testing.
    /// </summary>
    private ISceneChanger sceneChanger;

    /// <summary>
    /// Prevents multiple triggers while a scene load is in progress.
    /// This is per‑door, not global.
    /// </summary>
    private bool triggerDebounce = false;

    /// <summary>
    /// Validates required fields and retrieves the scene‑changing service
    /// if one has not been injected.
    /// </summary>
    private void Awake()
    {
        Assert.IsNotNull(doorData, "DoorData field cannot be null.");

        // Auto-fetch only if not injected (supports unit testing)
        if (sceneChanger == null)
        {
            sceneChanger = GameObject
                .Find("Services")
                .GetComponent<Services>()
                .sceneChanger;
        }
    }

    /// <summary>
    /// Injects a custom <see cref="ISceneChanger"/> implementation.
    /// Used primarily for unit testing to avoid scene loads.
    /// </summary>
    /// <param name="changer">The scene changer implementation to use.</param>
    public void InjectSceneChanger(ISceneChanger changer)
    {
        sceneChanger = changer;
    }

    /// <summary>
    /// Called when the player enters this door's trigger volume.
    /// Loads the destination scene and teleports the player to the
    /// corresponding target door once loading completes.
    /// </summary>
    /// <param name="playerRig">The player's XR rig GameObject.</param>
    /// <exception cref="MissingComponentException">
    /// Thrown if the player object does not contain a <see cref="PlayerLogic"/> component.
    /// </exception>
    public void OnPlayerEnter(GameObject playerRig)
    {
        // Validate player object
        PlayerLogic player = playerRig.GetComponent<PlayerLogic>();
        if (player == null)
            throw new MissingComponentException(
                "This function requires PlayerLogic component attached to PlayerRig."
            );

        // Prevent re-entry during load
        if (triggerDebounce) return;
        triggerDebounce = true;

        // Request scene load
        Scenes sceneIdx = doorData.sceneDestination;
        AsyncOperation loadingScene = sceneChanger.LoadScene(sceneIdx);

        // SceneChanger may reject the request (debounce)
        if (loadingScene == null)
        {
            triggerDebounce = false;
            return;
        }

        // Teleport player once the scene finishes loading
        loadingScene.completed += _ =>
        {
            DoorData targetDoor = doorData.GetTargetDoor();

            Vector3 teleportPosition = targetDoor.GetTeleportPosition();
            Quaternion teleportRotation = targetDoor.GetTeleportRotation();

            player.teleportPlayerTo(teleportPosition, teleportRotation);

            triggerDebounce = false;
        };
    }
}