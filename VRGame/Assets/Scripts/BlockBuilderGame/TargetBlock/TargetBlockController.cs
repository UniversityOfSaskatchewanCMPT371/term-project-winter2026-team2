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

    /// <summary>
    /// The tolerance (in world units) for position matching
    /// </summary>
    private const float PositionTolerance = 0.1f;

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

        // Check built blocks:
        //      name match (bevel-hq-brick-1x1, bevel-hq-brick-1x2, etc.)
        //      position match (within PositionTolerance)
        //      rotation match (within RotationTolerance) 
        foreach (var target in targetBlocks)
        {
            bool matched = false;
            foreach (var block in builtBlocks)
            {
                bool nameMatch = block.gameObject.name == target.gameObject.name;
                Assert.IsTrue(nameMatch, "Built block name does not match target block target block's name");
                bool posMatch  = Vector3.Distance(block.transform.position, target.position) <= PositionTolerance;
                Assert.IsTrue(posMatch, "Built block is not within position tolerance of target block");
                bool rotMatch  = Mathf.Abs(Mathf.DeltaAngle(block.transform.eulerAngles.y, target.eulerAngles.y)) <= RotationTolerance;
                Assert.IsTrue(rotMatch, "Built block is not within rotation tolerance of target block");

                if (nameMatch && posMatch && rotMatch)
                {
                    matched = true;
                    break;
                }
            }
            if (!matched)
            {
                Debug.LogError("Player's built block does not match target block");
                return;
            }
        }
        modelInstance.IsComplete = true;
        viewInstance.OnComplete();
    }
    
}
