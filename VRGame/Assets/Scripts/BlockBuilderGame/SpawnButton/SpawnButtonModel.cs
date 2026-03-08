using UnityEngine;

/// <summary>
/// Model class for SpawnButton
/// Contains all data related to spawn button state
/// </summary>
public class SpawnButtonModel : ISpawnButtonModel
{
    /// <summary>
    /// The clicked state of the spawn button
    /// </summary>
    private bool isClicked;

    /// <inheritdoc/>
    public bool IsClicked()
    {
        get
        {
            return isClicked;
        }
        set
        {
            isClicked = value;
        }
    }
}
