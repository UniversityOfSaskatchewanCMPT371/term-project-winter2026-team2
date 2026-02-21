using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public interface IXRInteractable
{
    event UnityAction<HoverEnterEventArgs> HoverEntered;
    event UnityAction<HoverExitEventArgs> HoverExited;
}