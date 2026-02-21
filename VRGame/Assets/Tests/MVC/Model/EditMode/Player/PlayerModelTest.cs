using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerModel_EditTest
{
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
        Assert.AreEqual("TestPlayer", playerModel.getPlayerName());
    }
}
