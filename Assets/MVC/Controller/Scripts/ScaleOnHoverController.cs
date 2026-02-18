using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnHoverController : MonoBehaviour
{
    [SerializeField] private IScaleOnHoverModel model;
    [SerializeField] private IScaleOnHoverView view;

    public Transform[] retrieveLinkedObjects() 
    {
        return model.getLinkedObjects();
    }

    public Vector3[] retrieveTargetScale()
    {
        return model.getTargetScale();
    }
    
    public float retrieveScaleSpeed() 
    {
        return model.getScaleSpeed();
    }
}
