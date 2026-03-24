using UnityEngine;
using UnityEngine.Assertions;

public class BlockView : MonoBehaviour, IBlockView
{
    /// <summary>
    /// Current type of the block (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
    /// </summary>
    [SerializeField] private string currentBlockType;

    /// <summary>
    /// Awake method to perform initial checks and setup
    /// </summary>
    private void Awake()
    {
        // Check for MeshRenderer component in children
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        if (renderers.Length <= 0)
        {
            Assert.IsTrue(renderers.Length > 0, "No MeshRenderer found in " + gameObject.name);
            Debug.LogWarning("No MeshRenderer found in " + gameObject.name);
        } 
    }

    /// <inheritdoc/>
    public void UpdateVisuals(Vector3 position, Quaternion rotation)
    {
        if (position == null)
        {
            Debug.LogError("Cannot update visuals with null position");
            Assert.IsNotNull("Position cannot be null in UpdateVisuals");
            return;
        }
        if (rotation == null)        
        {
            Debug.LogError("Cannot update visuals with null rotation");
            Assert.IsNotNull("Rotation cannot be null in UpdateVisuals");
            return;
        }   
        // Store old position and rotation for debugging
        Vector3 oldPosition = transform.position;
        Quaternion oldRotation = transform.rotation;
        
        // Update position and rotation
        transform.position = position;
        transform.rotation = rotation;
        
        Debug.Log("Position updated from " + oldPosition + " to " + transform.position);
        Debug.Log("Rotation updated from " + oldRotation.eulerAngles + " to " + transform.rotation.eulerAngles);
    }

    /// <inheritdoc/>
    public void SetBlockType(string blockType)
    {
        if (blockType == null)
        {
            Debug.LogError("Cannot set block type to null");
            Assert.IsNotNull(blockType, "BlockType cannot be null in SetBlockType");
            return;
        }
        // Set the block type
        currentBlockType = blockType;
    }
}
