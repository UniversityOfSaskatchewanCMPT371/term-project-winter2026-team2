using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolTipTrigger : MonoBehaviour

{
    public GameObject interactiveElement;
    public XRBaseInteractable interactable;


    public void show(bool state)
    {
        interactiveElement.SetActive(state);
    }
    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        interactiveElement.SetActive(false);
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        show(true);
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        show(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
