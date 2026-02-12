

using UnityEngine;
using UnityEngine.Assertions;
public class DoorView : MonoBehaviour, IDoorView
{
    private IDoorController doorController;

    public IDoorController DoorController
    {
        get
        {
            return doorController;
        }
        set
        {
            Assert.IsNotNull(value, "doorController cannot be null.");
            doorController = value;
        }
    }


    public void Init()
    {
        Assert.IsNotNull(doorController, "Field doorController cannot be null.");
    }

    private void Start()
    {
        Init();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("MainCamera")) return;
    }

}