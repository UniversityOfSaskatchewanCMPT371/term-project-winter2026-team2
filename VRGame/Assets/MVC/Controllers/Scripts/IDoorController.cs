
using UnityEngine;
public interface IDoorController
{
    public IDoorModel DoorModel {get; set;}
    void OnPlayerEnter(IPlayerController player);

    public void Init();
}