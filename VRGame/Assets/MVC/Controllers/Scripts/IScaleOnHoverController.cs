using UnityEngine;

public interface IScaleOnHoverController
{
    Transform[] retrieveLinkedObjects();
    Vector3[] retrieveTargetScale();
    float retrieveScaleSpeed();
    bool IsHovering();
    void OnHoverEnter();
    void OnHoverExit();
}
