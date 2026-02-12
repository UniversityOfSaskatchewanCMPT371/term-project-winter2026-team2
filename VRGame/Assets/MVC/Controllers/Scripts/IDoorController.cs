
using UnityEngine;
public interface IDoorController
{
    public IDoorModel DoorModel {get; set;}
    void OnPlayerEnter(GameObject playerRig);

    public void Init();
}