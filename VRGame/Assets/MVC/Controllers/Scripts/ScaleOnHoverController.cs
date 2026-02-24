using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

[assembly: InternalsVisibleTo("Tests")]

/// <summary>
/// Controller class manages the interaction between Model and View layers
/// </summary>
public class ScaleOnHoverController : MonoBehaviour, IScaleOnHoverController
{
    [SerializeField] private ScaleOnHoverModel Model;
    [SerializeField] private ScaleOnHoverView View;

    /// Internal references to the model and view layer. This lets us use mocks to substitute 
    /// for the model and view in tests, while still allowing us to assign them in the inspector
    /// for ease of use in the editor.
    internal IScaleOnHoverView view;
    internal IScaleOnHoverModel model;

    /// <summary>
    /// Calls the Init() method to initialize and validate that the model and view layer exist
    /// </summary>
    public void Start()
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
    public void Init()
    {
        /// If model or view is not assigned in the inspector, attempt to get them from the same GameObject
        if (model == null)
        {
            if (Model != null) /// Checks if Model is assigned in the inspector and uses it if available
            {
                model = Model;
            }
            else /// Else try to get model component from the same GameObject
            {
                model = GetComponent<ScaleOnHoverModel>();
            }
        }
        if (view == null) /// Checks if View is assigned in the inspector and uses it if available
        {
            if (View != null)
            {
                view = View;
            }
            else /// Else try to get view component from the same GameObject
            {
                view = GetComponent<ScaleOnHoverView>();
            }
        }
        Assert.IsNotNull(model, "Model layer does not exist");
        Assert.IsNotNull(view, "View Layer does not exist");
    }


    /// <summary>
    /// Retrieves linked objects from model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Objects linked to the script are returned
    /// </post-condition>
    public Transform[] retrieveLinkedObjects() 
    {
        /// Check if model reference is null
        if (model == null) 
        {
            Debug.LogError("Model reference cannot be null");
            return null;
        }

        /// Assert to ensure model reference is not null before retrieving linked objects
        Assert.IsNotNull(model, "Model reference cannot be null");
        return model.LinkedObjects;
    }



    /// <summary>
    /// Retrieves target scale from model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Target scale(s) are returned
    /// </post-condition>
    public Vector3[] retrieveTargetScale()
    {
        /// Check if model reference is null
        if (model == null) 
        {
            Debug.LogError("Model reference cannot be null");
            return null;
        }

        /// Assert to ensure model reference is not null before retrieving target scale
        Assert.IsNotNull(model, "Model reference cannot be null");
        return model.TargetScales;
    }


    
    /// <summary>
    /// Retrieves scale speed from the model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   The model's scale speed is returned
    public float retrieveScaleSpeed() 
    {
        /// Check if model reference is null
        if (model == null) 
        {
            Debug.LogError("Model reference is null");
            return 1f;
        }

        /// Assert to ensure model reference is not null before retrieving scale speed
        Assert.IsNotNull(model, "Model reference cannot be null");

        return model.ScaleSpeed;
    }



    /// <summary>
    /// Returns current hover state of the model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Returns True if model is hovered on, false otherwise
    /// </post-condition>
    public bool IsHovering()
    {
        /// Check if model reference is null
        if (model == null) 
        {
            Debug.LogError("Model reference is null in IsHovering");
            return false;
        }
        /// Assert to ensure model reference is not null before checking hover state
        Assert.IsNotNull(model, "Model reference cannot be null in IsHovering");
        
        return model.IsHovering;
    }

    

    /// <summary>
    /// Hover enter event handler
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverEnter() is called in model
    /// </post-condition>
    public void OnHoverEnter()
    {
        /// Check if model reference is null
        if (model == null)
        {
            Debug.LogError("Model reference is null in OnHoverEnter");
            return;
        }

        /// Assert to ensure model reference is not null before calling OnHoverEnter
        Assert.IsNotNull(model, "Model reference cannot be null in OnHoverEnter");

        model.OnHoverEnter();
    }

    /// <summary>
    /// Hover exit event handler
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverExit() is called in model
    /// </post-condition>
    public void OnHoverExit()
    {
        /// Check if model reference is null
        if (model == null)
        {
            Debug.LogError("Model reference is null in OnHoverExit");
            return;
        }

        /// Assert to ensure model reference is not null before calling OnHoverExit
        Assert.IsNotNull(model, "Model reference cannot be null in OnHoverExit");

        model.OnHoverExit();
    }
}
