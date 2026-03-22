using UnityEngine;

/// <summary>
/// Interface for the ScaleOnHoverController to communicate with the View and Model layers
/// 
public interface IScaleOnHoverController
{
    /// <summary>
    /// Calls the Init() method to initialize and validate that the model and view layer exist
    /// </summary>
    void Start();

    /// <summary>
    /// Validate that the model and view layer exist
    /// </summary>
    /// <pre-condition>
    ///     -   model and view layers must exist
    /// </pre-condition>
    /// <post-condition>
    ///     -   Controller holds reference to Model and View layer
    /// </post-condition>
    void Init();

    /// <summary>
    /// Retrieves linked objects from model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Objects linked to the script are returned
    /// </post-condition>
    Transform[] retrieveLinkedObjects();

    /// <summary>
    /// Retrieves target scale from model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Target scale(s) are returned
    /// </post-condition>
    Vector3[] retrieveTargetScale();

    /// <summary>
    /// Retrieves scale speed from the model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   The model's scale speed is returned
    float retrieveScaleSpeed();

    /// <summary>
    /// Returns current hover state of the model
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   Returns True if model is hovered on, false otherwise
    /// </post-condition>
    bool IsHovering();

    /// <summary>
    /// Hover enter event handler
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   OnHoverEnter() is called in model
    /// </post-condition>
    void OnHoverEnter();

    /// <summary>
    /// Hover exit event handler.
    /// This ensures only one brain region is hovered at a time by cancelling out other active (scaled) ones
    /// </summary>
    /// <pre-condition>
    ///     -   model != null
    /// </pre-condition>
    /// <post-condition>
    ///     -   ensures only one brain region is hovered at a time
    ///     -   OnHoverExit() is called in model
    /// </post-condition>
    void OnHoverExit();
}
