using UnityEngine;


/// <summary>
/// Interface for the SpawnButton Controller
/// Manages the logic for handling button press events to spawn bricks
/// </summary>
public interface ISpawnButtonController
{
    /// <summary>
    /// Initializes the spawn button with a reference to the BlockSpawnerController
    /// </summary>
    /// <param name="spawnerController">Reference to the BlockSpawnerController</param>
    /// <remarks>
    /// pre-condition:
    ///     - spawnerController is not null and is a valid BlockSpawnerController instance
    /// post-condition:
    ///     - The spawn button is initialized and ready to trigger brick spawning
    /// </remarks>
    void Initialize(BlockSpawnerController spawnerController);

    /// <summary>
    /// Handles the button press event and triggers brick spawning
    /// </summary>
    /// <remarks>
    /// pre-condition:
    ///     - The button has been initialized with a valid BlockSpawnerController
    /// post-condition:
    ///     - A brick spawning request is sent to the BlockSpawnerController
    /// </remarks>
    void HandleButtonPress();
}

