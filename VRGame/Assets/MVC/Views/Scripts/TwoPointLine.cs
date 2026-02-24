using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;
public class TwoPointLine : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private LineRenderer line;


    // Start is called before the first frame update
    void Start()

    {
        Assert.IsNotNull(pointA,"Point A must be refrenced");
        Assert.IsNotNull(pointB,"Point B must be refrenced");
        if (pointA == null && pointB == null)
        {
            Debug.LogError("PointA and PointB must be assigned in the Unity Editor.");
            return;
        }
        line = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        line.positionCount = 2;
        line.SetPosition(0, pointA.position);
        line.SetPosition(1, pointB.position);

    }
}
