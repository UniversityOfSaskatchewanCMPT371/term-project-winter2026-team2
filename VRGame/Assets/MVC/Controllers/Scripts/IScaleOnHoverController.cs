using UnityEngine;

/// <summary>
/// Interface for the ScaleOnHoverController to communicate with the View and Model layers
/// 
public interface IScaleOnHoverController
{
    /// <summary>
    /// Calls the Init() method to initialize and validate that the model and view layer exist
    /// </summary>
    private void Start();

    /// <summary>
    /// Validate that the model and view layer exist
    /// </summary>
    /// <pre-condition>
    ///     -   model and view layers must exist
    /// </pre-condition>
    /// <post-condition>
    ///     -   Controller holds reference to Model and View layer
    /// </post-condition>
    private void Init();

    /// <summary>
    /// Retrieves linked objects from model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Objects linked to the script are returned
    /// </post-condition>
    public Transform[] retrieveLinkedObjects();

    /// <summary>
    /// Retrieves target scale from model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Target scale(s) are returned
    /// </post-condition>
    public Vector3[] retrieveTargetScale();

    /// <summary>
    /// Retrieves scale speed from the model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   The model's scale speed is returned
    public float retrieveScaleSpeed();

    /// <summary>
    /// Returns current hover state of the model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Returns True if model is hovered on, false otherwise
    /// </post-condition>
    public bool IsHovering();

    /// <summary>
    /// Hover enter event handler
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverEnter() is called in model
    /// </post-condition>
    public void OnHoverEnter();

    /// <summary>
    /// Hover exit event handler
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverExit() is called in model
    /// </post-condition>
    public void OnHoverExit();
}
