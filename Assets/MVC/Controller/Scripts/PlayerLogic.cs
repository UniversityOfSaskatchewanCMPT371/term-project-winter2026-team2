using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerLogic : MonoBehaviour
{
    public GameObject leftController;                   // Reference to the player's left controller object
    public GameObject rightController;                  // Reference to the player's right controller object

    /// <summary>
    /// Resets the left/right controller's ray interactor so that they are consistent
    /// with the player's rig position.
    /// </summary>
    private void resetRayInteractor()
    {
        XRRayInteractor leftRayInteractor = leftController.GetComponentInChildren<XRRayInteractor>();
        XRRayInteractor rightRayInteractor = rightController.GetComponentInChildren<XRRayInteractor>();

        // This thing is causing an error. Ill replace this when I have time
        // to learn more about ray interactor stuff

        leftRayInteractor.enabled = false;
        rightRayInteractor.enabled = false;
        leftRayInteractor.enabled = true;
        rightRayInteractor.enabled = true;
    }

    /// <summary>
    /// Teleports the player to a specified destination oriented in a specified rotation.
    /// </summary>
    /// <param name="position">The position in which the player rig is teleported to.</param>
    /// <param name="rotation">The position in which the player rig is oriented to.</param>
    public void teleportPlayerTo(Vector3 position, Quaternion rotation)
    {
        gameObject.transform.SetPositionAndRotation(position, rotation);
        resetRayInteractor();
    }
}
