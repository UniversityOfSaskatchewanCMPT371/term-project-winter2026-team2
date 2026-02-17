using UnityEngine;

public class BillBoardView : MonoBehaviour
{
    public Transform PointB;

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
            PointB.transform.localPosition = new Vector3(0, 0, Vector3.Distance(transform.position, tCam.position));
        }
        transform.LookAt(tCam.position, Vector3.up);
    }
}