using UnityEngine;

public interface IBlockView
{
    void UpdateVisuals(Vector3 position, Quaternion rotation);
    void SetBlockType(string blockType);
}
