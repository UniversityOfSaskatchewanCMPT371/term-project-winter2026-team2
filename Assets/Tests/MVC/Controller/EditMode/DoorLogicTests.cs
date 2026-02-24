using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using System.Reflection;

/// <summary>
/// Test class for DoorLogic component in edit mode.
/// This class contains unit tests to verify the behavior of the DoorLogic controller.
/// Uses NUnit framework with NSubstitute for mocking dependencies.
/// Each test method is isolated with [SetUp] and [TearDown] for consistent test execution.
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

    /// <summary>
    /// Mock implementation of ISceneChanger for testing scene loading behavior.
    /// </summary>
    private ISceneChanger mockSceneChanger;

    /// <summary>
    /// Data model for the door configuration.
    /// </summary>
    private DoorData doorData;

    /// <summary>
    /// GameObject representing the player rig in the test environment.
    /// </summary>
    private GameObject playerObject;

    /// <summary>
    /// Sets up the test environment before each test method execution.
    /// Creates mock objects, initializes the DoorLogic component with dependencies,
    /// and prepares test data for consistent test scenarios.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        // Create mock for scene changer to isolate scene loading behavior
        mockSceneChanger = Substitute.For<ISceneChanger>();

        // Create test GameObjects and components
        doorObject = new GameObject();
        doorLogic = doorObject.AddComponent<DoorLogic>();
        doorData = doorObject.AddComponent<DoorData>();

        // Inject doorData into DoorLogic using reflection (since it's private)
        typeof(DoorLogic)
            .GetField("doorData", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(doorLogic, doorData);

        // Inject mock scene changer for testing
        doorLogic.InjectSceneChanger(mockSceneChanger);

        // Configure door destination for test scenarios
        doorData.sceneDestination = Scenes.Room1;

        // Create player object with required PlayerLogic component
        playerObject = new GameObject();
        playerObject.AddComponent<PlayerLogic>();
    }

    /// <summary>
    /// Cleans up test objects after each test method execution.
    /// Destroys GameObjects to prevent memory leaks and ensure test isolation.
    /// </summary>
    [TearDown]
    public void Cleanup()
    {
        Object.DestroyImmediate(doorObject);
        Object.DestroyImmediate(playerObject);
    }

    /// <summary>
    /// Verifies that when the player enters the door, the DoorLogic
    /// requests the scene loader to load the configured destination scene.
    /// </summary>
    [Test]
    public void OnPlayerEnter_CallsLoadScene_WithCorrectScene()
    {
        // Act: simulate player entering the door collider
        doorLogic.OnPlayerEnter(playerObject);

        // Assert: the scene changer was asked to load the expected scene exactly once
        mockSceneChanger.Received(1)
            .LoadScene(Scenes.Room1);
    }
}

