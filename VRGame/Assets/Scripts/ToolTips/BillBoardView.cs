using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Makes a GameObject always face the camera.
/// </summary>
public class BillBoardView : MonoBehaviour
{
    /// <summary>
    /// Reference point used to calculate distance from camera.
    /// </summary>
    public Transform PointB;

    /// <summary>
    /// Cached reference to the main camera's transform.
    /// </summary>
    static Transform tCam = null;

    /// <summary>
    /// Updates the billboard to face the camera each frame.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - <c>PointB</c> must be assigned in the Unity Editor.
    /// - A camera tagged as 'MainCamera' must exist in the scene.
    /// Postconditions:
    /// - The GameObject faces the camera position.
    /// - <c>tCam</c> is cached for subsequent frames.
    /// - <c>PointB</c> local position is set based on camera distance on first frame.
    /// </remarks>
    void Update ()
    {
        // Cache camera transform and initialize PointB distance on first frame.
        if (!tCam)
        {
          Assert.IsNotNull(Camera.main, "Camera.main is null. Please ensure there is a camera in the scene tagged as 'MainCamera'.");
            tCam = Camera.main.transform;

            Assert.IsNotNull(PointB, "PointB must be assigned in the Unity Editor.");

            PointB.transform.localPosition = new Vector3(0, 0, Vector3.Distance(transform.position, tCam.position));
        }
        transform.LookAt(tCam.position, Vector3.up);
    }
}