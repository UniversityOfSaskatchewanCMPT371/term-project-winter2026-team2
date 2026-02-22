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

    /// <summary>
    /// Public accessor for the player's name
    /// </summary>
    public string getPlayerName
    {
        /// <summary>
        /// Getter method to retrieve the player's name
        /// </summary>
        /// <pre-condition> 
        ///     -   playerName has been initialized in the constructor
        /// </pre-condition>
        /// <post-condition> 
        ///     -   returns the name of the player
        /// </post-condition>
        get {
            return playerName;
        }

        /// <summary>
        /// Setter method to set the player's name
        /// </summary>
        /// <pre-condition>
        ///     -   value must be a non-null string
        /// </pre-condition>
        /// <post-condition>
        ///     -   sets the player's name to the value (if it is valid)
        /// </post-condition>
        set {
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

    /// <summary>
    /// Public accessor for the player's ID
    /// </summary>
    public int getPlayerId
    {
        /// <summary>
        /// Getter method to retrieve the player's ID
        /// </summary>
        /// <pre-condition>
        ///     -   id has been initialized in the constructor
        /// </pre-condition>
        /// <post-condition>   
        ///    -   returns the unique identifier of the player
        /// </post-condition>
        get {
            return id;
        }

        /// <summary>
        /// Setter method to set the player's ID
        /// </summary>
        /// <pre-condition>
        ///    -   value must be a positive integer
        /// </pre-condition>
        /// <post-condition>
        ///    -   sets the player's ID to the value (if it is valid)
        /// </post-condition>
        set {
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
    
    /// <summary>
    /// Public accessor for the player's alive status
    /// </summary>
    public bool playerIsAlive
    {
        /// <summary>
        /// Getter method to check if the player is alive        
        /// </summary>
        /// <pre-condition>
        ///     -   alive has been initialized in the constructor
        /// </pre-condition>
        /// <post-condition>
        ///     -   returns true if the player is alive, false otherwise
        /// </post-condition>
        get {
            return alive;
        }

        /// <summary>
        /// Method to set the player's alive status
        /// </summary> 
        /// <post-condition>
        ///     -   sets the alive status of the player to value
        /// </post-condition>
        set {
            alive = value;
        }
    }

    ///<summary>
    /// Initializes the player model with a name and an ID
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="id">The unique identifier for the player</param>
    /// <pre-condition>
    ///     -   name must be a non-null string
    ///     -   id must be a positive integer
    /// </pre-condition>
    /// <post-condition>
    ///     -   PlayerModel is initialized to alive
    /// </post-condition>
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
