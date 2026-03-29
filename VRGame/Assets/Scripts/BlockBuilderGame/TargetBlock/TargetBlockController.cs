using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller component of TargetBlock.
/// </summary>
public class TargetBlockController : Controller<ITargetBlockModel, ITargetBlockView>, ITargetBlockController
{
    /// <summary>
    /// The block configuration that the player must match
    /// </summary>
    private Transform[] targetBlocks;

    /// <inheritdoc/>
    public void Awake()
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckModelRef();
        this.CheckViewRef();

        // Count child transforms
        int count = transform.childCount;
        targetBlocks = new Transform[count];
        for (int i = 0; i < count; i++)
            targetBlocks[i] = transform.GetChild(i);

        Assert.IsTrue(targetBlocks.Length > 0, "TargetBlock has no children to define the target configuration.");
    }
    
}
