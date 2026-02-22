using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    [SerializeField] private PlayerView view;

    /// <summary>
    /// Initialize the player controller and validate model/view references
    /// </summary>
    private void Awake()
    {
        // Try to get components if not assigned
        if (model == null)
        {
            model = GetComponent<PlayerModel>();
        }
        if (view == null)
        {
            view = GetComponent<PlayerView>();
        }

        // Validate references
        Assert.IsNotNull(model, "PlayerModel reference cannot be null");
        Assert.IsNotNull(view, "PlayerView reference cannot be null");

        // Initialize the player with default values
        // You can customize this based on your needs
        model.Initialize("Player", 1);
        
        Debug.Log($"Player initialized: {model.getPlayerName} (ID: {model.getPlayerId})");
    }
}
