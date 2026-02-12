
using System.Numerics;
using UnityEditor.Build.Reporting;

public interface IDoorModel {

    int DoorId{get; set;}
    int TargetDoorId{get; set;}
    IDoorModel GetTargetDoor();
    public void Init();

}