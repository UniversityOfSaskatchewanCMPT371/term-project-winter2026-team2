using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Controller component of BrainController.
/// </summary>
public class BrainController : Controller<IModel, IView>, IBrainController
{
    /// <inheritdoc/>
    public override void Init()
    {
        // Validate model and view components
        this.CheckModelRef();
        this.CheckViewRef();
    }

    /// <inheritdoc/>
    public void OnHoverEnter()
    {
        modelInstance.pause();
    }

    /// <inheritdoc/>
    public void OnHoverExit()
    {
        moodelInstance.resume();
    }
}
