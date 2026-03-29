using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// View component of SnapFitView.
/// </summary>
public class SnapFitView : View<ISnapFitController>, ISnapFitView
{
    private XRGrabInteractable grab;

    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckControllerRef();

        grab = GetComponent<XRGrabInteractable>();
        Assert.IsNotNull(grab, "grab component must not be null on SnapFitView");

        grab.selectEntered.AddListener(OnGrabbed);
        grab.selectExited.AddListener(OnReleased);
    }

    /// </inheritdoc/>
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        controllerInstance.Detach();
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        controllerInstance.TrySnap();
    }

    /// </inheritdoc>
    private void OnDestroy()
    {
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabbed);
            grab.selectExited.RemoveListener(OnReleased);
        }
    }
}
