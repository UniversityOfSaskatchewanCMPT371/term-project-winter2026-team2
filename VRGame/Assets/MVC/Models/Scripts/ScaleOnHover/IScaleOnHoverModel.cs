using UnityEngine;

/// <summary>
/// Interface for the ScaleOnHoverModel to communicate with the Controller layer
/// </summary>
/// 
public interface IScaleOnHoverModel
{
    /// <summary>
    /// Public accessor method for linked objects
    /// </summary>
    public Transform[] LinkedObjects;
    {
        /// <summary>
        /// Getter method for linked objects
        /// </summary>
        /// <post-condition>
        ///     - Returns the array of linked transform objects
        /// </post-condition>
        get;

        /// <summary>
        /// Setter method for linked objects
        /// </summary>
        /// <pre-condition>
        ///     -   value != null && value.length > 0
        /// </pre-condition>
        /// <post-condition>
        ///     -   linkedObjects is updated to the new array of transform objects
        /// </post-condition>
        set;
    }



    /// <summary>
    /// Public accessor method for hover scale multiplier
    /// </summary>
    public float HoverScaleMultiplier
    {
        /// <summary>
        /// Getter method for hover scale multiplier
        /// </summary>
        /// <post-condition>
        ///     -   Returns the hover scale multiplier value
        /// </post-condition>
        get;

        /// <summary>
        /// Setter method for hover scale multiplier
        /// </summary>
        /// <pre-condition>
        ///     -   value > 0
        /// </pre-condition>
        /// <post-condition>
        ///     -   Hover scale multiplier is updated to the new value
        /// </post-condition>
        set;
    }



    /// <summary>
    /// Public accessor method for scale speed
    /// </summary>
    public float ScaleSpeed
    {
        /// <summary>
        /// Getter method for scale speed
        /// </summary>
        /// <post-condition>
        ///     -   Returns the scale speed value
        /// </post-condition>
        get;

        /// <summary>
        /// Setter method for scale speed
        /// </summary>
        /// <pre-condition>
        ///     -   value > 0
        /// </pre-condition>
        /// <post-condition>
        ///     -   Scale speed is updated to the new value
        /// </post-condition>
        set;
    }



    /// <summary>
    /// Public accessor method for normal scales
    /// </summary>
    public Vector3[] NormalScales
    {
        /// <summary>
        /// Getter method for normal scales
        /// </summary>
        /// <post-condition>
        ///     -   Returns the array of normal scales for linked objects
        /// </post-condition>
        get;

        /// Note: No setter for normal scales since they are initialized based 
        ///         on the linked objects' original scales 
        ///         (they should not be arbitrarily changed)
    }   

    

    /// <summary>
    /// Public accessor method for target scales
    /// </summary>
    public Vector3[] TargetScales
    {
        /// <summary>
        /// Getter method for target scales
        /// </summary>
        /// <post-condition>
        ///     -   Returns the array of target scales for linked objects
        /// </post-condition>
        get;

        /// <summary>
        /// Setter method for target scales
        /// </summary>
        /// <pre-condition>
        ///     -   value != null
        /// </pre-condition>
        /// <post-condition>
        ///     -   Target scales are updated to the new values
        /// </post-condition>
        set;
    }



    /// <summary>
    /// Public accessor method for isHovering
    /// </summary>
    public bool IsHovering
    {
        /// <summary>
        /// Getter method for isHovering
        /// </summary>
        /// <post-condition>
        ///     -   Returns true if the object is currently being hovered, false otherwise
        /// </post-condition>
        get;

        /// Note: No setter for isHovering since it should only be changed 
        ///         through the OnHoverEnter and OnHoverExit methods
    }


    /// <summary>
    /// Initializes the model with parameters
    /// </summary>
    /// <param name="linkedObjects">Objects linked to the script</param>
    /// <param name="hoverScaleMultiplier">How big the linkedObject will grow</param>
    /// <param name="scaleSpeed">Speed of scale transitions</param>
    /// <pre-condition>
    ///     -   linkedObjects != null && linkedObjects.length > 0
    ///     -   hoverScaleMultiplier > 0
    ///     -   scaleSpeed > 0
    /// </pre-condition>
    /// <post-condition>
    ///     -   Model is initialized with the parameters
    /// </post-condition>
    public void Initialize(Transform[] linkedObjects, float hoverScaleMultiplier, float scaleSpeed);



    /// <summary>
    /// Initializes the normal and target scales for objects linked
    /// </summary>
    /// <pre-condition>
    ///      -   linkedObject != null && linkedObject > 0
    /// </pre-condition>
    /// <post-condition>
    ///     -   The normal scale and target scale for a linked object is initialized
    /// </post-condition>
    private void InitializeScales();
    



    /// <summary>
    /// Called when hover enters - sets target scales to bigger values
    /// </summary>
    /// <pre-condition> 
    ///     -   linkedObjects must exist and not be null
    /// </pre-condition>
    /// <post-condition> 
    ///     -   Target scales are set to be bigger (1.25x)
    /// </post-condition>
    public void OnHoverEnter();



    /// <summary>
    /// Called when hover exits - sets target scales back to normal
    /// </summary>
    /// <pre-condition> 
    ///     -   linkedObjects must exist and not be null
    /// </pre-condition>
    /// <post-condition> 
    ///     -   Target scales goes back to normal scale
    /// </post-condition>
    public void OnHoverExit();



    /// <summary>
    /// Initialize scales on Awake (to the linkedObjects that are assigned in inspector)
    /// </summary>
    /// <pre-condition>
    ///     -   0 < linkedObjects && linkedObjects != null
    /// </pre-condition>
    /// <post-condition>
    ///     - Initializes the linkedObjects on Awake()
    /// </post-condition>
    private void Awake();

}
