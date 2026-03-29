using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// View component of SnapFitView.
/// </summary>
public class SnapFitView : View<ISnapFitController>, ISnapFitView
{
    /// <summary>
    /// Reference to the XRGrabInteractable component on this block
    /// </summary>
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
    public void OnGrabbed(SelectEnterEventArgs args)
    {
        controllerInstance.Detach();
    }

    /// </inheritdoc/>
    public void OnReleased(SelectExitEventArgs args)
    {
        controllerInstance.Snap();
    }

    /// </inheritdoc>
    public void OnDestroy()
    {
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabbed);
            grab.selectExited.RemoveListener(OnReleased);
        }
    }
}
