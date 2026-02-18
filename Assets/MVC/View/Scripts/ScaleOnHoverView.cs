using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScaleOnHoverView : IScaleOnHoverView
{
    [SerializeField] IScaleOnHoverModel model;

    public void Init(IScaleOnHoverModel model)
    {
        this.model = model;
    }

    public void Update() 
    {
        Transform[] linkedObjects = model.getLinkedObjects();
        Vector3[] targetScales = model.getTargetScale();
        float scaleSpeed = model.getScaleSpeed();

        for (int i = 0; i < linkedObjects.Length; i++) {
            linkedObjects[i].localScale = Vector3.Lerp(
                linkedObjects[i].localScale,
                targetScales[i],
                deltaTime * speed
            );
        }
    }
}
