
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

    public void OnPlayerEnter(GameObject playerRig)
    {
        // check for playerig having player logic TODO - does not exist yet
        //Assert.IsNotNull(playerRig.GetComponent<IPlayerController>());

        // makes it so the player can only enter the door once
        if (triggerDebounce) return;
        triggerDebounce = true;

        triggerDebounce = false;
    }
}