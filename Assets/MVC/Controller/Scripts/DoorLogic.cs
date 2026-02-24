using System;
using UnityEngine;
using UnityEngine.Assertions;

[ RequireComponent(typeof(DoorData)) ]

/// <summary>
/// Handles door interaction logic
/// </summary>
public class DoorLogic : MonoBehaviour
{
    public DoorData doorData;                   // Reference to the data attached to this object
    private ISceneChanger sceneChanger;          // changed from global reference to injected dependency to support testing
    private static bool triggerDebounce = false;     // Prevents rapid requests   

    /// <summary>
    /// Validates required fields and retrieves the SceneChanger service.
    /// </summary>
private void Awake()
{
    // Ensure doorData is assigned in the inspector
    Assert.IsNotNull(doorData, "DoorData field cannot be null.");
}

private void Start()
{
    // Auto-fetch SceneChanger from Services if not injected (supports unit testing)
    if (sceneChanger == null)
    {
        // Locate the project's Services GameObject and obtain the default
        // scene changer implementation. This is a runtime fallback used when
        // no scene changer has been injected.
        sceneChanger = (ISceneChanger)GameObject
            .Find("Services")
            .GetComponent<Services>()
            .sceneChanger;
    }
}

/// <summary>
/// Injects an ISceneChanger instance. Primarily used by tests
/// to supply a mock implementation that verifies load requests without changing scenes.
/// </summary>
/// <param name="changer">The scene changer implementation to use.</param>
public void InjectSceneChanger(ISceneChanger changer)
{
    // Allow tests to provide a custom ISceneChanger.
    // This prevents the component from using the global Services singleton
    // and enables deterministic unit tests that can assert calls instead
    // of performing actual scene transitions.
    sceneChanger = changer;
}

/// <summary>
/// Injects DoorData instance programmatically. Useful for tests
/// that construct GameObjects at runtime and need to assign DoorData.
/// </summary>
/// <param name="data">The DoorData instance to assign.</param>
public void InjectDoorData(DoorData data)
{
    // helper function for tests.
    // Using this avoids relying on inspector assignment when creating
    // objects at runtime inside test harnesses.
    doorData = data;
}

/// <summary>
/// Called when the player enters this door's collider.
/// Requests a scene load via ISceneChanger, and when the
/// scene has finished loading teleports the player's rig to the target door.
/// </summary>
/// <param name="playerRig">The player's XR rig GameObject; must include PlayerLogic.</param>
public void OnPlayerEnter(GameObject playerRig)
{
    // Validate the player rig contains the PlayerLogic component which
    // exposes the teleport API used below. Throw early to fail fast
    // when the API contract is not satisfied.
    PlayerLogic player = playerRig.GetComponent<PlayerLogic>();
    if (player == null)
        throw new MissingComponentException(
            "This function requires PlayerLogic component attached to PlayerRig."
        );

    // Debounce repeated trigger events to avoid initiating multiple concurrent scene loads.
    if (triggerDebounce) return;
    triggerDebounce = true;

    // Request the configured destination scene from the scene changer.
    Scenes scene = doorData.sceneDestination;
    AsyncOperation loadingScene = sceneChanger.LoadScene(scene);

    // If the scene changer refused the request (null), reset debounce and exit.
    if (loadingScene == null)
    {
        triggerDebounce = false;
        return;
    }

    // When the scene finishes loading, compute the teleport target from
    // the door data, then teleport the player's rig and clear the debounce.
    loadingScene.completed += _ =>
    {
        DoorData targetDoor = doorData.GetTargetDoor();

        Vector3 teleportPosition = targetDoor.GetTeleportPosition();
        Quaternion teleportRotation = targetDoor.GetTeleportRotation();

        // Use PlayerLogic to perform the actual teleport action.
        player.teleportPlayerTo(teleportPosition, teleportRotation);

        // Allow subsequent triggers after teleport completes.
        triggerDebounce = false;
    };
}
}