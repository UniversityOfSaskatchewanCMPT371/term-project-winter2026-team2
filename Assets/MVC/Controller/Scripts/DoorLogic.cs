using System;
using UnityEngine;
using UnityEngine.Assertions;

[ RequireComponent(typeof(DoorData)) ]

/// <summary>
/// Handles door interaction logic
/// </summary>
public class DoorLogic : MonoBehaviour
{
    public DoorData doorData;                   // Reference to the data attached to this object
    private SceneChanger sceneChanger;          // Reference to the global scene changer service

    /// <summary>
    /// Validates required fields and retrieves the SceneChanger service.
    /// </summary>
    private void Awake()
    {
        // Ensure doorData is assigned in the inspector
        Assert.IsNotNull(doorData, "DoorData field cannot be null.");

        // Locate the global Services object and extract the SceneChanger.
        sceneChanger = GameObject.Find("Services").GetComponent<Services>().sceneChanger;
    }

    /// <summary>
    /// Called when the player enters this door's collider.
    /// Loads the destination scene, and teleports the player.
    /// </summary>
    /// <param name="playerRig">The player's XR rig GameObject.</param>
    public void OnPlayerEnter(GameObject playerRig)
    {
        // TODO: Throw an exception if doorData is missing or invalid

        DoorData targetDoor;
        Vector3 teleportPosition = new Vector3(0, 1, 0);
        Quaternion teleportRotation = new Quaternion();

        try
        {
            targetDoor = doorData.GetTargetDoor();
            teleportPosition = targetDoor.GetTeleportPosition();
            teleportRotation = targetDoor.GetTeleportRotation();
        }
        catch (Exception e)
        {
            // Exception caught properly
        }

        // Load the destination scene
        Scenes sceneIdx = doorData.sceneDestination;
        AsyncOperation loadingScene = sceneChanger.LoadScene(sceneIdx);

        // When the scene finishes loading, teleport the player rig
        loadingScene.completed += (o) =>
        {
            playerRig.transform.SetPositionAndRotation(teleportPosition, teleportRotation);
        };
    }
}