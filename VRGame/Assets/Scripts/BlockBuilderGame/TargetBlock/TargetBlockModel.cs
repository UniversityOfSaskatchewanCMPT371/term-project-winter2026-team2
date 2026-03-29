using UnityEngine.Assertions;

/// <summary>
/// Model component of TargetBlock.
/// Holds the target block.
/// </summary>
public class TargetBlockModel : Model, ITargetBlockModel
{
    /// <summary>
    /// Whether the player's build currently matches the target configuration.
    /// </summary>
    private bool isComplete = false;

    /// <inheritdoc/>
    public bool IsComplete
    {
        get
        {
            return isComplete;
        }
        set
        {
            isComplete = value;
        }
    }

    /// <inheritdoc/>
    public override void Init()
    {
        isComplete = false;
        Assert.IsFalse(isComplete, "TargetBlock isComplete state must be false on Init");
    }
}
