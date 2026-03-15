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
    /// Updates the object's position and rotation
    /// positions object at target + offset then rotates to face
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - <c>target</c> must be assigned in the Unity Editor.
    /// - A camera tagged as 'Main Camera' must exist in the scene.
    /// Postconditions:
    /// - The object's rotation is set to face the camera (so its readable)
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