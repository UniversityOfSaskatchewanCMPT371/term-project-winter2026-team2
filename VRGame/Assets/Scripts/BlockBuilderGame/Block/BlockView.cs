using UnityEngine;
using UnityEngine.Assertions;

public class BlockView : MonoBehaviour, IBlockView
{
    [SerializeField] private string currentBlockType;

    private void Awake()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        if (renderers.Length <= 0)
        {
            Assert.IsTrue(renderers.Length > 0, "No MeshRenderer found in " + gameObject.name);
            Debug.LogWarning("No MeshRenderer found in " + gameObject.name);
        } 
    }

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
        Vector3 oldPosition = transform.position;
        Quaternion oldRotation = transform.rotation;
        
        transform.position = position;
        transform.rotation = rotation;
        
        Debug.Log("Position updated from " + oldPosition + " to " + transform.position);
        Debug.Log("Rotation updated from " + oldRotation.eulerAngles + " to " + transform.rotation.eulerAngles);
    }

    public void SetBlockType(string blockType)
    {
        if (blockType == null)
        {
            Debug.LogError("Cannot set block type to null");
            Assert.IsNotNull(blockType, "BlockType cannot be null in SetBlockType");
            return;
        }
        currentBlockType = blockType;
    }
}
