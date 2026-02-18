using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointLine : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private LineRenderer line;


    // Start is called before the first frame update
    void Start()

    {
        if (pointA == null || pointB == null)
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
