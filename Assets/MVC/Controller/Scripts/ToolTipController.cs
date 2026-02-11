using UnityEngine;

public class ToolTipController : MonoBehaviour
{
    public ToolTipView view;
    public Transform playerCamera;
    
    private void Awake()
    {
        view = GetComponentInChildren<ToolTipView>();
        playerCamera = Camera.main.transform;
        view.Toggle(false);
    }

    void Start()
    {
        view.Toggle(false);
    }

    public void OnHoverEnter(ToolTipModel data, Vector3 position)
    {
        view.Setup(data.title, data.description);
        transform.position = position;
        transform.LookAt(playerCamera);
        view.Toggle(true);
    }

    public void OnHoverExit()
    {
        view.Toggle(false);
    }

    private void LateUpdate()
    {
        view.transform.LookAt(view.transform.position + playerCamera.forward);
    }
}