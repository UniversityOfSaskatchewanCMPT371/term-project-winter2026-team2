using UnityEngine;

public class BillBoardView : MonoBehaviour
{
    void LateUpdate()
    {
        Camera mainCamera = Camera.main;

        if (!mainCamera) return;

        transform.LookAt(transform.position + mainCamera.transform.position);
    }
}