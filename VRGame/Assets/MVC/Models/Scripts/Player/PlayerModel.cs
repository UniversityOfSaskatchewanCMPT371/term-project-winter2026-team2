using UnityEngine;

/// <summary>
/// A simple PlayerModel class to represent the player in the MVC architecture
/// </summary>
public class PlayerModel : MonoBehaviour, IPlayerModel
{
    // Basic player attributes
    [SerializeField] private string playerName;
    [SerializeField] private int id;
    [SerializeField] bool alive = false;

    ///<summary>
    /// Initializes the player model with a name and an ID
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="id">The unique identifier for the player</param>
    /// <param name="isAlive">Sets player's alive status to be true once initialized</param>
    public void Initialize(string name, int id)
    {
        this.playerName = name;
        this.id = id;
        this.alive = true;
    }

    /// <summary>
    /// Getter method to retrieve the player's name
    /// </summary>
    /// pre-condition: 
    ///     -   playerName has been initialized in the constructor
    /// post-condition: 
    ///     -   returns the name of the player
    public string getPlayerName()
    {
        return playerName;
    }

    /// <summary>
    /// Getter method to retrieve the player's ID
    /// </summary>
    /// pre-condition:
    ///     -   id has been initialized in the constructor
    /// post-condition:
    ///    -   returns the unique identifier of the player
    public int getPlayerId()
    {
        return id;
    }

    public bool playerIsAlive()
    {
        return alive;
    }

}
