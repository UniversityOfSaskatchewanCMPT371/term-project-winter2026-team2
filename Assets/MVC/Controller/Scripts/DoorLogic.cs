using UnityEngine;
using UnityEngine.Assertions;

[ RequireComponent(typeof(DoorData)) ]
public class DoorLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public DoorData doorData;
    public SceneChanger sceneChanger;
    public int exitDoorId;

    void Awake()
    {
        Assert.IsNotNull(doorData, "DoorData field cannot be null.");
        Assert.IsNotNull(sceneChanger, "SceneChanger field cannot be null.");
    }

    public void OnPlayerEnter(Collider other)
    {
        Scenes sceneIdx = doorData.sceneDestination;

        sceneChanger.LoadScene(sceneIdx);
    }
}
