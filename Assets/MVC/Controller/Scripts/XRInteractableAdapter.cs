using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractableAdapter : IXRInteractable
{
    private readonly XRBaseInteractable _interactable;

    public XRInteractableAdapter(XRBaseInteractable interactable)
    {
        _interactable = interactable;
    }

    public event UnityAction<HoverEnterEventArgs> HoverEntered
    {
        add => _interactable.hoverEntered.AddListener(value);
        remove => _interactable.hoverEntered.RemoveListener(value);
    }

    public event UnityAction<HoverExitEventArgs> HoverExited
    {
        add => _interactable.hoverExited.AddListener(value);
        remove => _interactable.hoverExited.RemoveListener(value);
    }
}