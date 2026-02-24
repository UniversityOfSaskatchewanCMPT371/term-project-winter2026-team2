using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using System.Reflection;

/// <summary>
/// Test class for DoorLogic component in edit mode.
/// This class contains unit tests to verify the behavior of the DoorLogic controller.
/// </summary>
public class DoorLogicTests
{
    /// <summary>
    /// GameObject representing the door in the test scene.
    /// </summary>
    private GameObject doorObject;

    /// <summary>
    /// Instance of DoorLogic component being tested.
    /// </summary>
    private DoorLogic doorLogic;
    private ISceneChanger mockSceneChanger;

    /// <summary>
    /// Data model for the door configuration.
    /// </summary>
    private DoorData doorData;
    private GameObject playerObject;

    [SetUp]
    public void Setup()
    {
        mockSceneChanger = Substitute.For<ISceneChanger>();

        doorObject = new GameObject();
        doorLogic = doorObject.AddComponent<DoorLogic>();
        doorData = doorObject.AddComponent<DoorData>();

    
        typeof(DoorLogic)
            .GetField("doorData", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(doorLogic, doorData);

        
        doorLogic.InjectSceneChanger(mockSceneChanger);

        
        doorData.sceneDestination = Scenes.Room1;

        
        playerObject = new GameObject();
        playerObject.AddComponent<PlayerLogic>();
    }
}
