using System;
using UnityEngine;
using UnityEngine.Assertions;

//[ RequireComponent(typeof(IDoorModel)) ]

public class DoorController : MonoBehaviour, IDoorController
{
    private IDoorModel doorModel;
    public IDoorModel DoorModel {
        get
        {
            return doorModel;
        }
        set
        {
            Assert.IsNotNull(value, "Door model must not be null");
            doorModel = value;
        }
    }

    private static bool triggerDebounce = false;



    public void Init()
    {
        Assert.IsNotNull(doorModel, "DoorModel field cannot be null.");

    }

    public void OnPlayerEnter(IPlayerController playerController)
    {
        Assert.IsNotNull(playerController, "Player controller must be non-null.");

        // makes it so the player can only enter the door once
        if (triggerDebounce) return;
        triggerDebounce = true;
        //
        triggerDebounce = false;


        IDoorModel targetDoor;
        Vector3 teleportPosition = new Vector3(0, 0, 0);
        Quaternion teleportRotation = new Quaternion();

        // load this door's destination scene
    }
}