using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

[ RequireComponent(typeof(DoorData)) ]
public class DoorLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public DoorData doorData;
    public SceneChanger sceneChanger;

    void Awake()
    {
        Assert.IsNotNull(doorData, "DoorData field cannot be null.");
        Assert.IsNotNull(sceneChanger, "SceneChanger field cannot be null.");
    }

    public void OnPlayerEnter(GameObject playerRig)
    {
        if (!playerRig.CompareTag("Player")) return;

        DoorData targetDoor;
        Vector3 teleportPosition = new Vector3();
        Quaternion teleportRotation = new Quaternion();

        try
        {
            targetDoor = doorData.GetTargetDoor();
            teleportPosition = targetDoor.GetTeleportPosition();
            teleportRotation = targetDoor.GetTeleportRotation();
        } catch (Exception e)
        {
            // Exception caught properly
        }

        Scenes sceneIdx = doorData.sceneDestination;
        sceneChanger.LoadScene(sceneIdx);

        playerRig.transform.SetPositionAndRotation(teleportPosition, teleportRotation);
    }
}
