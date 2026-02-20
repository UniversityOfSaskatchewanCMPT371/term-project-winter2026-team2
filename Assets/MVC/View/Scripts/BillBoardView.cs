using UnityEngine;
using UnityEngine.Assertions;

public class BillBoardView : MonoBehaviour
{
    public Transform PointB;

    static Transform tCam = null;
    void Update ()
    {
        // <summary>
        // Make the billboard face the camera by looking at the camera's position.
        // If the camera transform is not cached, find the main camera and cache its transform for future use.
        // Ensure that there is a main camera in the scene and provide an assertion message if it's missing.
        // Set the local position of PointB to be directly in front of the camera at a distance equal to the distance from the billboard to the camera.
        // </summary>
        if (!tCam)
        {
          Assert.IsNotNull(Camera.main, "Camera.main is null. Please ensure there is a camera in the scene tagged as 'MainCamera'.");
            tCam = Camera.main.transform;
            PointB.transform.localPosition = new Vector3(0, 0, Vector3.Distance(transform.position, tCam.position));
        }
        transform.LookAt(tCam.position, Vector3.up);
    }
}