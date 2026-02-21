using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// A simple PlayerModel class to represent the player in the MVC architecture
/// </summary>
public class PlayerModel : MonoBehaviour
{
    // Basic player attributes
    public String playerName;
    public int id;

    ///<summary>
    /// Initializes the player model with a name and an ID
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="id">The unique identifier for the player</param>
    public void Initialize(string name, int id)
    {
        this.playerName = name;
        this.id = id;
    }

    /// <summary>
    /// Getter method to retrieve the player's name
    /// </summary>
    /// pre-condition: 
    ///     -   playerName has been initialized in the constructor
    /// post-condition: 
    ///     -   returns the name of the player
    public String getPlayerName()
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
    public int getId()
    {
        return id;
    }
}
