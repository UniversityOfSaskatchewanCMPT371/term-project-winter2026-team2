using UnityEngine;

public class BillBoardView : MonoBehaviour
{
    static Transform tCam = null;
    void Update ()
    {
        if(!tCam)
        {
            if(!Camera.main)
            {
                return;
            }
            tCam = Camera.main.transform;
        }
        transform.LookAt(tCam.position, Vector3.up);
    }
}