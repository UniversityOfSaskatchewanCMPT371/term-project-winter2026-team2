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
    private const float RotationTolerance = 10f;

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

        // Use bubble sort to sort built blocks by y position (bottom to top) for comparison against targetBlocks order
        SnapFitController[] builtSorted = new SnapFitController[builtBlocks.Length];
        builtBlocks.CopyTo(builtSorted, 0);
        for (int i = 0; i < builtSorted.Length - 1; i++)
            for (int j = i + 1; j < builtSorted.Length; j++)
                if (builtSorted[j].transform.position.y < builtSorted[i].transform.position.y)
                    (builtSorted[i], builtSorted[j]) = (builtSorted[j], builtSorted[i]);

        // Check:
        //      name match (bevel-hq-brick-1x1, bevel-hq-brick-1x2, etc.)
        //      rotation match (within RotationTolerance)
        //      material match
        for (int i = 0; i < targetBlocks.Length; i++)
        {
            string builtName  = builtSorted[i].gameObject.name.Replace("(Clone)", "").Trim();
            string targetName = targetBlocks[i].gameObject.name;
            bool nameMatch = builtName == targetName;
            bool rotMatch  = Mathf.Abs(Mathf.DeltaAngle(builtSorted[i].transform.eulerAngles.y, targetBlocks[i].eulerAngles.y)) <= RotationTolerance;

            var builtRenderer  = builtSorted[i].GetComponentInChildren<Renderer>();
            var targetRenderer = targetBlocks[i].GetComponentInChildren<Renderer>();
            bool matMatch = builtRenderer != null && targetRenderer != null &&
                            builtRenderer.sharedMaterial == targetRenderer.sharedMaterial;

            Debug.Log(i + "Name: " + builtName + ". Rotation: " + builtSorted[i].transform.eulerAngles.y + ". Matches target? " + nameMatch + " Rotation matches target? " + rotMatch + " Material matches target? " + matMatch);

            if (!nameMatch || !rotMatch || !matMatch)
            {
                Debug.Log($"Block " + i + " does not match target. Try again!");
                return;
            }
        }
        modelInstance.IsComplete = true;
        viewInstance.OnComplete();
    }
    
}
