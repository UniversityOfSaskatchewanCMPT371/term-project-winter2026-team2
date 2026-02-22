using UnityEngine;

/// <summary>
/// Model class holds the data and state for scale-on-hover functionality
/// </summary>
public class ScaleOnHoverModel : MonoBehaviour, IScaleOnHoverModel
{
    /// <summary>
    /// Array of linked objects that will be scaled when hovered
    /// </summary>
    [SerializeField] private Transform[] linkedObjects;

    /// <summary>
    /// Public accessor method for linked objects
    /// </summary>
    public Transform[] LinkedObjects
    {
        /// <summary>
        /// Getter method for linked objects
        /// </summary>
        /// <post-condition>
        ///     - Returns the array of linked transform objects
        /// </post-condition>
        get
        {
            return linkedObjects;
        }

        /// <summary>
        /// Setter method for linked objects
        /// </summary>
        /// <pre-condition>
        ///     -   value != null && value.length > 0
        /// </pre-condition>
        /// <post-condition>
        ///     -   linkedObjects is updated to the new array of transform objects
        /// </post-condition>
        set
        {
            if (value == null || value.Length == 0)
            {
                Debug.LogError("Can't set linked objects to null or empty array");
                return;
            }
            Assert.IsTrue(value != null && value.Length > 0, "Linked objects array must not be null or empty");
            linkedObjects = value;
        }
    }



    /// <summary>
    /// Float multiplier for how much the linked objects should scale up when hovered
    /// </summary>
    [SerializeField] private float hoverScaleMultiplier = 1.25f;

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
        get
        {
            return hoverScaleMultiplier;
        }

        /// <summary>
        /// Setter method for hover scale multiplier
        /// </summary>
        /// <pre-condition>
        ///     -   value > 0
        /// </pre-condition>
        /// <post-condition>
        ///     -   Hover scale multiplier is updated to the new value
        /// </post-condition>
        set
        {
            if (value <= 0)
            {
                Debug.LogError("Hover scale multiplier must be zero or positive");
                return;
            }
            Assert.IsTrue(value > 0, "Hover scale multiplier must be greater than zero");
            hoverScaleMultiplier = value;
        }
    }



    [SerializeField] private float scaleSpeed = 10f;
    
    private Vector3[] normalScales;
    private Vector3[] targetScales;
    private bool isHovering = false;


    /// <summary>
    /// Initializes the model with parameters
    /// </summary>
    /// <param name="linkedObjects">Objects linked to the script</param>
    /// <param name="hoverScaleMultiplier">How big the linkedObject will grow</param>
    /// <param name="scaleSpeed">Speed of scale transitions</param>
    public void Initialize(Transform[] linkedObjects, float hoverScaleMultiplier, float scaleSpeed)
    {
        this.linkedObjects = linkedObjects;
        this.hoverScaleMultiplier = hoverScaleMultiplier;
        this.scaleSpeed = scaleSpeed;

        InitializeScales();
    }

    /// <summary>
    /// Initializes the normal and target scales for objects linked
    /// </summary>
    /// // Pre-condition:
    ///      -   linkedObject != null && linkedObject > 0
    /// // Post-condition:
    ///     -   The normal scale and target scale for a linked object is initialized
    private void InitializeScales()
    {
        if (linkedObjects == null || linkedObjects.Length == 0) return;
        
        // Initialize normal and target/bigger scale
        normalScales = new Vector3[linkedObjects.Length];
        targetScales = new Vector3[linkedObjects.Length];

        // Initialize scales 
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            if (linkedObjects[i] != null)
            {
                normalScales[i] = linkedObjects[i].localScale;
                targetScales[i] = normalScales[i];
            }
        }
    }

    /// <summary>
    /// Called when hover enters - sets target scales to bigger values
    /// </summary>
    /// Pre-condition: 
    ///     -   linkedObjects must exist and not be null
    /// Post-condition: 
    ///     -   Target scales are set to be bigger (1.25x)
    public void OnHoverEnter()
    {
        isHovering = true;
        
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            if (linkedObjects[i] != null)
            {
                targetScales[i] = normalScales[i] * hoverScaleMultiplier;
            }
        }
    }

    /// <summary>
    /// Called when hover exits - sets target scales back to normal
    /// </summary>
    /// Pre-condition: 
    ///     -   linkedObjects must exist and not be null
    /// Post-condition: 
    ///     -   Target scales goes back to normal scale
    public void OnHoverExit()
    {
        isHovering = false;
        
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            if (linkedObjects[i] != null)
            {
                targetScales[i] = normalScales[i];
            }
        }
    }

    /// <summary>
    /// Returns the target scale 
    /// </summary>
    /// Pre-condition:
    ///     -   None
    /// Post-condition:
    ///     -   Returns the objects target
    public Vector3[] getTargetScale()
    {
        return targetScales;
    }


    /// <summary>
    /// Returns the scale transition speed
    /// </summary>
    /// Pre-condition:
    ///     -   None
    /// Post-condition:
    ///     -   Returns the scale transition speed
    public float getScaleSpeed()
    {
        return scaleSpeed;
    }

    /// <summary>
    /// Gets whether the object is currently being hovered
    /// </summary>
    /// Pre-condition:
    ///     -   None
    /// Post-condition:
    ///     -   Returns True if hovering, false otherwise
    public bool IsHovering()
    {
        return isHovering;
    }

    /// <summary>
    /// Initialize scales on Awake (to the linkedObjects that are assigned in inspector)
    /// </summary>
    /// Pre-condition:
    ///     -   0 < linkedObjects && linkedObjects != null
    /// Post-condition:
    ///     - Initializes the linkedObjects on Awake()
    private void Awake()
    {
        if (linkedObjects != null && linkedObjects.Length > 0)
        {
            InitializeScales();
        }
    }

}
