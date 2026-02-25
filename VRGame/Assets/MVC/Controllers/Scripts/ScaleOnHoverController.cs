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
    /*
    Internal references to the model and view layer. This lets us use mocks to substitute 
    for the model and view in tests, while still allowing us to assign them in the inspector
    for ease of use in the editor.
    */
    internal IScaleOnHoverView view;
    internal IScaleOnHoverModel model;

    /// <inheritdoc/>
    public void Start()
    {
        Init();
    }

    /// <inheritdoc/>
    public void Init()
    {
        // If model or view is null, create them by getting the component from the GameObject
        if (model == null)
        {
            if (Model != null) // Checks if Model is assigned in the inspector and uses it if available
            {
                model = Model;
            }
            else // Else try to get model component from the same GameObject
            {
                model = GetComponent<ScaleOnHoverModel>();
            }
        }
        if (view == null) // Checks if View is assigned in the inspector and uses it if available
        {
            if (View != null)
            {
                view = View;
            }
            else // Else try to get view component from the same GameObject
            {
                view = GetComponent<ScaleOnHoverView>();
            }
        }
        Assert.IsNotNull(model, "Model layer does not exist");
        Assert.IsNotNull(view, "View Layer does not exist");
    }


    /// <inheritdoc/>
    public Transform[] retrieveLinkedObjects() 
    {
        if (model == null) 
        {
            Debug.LogError("Model reference cannot be null");
            return null;
        }

        // Assert to ensure model reference is not null before retrieving linked objects
        Assert.IsNotNull(model, "Model reference cannot be null");
        return model.LinkedObjects;
    }



    /// <inheritdoc/>
    public Vector3[] retrieveTargetScale()
    {
        if (model == null) 
        {
            Debug.LogError("Model reference cannot be null");
            return null;
        }

        // Assert to ensure model reference is not null before retrieving target scale
        Assert.IsNotNull(model, "Model reference cannot be null");
        return model.TargetScales;
    }


    
    /// <inheritdoc/>
    public float retrieveScaleSpeed() 
    {
        if (model == null) 
        {
            Debug.LogError("Model reference is null");
            return 1f;
        }

        // Assert to ensure model reference is not null before retrieving scale speed
        Assert.IsNotNull(model, "Model reference cannot be null");

        return model.ScaleSpeed;
    }



    /// <inheritdoc/>
    public bool IsHovering()
    {
        if (model == null) 
        {
            Debug.LogError("Model reference is null in IsHovering");
            return false;
        }
        // Assert to ensure model reference is not null before checking hover state
        Assert.IsNotNull(model, "Model reference cannot be null in IsHovering");
        
        return model.IsHovering;
    }

    

    /// <inheritdoc/>
    public void OnHoverEnter()
    {
        if (model == null)
        {
            Debug.LogError("Model reference is null in OnHoverEnter");
            return;
        }

        // Assert to ensure model reference is not null before calling OnHoverEnter
        Assert.IsNotNull(model, "Model reference cannot be null in OnHoverEnter");

        model.OnHoverEnter();
    }

    /// <inheritdoc/>
    public void OnHoverExit()
    {
        if (model == null)
        {
            Debug.LogError("Model reference is null in OnHoverExit");
            return;
        }

        // Assert to ensure model reference is not null before calling OnHoverExit
        Assert.IsNotNull(model, "Model reference cannot be null in OnHoverExit");

        model.OnHoverExit();
    }
}
