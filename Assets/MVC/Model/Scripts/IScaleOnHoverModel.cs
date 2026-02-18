using UnityEngine;

public interface IScaleOnHoverModel
{
    void Initialize(Transform[] linkedObjects, float hoverScaleMultiplier, float scaleSpeed);
    void InitializeScales();
    void OnHoverEnter();
    void OnHoverExit();
    Vector3[] getTargetScale();
    Transform[] getLinkedObjects();
    void getScaleSpeed();
    void isHovering();
    void Awake();
}
