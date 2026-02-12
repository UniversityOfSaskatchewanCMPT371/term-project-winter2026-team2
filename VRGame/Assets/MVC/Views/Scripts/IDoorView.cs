

using UnityEngine;
public interface IDoorView
{
    public IDoorController DoorController {get; set;}

    public void Init();

    public void OnTriggerEnter(Collider other);
}