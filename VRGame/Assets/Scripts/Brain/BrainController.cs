using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Controller component of BrainController that manages hover interactions
/// </summary>
public class BrainController : Controller<IBrainModel, IBrainView>, IBrainController
{
    /// <summary>
    /// An integer counter for brain regions hovered
    /// </summary>
    private int hoverCount = 0;

    /// <inheritdoc/>
    public void Awake()
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Start()
    {}

    /// <inheritdoc/>
    public override void Init()
    {
        if (modelInstance != null)
        {
            Debug.LogError("Model instance already exists");
        }
        Assert.IsNull(modelInstance, "Model instance must be null prior to initialization");
        if (viewInstance != null)
        {
            Debug.LogError("View instance already exists");
        }
        Assert.IsNull(viewInstance, "View instance must be null prior to initialization");
    
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
        Assert.IsNotNull(modelInstance, "Model instance must be initialized before hover enter");
        // Increment count of regions hovered
        hoverCount++;
        if (hoverCount == 1)
        {
            modelInstance.pause();
        }
    }

    /// <inheritdoc/>
    public void OnHoverExit()
    {
        if (modelInstance == null)
        {
            Debug.LogError("Model instance is null on hover exit");
        }
        Assert.IsNotNull(modelInstance, "Model instance must be initialized before hover exit");
        // Decrement count of regions hovered
        hoverCount--;
        if (hoverCount <= 0)
        {
            // Reset to 0 if counter touches negative
            hoverCount = 0;
            modelInstance.resume();
        }
    }
}
