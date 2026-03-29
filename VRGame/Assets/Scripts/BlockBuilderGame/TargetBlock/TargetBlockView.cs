using UnityEngine;

/// <summary>
/// View component of TargetBlock.
/// Handles feedback when the player completes the target.
/// </summary>
public class TargetBlockView : View<ITargetBlockController>, ITargetBlockView
{
    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckControllerRef();
    }

    /// <inheritdoc/>
    public void OnComplete()
    {
        Debug.Log("Target complete!");
    }
}
