using UnityEngine;

/// <summary>
/// Model portion of the reusable player component.
/// Player data is stored here
public interface IPlayerModel
{
    /// <summary>
    /// Public accessor for the player's name
    /// </summary>
    string getPlayerName { 
        /// <summary>
        /// Getter method to retrieve the player's name
        /// </summary>
        /// <pre-condition> 
        ///     -   playerName has been initialized in the constructor
        /// </pre-condition>
        /// <post-condition> 
        ///     -   returns the name of the player
        /// </post-condition>
        get; 
        
        /// <summary>
        /// Setter method to set the player's name
        /// </summary>
        /// <pre-condition>
        ///     -   value must be a non-null string
        /// </pre-condition>
        /// <post-condition>
        ///     -   sets the player's name to the value (if it is valid)
        /// </post-condition>
        set; 
        }



    /// <summary>
    /// Public accessor for the player's ID
    /// </summary>
    int getPlayerId { 
        /// <summary>
        /// Getter method to retrieve the player's ID
        /// </summary>
        /// <pre-condition>
        ///     -   id has been initialized in the constructor
        /// </pre-condition>
        /// <post-condition>   
        ///    -   returns the unique identifier of the player
        /// </post-condition>
        get; 
        
        /// <summary>
        /// Setter method to set the player's ID
        /// </summary>
        /// <pre-condition>
        ///    -   value must be a positive integer
        /// </pre-condition>
        /// <post-condition>
        ///    -   sets the player's ID to the value (if it is valid)
        /// </post-condition>
        set; 
        }



    /// <summary>
    /// Public accessor for the player's alive status
    /// </summary>
    bool playerIsAlive { 
        /// <summary>
        /// Getter method to check if the player is alive        
        /// </summary>
        /// <pre-condition>
        ///     -   alive has been initialized in the constructor
        /// </pre-condition>
        /// <post-condition>
        ///     -   returns true if the player is alive, false otherwise
        /// </post-condition>
        get; 

        /// <summary>
        /// Method to set the player's alive status
        /// </summary> 
        /// <post-condition>
        ///     -   sets the alive status of the player to value
        /// </post-condition>
        set;
        }



    /// <summary>
    /// Method to initialize the player model with a name and ID, and set the player as alive
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="id">The unique identifier for the player</param>
    void Initialize(string name, int id);
}
