using UnityEngine;
using UnityEngine.Assertions;

public class BlockView : MonoBehaviour, IBlockView
{
    /// <summary>
    /// Current shape of the block (e.g., bevel_lq_brick_1x1, bevel_lq_brick_1x2, etc.)
    /// </summary>
    [SerializeField] private BlockShape currentBlockShape;

    /// <summary>
    /// Current colour of the block (e.g., red, blue, green etc.)
    /// </summary>
    [SerializeField] private BlockColour currentBlockColour;

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
    public void SetBlockShape(BlockShape shape)
    {
        // Set the block shape
        currentBlockShape = shape;
    }

    /// <inheritdoc/>
    public void SetBlockColour(BlockColour colour)
    {
        // Set the block colour
        currentBlockColour = colour;
    }
}
