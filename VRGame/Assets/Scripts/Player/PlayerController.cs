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
        // Yet to be implemented
    }
}
