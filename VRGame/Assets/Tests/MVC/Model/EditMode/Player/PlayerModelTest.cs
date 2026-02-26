using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerModel_EditTest
{
    /// <summary>
    /// A simple initialization test to check if the PlayerModel can be initialized without errors
    /// </summary>
    [Test]
    public void PlayerModel_EditTestInitialization()
    {
        GameObject go = new GameObject();
        PlayerModel playerModel = go.AddComponent<PlayerModel>();
        playerModel.Initialize("TestPlayer", 1);
        Assert.IsNotNull(playerModel);
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// A simple test to check if the getPlayerName method of the PlayerModel class 
    ///      returns the correct name after initialization
    /// </summary>
    [Test]
    public void PlayerModel_EditTestGetPlayerName()
    {
        GameObject go = new GameObject();
        PlayerModel playerModel = go.AddComponent<PlayerModel>();
        playerModel.Initialize("TestPlayer", 1);
        // Use the Assert class to test if the getPlayerName method returns the expected name after initializing the PlayerModel
        Assert.AreEqual("TestPlayer", playerModel.getPlayerName);
    }

    /// <summary>
    /// A simple test to check if the getId method of the PlayerModel class
    ///      returns the correct ID after initialization
    /// </summary>
    [Test]
    public void PlayerModel_EditTestGetPlayerId()
    {
        GameObject go = new GameObject();
        PlayerModel playerModel = go.AddComponent<PlayerModel>();
        playerModel.Initialize("TestPlayer", 42);
        // Use the Assert class to test if the getId method returns the expected ID after initializing the PlayerModel
        Assert.AreEqual(42, playerModel.getPlayerId);
    }

    /// <summary>
    /// A simple test to check if the isAlive method of the PlayerModel class
    ///      returns true after initialization, indicating that the player is alive
    /// </summary>
    [Test]
    public void PlayerModel_EditTestPlayerIsAlive()
    {
        GameObject go = new GameObject();
        PlayerModel playerModel = go.AddComponent<PlayerModel>();
        playerModel.Initialize("TestPlayer", 1);
        Assert.IsTrue(playerModel.playerIsAlive);
    }
    
}
