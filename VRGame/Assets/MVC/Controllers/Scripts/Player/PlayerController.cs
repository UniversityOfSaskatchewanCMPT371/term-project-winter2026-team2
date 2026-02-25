using System.Diagnostics;
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
            Debug.LogErrorIf(model == null, "PlayerModel component not found on GameObject");
        }
        if (view == null)
        {
            Debug.LogWarning("PlayerView reference was not set in inspector, trying to get component...");
            view = GetComponent<PlayerView>();
            Debug.LogErrorIf(view == null, "PlayerView component not found on GameObject");
        }

        // Validate references
        Assert.IsNotNull(model, "PlayerModel reference cannot be null");
        Assert.IsNotNull(view, "PlayerView reference cannot be null");

        // Initialize the player with default values
        model.Initialize("Player", 1);
        
        Debug.Log($"Player initialized: {model.getPlayerName} (ID: {model.getPlayerId})");
    }
}
