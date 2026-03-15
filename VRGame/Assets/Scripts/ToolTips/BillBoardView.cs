using UnityEngine;

/// <summary>
/// Keeps a GameObject positioned relative to camera's view
/// set offset to place it on top left of screen
/// </summary>
public class BillBoardView : MonoBehaviour
{
    /// <summary>
    /// the main camera
    /// </summary>
    public Transform target; 

    /// <summary>
    /// Offset from the target's position
    /// </summary>
    /// <remarks>
    /// X = left/right,  Y = up/down, Z = forward/back
    /// default values to be top left
    /// </remarks>
    public Vector3 offset = new Vector3(-0.6f, 0.15f, 2.63f); // left, up, forward


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
    void LateUpdate()
    {
        // Move the tooltip to the camera position plus our offset,
        // but we have to rotate the offset so it moves with the camera's orientation.
        transform.position = target.position
                           + target.right * offset.x
                           + target.up * offset.y
                           + target.forward * offset.z;

        // Rotate the tooltip so it faces the camera.
        transform.LookAt(target.position, Vector3.up);
    }
}