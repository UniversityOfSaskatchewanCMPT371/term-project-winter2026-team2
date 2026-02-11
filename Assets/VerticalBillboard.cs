using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBillboard : MonoBehaviour
{
    [ExecuteInEditMode]
    public Camera MainCamera;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        target = Camera.main.transform;
        if (MainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please assign a camera to the VerticalBillboard script.");
        }


    }

    // Update is called once per frame
    void Update()
    {
        MainCamera = Camera.main;
        if (MainCamera != null)
        {
            transform.LookAt(target , Vector3.up);
        }
    }
}
