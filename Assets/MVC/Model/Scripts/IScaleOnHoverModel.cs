using UnityEngine;

public interface IScaleOnHoverModel
{
    void Initialize(Transform[] linkedObjects, float hoverScaleMultiplier, float scaleSpeed);
    void OnHoverEnter();
    void OnHoverExit();
    Vector3[] GetCurrentTargetScales();
    Transform[] GetLinkedObjects();
}
