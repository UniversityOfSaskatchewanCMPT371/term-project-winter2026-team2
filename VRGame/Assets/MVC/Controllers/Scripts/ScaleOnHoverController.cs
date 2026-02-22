using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller class manages the interaction between Model and View layers
/// </summary>
public class ScaleOnHoverController : MonoBehaviour, IScaleOnHoverController
{
    [SerializeField] private ScaleOnHoverModel model;
    [SerializeField] private ScaleOnHoverView view;

    /// <summary>
    /// Initialize and validate that the model and view layer exist
    /// </summary>
    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Validate that the model and view layer exist
    /// </summary>
    /// <pre-condition>
    ///     -   model and view layers must exist
    /// </pre-condition>
    /// <post-condition>
    ///     -   Controller holds reference to Model and View layer
    /// </post-condition>
    private void Init()
    {
        /// If model or view is not assigned in the inspector, attempt to get them from the same GameObject
        if (model == null)
        {
            model = GetComponent<ScaleOnHoverModel>();
        }
        if (view == null)
        {
            view = GetComponent<ScaleOnHoverView>();
        }

        /// Assert to ensure model and view are not null
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
        if (model == null) 
            return null;
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
        if (model == null) 
            return null;
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
        if (model == null) return 1f;
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
        if (model == null) 
            return false;
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
        if (model != null)
        {
            model.OnHoverEnter();
        }
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
        if (model != null)
        {
            model.OnHoverExit();
        }
    }
}
