using UnityEngine;

public interface IBlockController
{
    void Initialize(string blockType);
    void UpdatePosition(Vector3 position);
    void UpdateRotation(Quaternion rotation);
}
