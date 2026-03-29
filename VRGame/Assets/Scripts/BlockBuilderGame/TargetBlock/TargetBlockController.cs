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

    /// <summary>
    /// The tolerance (in degrees) for y-axis rotation
    /// </summary>
    private const float RotationTolerance = 5f;

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

    /// <inheritdoc/>
    public void CheckCompletion(SnapFitController[] builtBlocks)
    {
        Assert.IsNotNull(builtBlocks, "builtBlocks must not be null");

        // Check if the number of built blocks matches the number of target blocks
        if (builtBlocks.Length != targetBlocks.Length) 
        {
            Debug.Log("Target contains " + targetBlocks.Length + " blocks, but player only has " + builtBlocks.Length + ". Try again!");
            return;
        }

        for (int i = 0; i < targetBlocks.Length; i++)
        {
            // Get y-axis rotations for both built and target blocks
            float builtY  = builtBlocks[i].transform.eulerAngles.y;
            float targetY = targetBlocks[i].eulerAngles.y;

            // Check if the angles are within the specified tolerance
            if (Mathf.Abs(Mathf.DeltaAngle(builtY, targetY)) > RotationTolerance) 
            {
                Debug.Log("Block " + i + " is not aligned correctly.");
                return;
            }
        }
    }
    
}
