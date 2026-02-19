using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller class manages the interaction between Model and View layers
/// </summary>
public class ScaleOnHoverController : MonoBehaviour, IScaleOnHoverController
{
    [SerializeField] private IScaleOnHoverModel model;
    [SerializeField] private IScaleOnHoverView view;

    /// <summary>
    /// Validate that the model and view layer exist
    /// </summary>
    /// Pre-condition:
    ///     -   model and view layers must exist
    /// Post-condition:
    ///     -   Controller holds reference to Model and View layer
    private void Init()
    {
        Assert.IsNotNull(model, "Model layer does not exist");
        Assert.IsNotNull(view, "View Layer does not exist");
    }

    /// <summary>
    /// Retrieves linked objects from model
    /// </summary>
    /// Pre-condition:
    ///     -   model != null
    /// Post-condition:
    ///     -   Objects linked to the script are returned
    public Transform[] retrieveLinkedObjects() 
    {
        return model.getLinkedObjects();
    }

    /// <summary>
    /// Retrieves target scale from model
    /// </summary>
    /// Pre-condition:
    ///     -   model != null
    /// Post-condition:
    ///     -   Target scale(s) are returned
    public Vector3[] retrieveTargetScale()
    {
        return model.getTargetScale();
    }
    
    /// <summary>
    /// Retrieves scale speed from the model
    /// </summary>
    /// Pre-condition:
    ///     -   model != null
    /// Post-condition:
    ///     -   The model's scale speed is returned
    public float retrieveScaleSpeed() 
    {
        return model.getScaleSpeed();
    }

    /// <summary>
    /// Returns current hover state of the model
    /// </summary>
    /// Pre-condition:
    ///     -   model != null
    /// Post-condition:
    ///     -   Returns True if model is hovered on, false otherwise
    public bool IsHovering()
    {
        return model.IsHovering();
    }

    /// <summary>
    /// Hover enter event handler
    /// </summary>
    /// Pre-condition:
    ///     -   model != null
    /// Post-condition:
    ///     -   OnHoverEnter() is called in model
    public void OnHoverEnter()
    {
        model.OnHoverEnter();
    }

    /// <summary>
    /// Hover exit event handler
    /// </summary>
    /// Pre-condition:
    ///     -   model != null
    /// Post-condition:
    ///     -   OnHoverExit() is called in model
    public void OnHoverExit()
    {
        model.OnHoverExit();
    }
}
