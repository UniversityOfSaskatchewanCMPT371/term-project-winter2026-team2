using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// A simple PlayerModel class to represent the player in the MVC architecture
/// </summary>
public class PlayerModel : MonoBehaviour, IPlayerModel
{

    /// <summary>
    /// The name of the player
    /// </summary>
    [SerializeField]
    private string playerName;

    /// <inheritdoc/>
    public string getPlayerName
    {
        /// <inheritdoc/>
        get
        {
            return playerName;
        }

        /// <inheritdoc/>
        set
        {
            if (value == null)
            {
                Debug.LogError("Player name cannot be null");
                return;
            }
            Assert.IsNotNull(value, "Player name cannot be null");
            playerName = value;
        }
    }



    /// <summary>
    ///  Integer id for the player
    /// </summary>
    [SerializeField] private int id;

    /// <inheritdoc/>
    public int getPlayerId
    {
        /// <inheritdoc/>
        get
        {
            return id;
        }

        /// <inheritdoc/>
        set
        {
            if (value <= 0)
            {
                Debug.LogError("Player ID must be a positive integer");
                return;
            }
            Assert.IsTrue(value > 0, "Player ID must be a positive integer");
            id = value;
        }
    }



    /// <summary>
    /// Boolean to track if the player is alive or not
    /// </summary>
    [SerializeField] bool alive = false;

    /// <inheritdoc/>
    public bool playerIsAlive
    {
        /// <inheritdoc/>
        get
        {
            return alive;
        }

        /// <inheritdoc/>
        set
        {
            alive = value;
        }
    }

    /// <inheritdoc/>
    public void Initialize(string name, int id)
    {
        // Pre-condition checks
        if (name == null)
        {
            Debug.LogError("Player name cannot be null");
            return;
        }
        if (id <= 0)
        {
            Debug.LogError("Player ID must be a positive integer");
            return;
        }

        Assert.IsNotNull(name, "Player name cannot be null");
        Assert.IsTrue(id > 0, "Player ID must be a positive integer");

        // Set values
        this.playerName = name;
        this.id = id;
        this.alive = true;

        // Post-condition check
        Assert.IsTrue(this.alive, "Player should be alive after initialization");
    }


}
