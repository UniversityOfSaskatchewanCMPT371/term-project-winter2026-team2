using NUnit.Framework;
using NSubstitute;
using UnityEngine;

/// <summary>
/// Unit tests for DoorLogic.
/// Uses NSubstitute to mock scene loading service.
/// 
/// Tests:
/// - Scene load invocation
/// - Debounce behavior
/// - Exception handling
/// </summary>
public class DoorLogicTests
{
    private GameObject doorObject;
    private DoorLogic doorLogic;
    private DoorData doorData;
    private ISceneChanger mockSceneChanger;

    private GameObject playerObject;

    [SetUp]
    public void Setup()
    {
        // Create mock scene changer
        mockSceneChanger = Substitute.For<ISceneChanger>();

        // Create Door GameObject
        doorObject = new GameObject();
        doorLogic = doorObject.AddComponent<DoorLogic>();
        doorData = doorObject.AddComponent<DoorData>();

        doorLogic.doorData = doorData;
        doorLogic.InjectSceneChanger(mockSceneChanger);

        // Set test scene destination
        doorData.sceneDestination = Scenes.Room1;

        // Create Player GameObject
        playerObject = new GameObject();
        playerObject.AddComponent<PlayerLogic>();
    }

    [TearDown]
    public void Cleanup()
    {
        Object.DestroyImmediate(doorObject);
        Object.DestroyImmediate(playerObject);
    }

    /// <summary>
    /// Verifies that LoadScene is called with correct destination
    /// when player enters the door.
    /// </summary>
    [Test]
    public void OnPlayerEnter_CallsLoadScene_WithCorrectScene()
    {
        // Act
        doorLogic.OnPlayerEnter(playerObject);

        // Assert
        mockSceneChanger.Received(1)
            .LoadScene(Scenes.Room1);
    }

    /// <summary>
    /// Verifies that debounce prevents multiple rapid scene loads.
    /// </summary>
    [Test]
    public void OnPlayerEnter_Debounce_PreventsDoubleLoad()
    {
        // Act
        doorLogic.OnPlayerEnter(playerObject);
        doorLogic.OnPlayerEnter(playerObject);

        // Assert
        mockSceneChanger.Received(1)
            .LoadScene(Scenes.Room1);
    }

    /// <summary>
    /// Ensures MissingComponentException is thrown
    /// if PlayerLogic is not attached.
    /// </summary>
    [Test]
    public void OnPlayerEnter_WithoutPlayerLogic_ThrowsException()
    {
        var invalidPlayer = new GameObject(); // No PlayerLogic

        Assert.Throws<MissingComponentException>(() =>
        {
            doorLogic.OnPlayerEnter(invalidPlayer);
        });

        Object.DestroyImmediate(invalidPlayer);
    }
}