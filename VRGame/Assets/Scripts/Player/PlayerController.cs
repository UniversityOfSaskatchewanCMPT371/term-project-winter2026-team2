using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private PlayerModel model;
    [SerializeField] private PlayerView view;

    /// <inheritdoc/>
    public void Awake()
    {
        // Try to get components if not assigned
        if (model == null)
        {
            Debug.LogWarning("PlayerModel reference was not set in inspector, trying to get component...");
            model = GetComponent<PlayerModel>();
        }

        if (view == null)
        {
            Debug.LogWarning("PlayerView reference was not set in inspector, trying to get component...");
            view = GetComponent<PlayerView>();
        }

        // Validate references
        Assert.IsNotNull(model, "PlayerModel reference cannot be null");
        Assert.IsNotNull(view, "PlayerView reference cannot be null");

        if (model == null || view == null)
        {
            Debug.LogError("PlayerController initialization failed due to missing references. Check inspector assignments.");
            enabled = false; // Disable the controller to prevent further errors
            return;
        }

        // Initialize the player with default values
        model.Initialize("Player", 1);

        Debug.Log($"Player initialized: {model.getPlayerName} (ID: {model.getPlayerId})");
    }

    /// <inheritdoc/>
    public void teleportPlayerTo(Vector3 position, Quaternion rotation)
    {
        // Move rig first, then align camera-facing yaw with requested orientation.
        transform.position = position;

        // First see if camera rig has a Camera component so we can align the 
        // player's yaw to rotation
        Camera rigCamera = GetComponentInChildren<Camera>();
        if (rigCamera == null)
        {
            // No camera found, just set the rotation directly
            transform.rotation = rotation;
            Debug.LogWarning("No camera found in XR rig, setting rotation directly.");
            return;
        }

        // Calculate the forward direction, ignore vertical direction
        Vector3 desiredForward = rotation * Vector3.forward;
        desiredForward.y = 0f;

        // Calculate the current direction, ignore vertical direction
        Vector3 currentForward = rigCamera.transform.forward;
        currentForward.y = 0f;

        // Rotate the player around the Y axis to align with the desired forward direction
        float deltaYaw = Vector3.SignedAngle(currentForward.normalized, desiredForward.normalized, Vector3.up);
        transform.Rotate(Vector3.up, deltaYaw, Space.World);

        Debug.Log($"Player teleported to position: {position}, facing: {desiredForward.normalized}");
    }
}