using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Controller component of BrainController that manages hover interactions
/// </summary>
public class BrainController : Controller<IBrainModel, IBrainView>, IBrainController
{
    /// <inheritdoc/>
    public override void Init()
    {
        if (modelInstance != null)
        {
            Debug.LogError("Model instance already exists");
        }
        if (viewInstance != null)
        {
            Debug.LogError("View instance already exists");
        }
    
        this.CheckModelRef();
        this.CheckViewRef();

        Assert.IsNotNull(modelInstance, "Model instance failed to initialize in BrainController");
        Assert.IsNotNull(viewInstance, "View instance failed to initialize in BrainController");
    }

    /// <inheritdoc/>
    public void OnHoverEnter()
    {
        if (modelInstance == null)
        {
            Debug.LogError("Model instance is null on hover enter");
        }
        modelInstance.pause();
    }

    /// <inheritdoc/>
    public void OnHoverExit()
    {
        if (modelInstance == null)
        {
            Debug.LogError("Model instance is null on hover enter");
        }
        modelInstance.resume();
    }
}
