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
        if (grab == null)
        {
            Debug.LogError("XR Grab interactable failed to set on Init");
            Assert.IsNotNull(grab, "grab component must not be null on SnapFitView");
            return;
        }

        grab.selectEntered.AddListener(OnGrabbed);
        grab.selectExited.AddListener(OnReleased);
    }

    /// </inheritdoc/>
    public void OnGrabbed(SelectEnterEventArgs args)
    {
        if (controllerInstance == null)
        {
            Debug.LogError("controllerInstance is null on OnGrabbed");
            Assert.IsNotNull(controllerInstance, "controllerInstance must not be null on OnGrabbed");
        }
        controllerInstance.Detach();
        // Reset to upright orientation
        // AI generated ideas referenced: 
        //  -   Quaternioin.Euler(x, y, z) 
        //  -   Vector3 euler = transform.eulerAngles; 
        //  -   transform.rotation = Quaternion.Euler(0f, euler.y, 0f); 
        Vector3 upright = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, upright.y, 0f);
    }

    /// </inheritdoc/>
    public void OnReleased(SelectExitEventArgs args)
    {
        if (controllerInstance == null)
        {
            Debug.LogError("controllerInstance is null on OnReleased");
            Assert.IsNotNull(controllerInstance, "controllerInstance must not be null on OnReleased");
        }
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
