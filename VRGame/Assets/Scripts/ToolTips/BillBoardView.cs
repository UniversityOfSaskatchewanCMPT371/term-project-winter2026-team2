using UnityEngine;

/// <summary>
/// Keeps a GameObject positioned relative to camera's view
/// set offset to place it on top left of screen
/// </summary>
public class BillBoardView : MonoBehaviour
{
    /// <summary>
    /// the main camera. Must be assigned in the Unity Editor.
    /// </summary>
    public Transform target; 

    /// <summary>
    /// Offset from the target's position
    /// </summary>
    /// <remarks>
    /// X = left/right,  Y = up/down, Z = forward/back
    /// default values to be top left
    /// </remarks>
    public Vector3 offset = new Vector3(0.0f, 0.0f, 2.63f); // middle, forward

    /// <summary>
    /// Additional tilt applied after facing the camera (in degrees).
    /// </summary>
    public float tiltAngle = -10f; // tilt bottom side towards camera

    /// <summary>
    /// Updates the object's position and rotation
    /// positions object at target + offset then rotates to face
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - <c>target</c> must be assigned in the Unity Editor.
    /// - A camera tagged as 'Main Camera' must exist in the scene.
    /// Postconditions:
    /// - The object's rotation is set to face the camera with extra tilt (so its readable)
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

        // Then apply an additional tilt around the local X axis.
        transform.Rotate(Vector3.right, tiltAngle, Space.Self);
    }
}