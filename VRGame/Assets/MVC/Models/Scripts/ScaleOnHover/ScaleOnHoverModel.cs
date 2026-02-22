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



    /// <summary>
    /// Float speed for how quickly the linked objects should transition to their target scale
    /// </summary>
    [SerializeField] private float scaleSpeed = 10f;

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
        get
        {
            return scaleSpeed;
        }

        /// <summary>
        /// Setter method for scale speed
        /// </summary>
        /// <pre-condition>
        ///     -   value > 0
        /// </pre-condition>
        /// <post-condition>
        ///     -   Scale speed is updated to the new value
        /// </post-condition>
        set
        {
            if (value <= 0)            
            {
                Debug.LogError("Scale speed must be zero or positive");
                return;
            }
            Assert.IsTrue(value > 0, "Scale speed must be greater than zero");
            scaleSpeed = value;
        }
    }

    

    /// <summary>
    /// Array to store the normal (original)scales of linked objects
    /// </summary>
    private Vector3[] normalScales;

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
        get
        {            
            return normalScales;
        }
        /// Note: No setter for normal scales since they are initialized based 
        ///         on the linked objects' original scales 
        ///         (they should not be arbitrarily changed)
    }   

    

    /// <summary>
    /// Array to store the target scales of linked objects 
    /// </summary>
    private Vector3[] targetScales;

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
        get
        {            
            return targetScales;
        }

        /// <summary>
        /// Setter method for target scales
        /// </summary>
        /// <pre-condition>
        ///     -   value != null
        /// </pre-condition>
        /// <post-condition>
        ///     -   Target scales are updated to the new values
        /// </post-condition>
        set
        {
            if (value == null)
            {
                Debug.LogError("Setting of target scales array cannot be null");
                return;
            }
            Assert.IsTrue(value != null, "Target scales array cannot be null");
            targetScales = value;
        }
    }



    /// <summary>
    /// Boolean data to track whether the object is currently being hovered
    /// </summary>
    private bool isHovering = false;

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
        get
        {
            return isHovering;
        }
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
    public void Initialize(Transform[] linkedObjects, float hoverScaleMultiplier, float scaleSpeed)
    {
        //// Debug logs for LinkedObjects parameter
        if (linkedObjects == null || linkedObjects.Length == 0)
        {
            Debug.LogError("Linked objects array cannot be null or empty");
            return;
        }
        /// Assertions for linkedObjects parameter
        assert.IsNotNull(linkedObjects, "Linked objects array cannot be null");
        assert.IsTrue(linkedObjects.Length > 0, "Linked objects array must have at least one element");
        this.linkedObjects = linkedObjects;

        /// Debug log for hoverScaleMultiplier parameter
        if (hoverScaleMultiplier <= 0)
        {
            Debug.LogError("Hover scale multiplier must be non-negative");
            return;
        }
        /// Assertion for hoverScaleMultiplier parameter
        assert.IsTrue(hoverScaleMultiplier >= 0, "Hover scale multiplier must be non-negative");
        this.hoverScaleMultiplier = hoverScaleMultiplier;

        /// Debug log for scaleSpeed parameter
        if (scaleSpeed <= 0)        {
            Debug.LogError("Scale speed must be non-negative");
            return;
        }
        /// Assertion for scaleSpeed parameter
        assert.IsTrue(scaleSpeed >= 0, "Scale speed must be non-negative");
        this.scaleSpeed = scaleSpeed;

    
        /// Initialize scales based on the linked objects' original scales
        InitializeScales();
    }



    /// <summary>
    /// Initializes the normal and target scales for objects linked
    /// </summary>
    /// <pre-condition>
    ///      -   linkedObject != null && linkedObject > 0
    /// </pre-condition>
    /// <post-condition>
    ///     -   The normal scale and target scale for a linked object is initialized
    /// </post-condition>
    private void InitializeScales()
    {
        /// Debug log for linkedObjects array
        if (linkedObjects == null || linkedObjects.Length == 0)
        {
            Debug.LogError("Cannot initialize scales: linked objects array is null or empty");
            return;
        }
        /// Assertions for linkedObjects array
        Assert.IsNotNull(linkedObjects, "Linked objects array cannot be null");
        Assert.IsTrue(linkedObjects.Length > 0, "Linked objects array must have at least one element"); 
        
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
    /// <pre-condition> 
    ///     -   linkedObjects must exist and not be null
    /// </pre-condition>
    /// <post-condition> 
    ///     -   Target scales are set to be bigger (1.25x)
    /// </post-condition>
    public void OnHoverEnter()
    {
        isHovering = true;
        
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            /// Debug log for linked object at index i
            if (linkedObjects[i] == null)
            {
                /// Stop processing if a linked object is null
                Debug.LogError($"Linked object at index {i} is null");
                return;
            }
            /// Assertion for linked object at index i
            assert.IsNotNull(linkedObjects[i], $"Linked object at index {i} cannot be null");

            targetScales[i] = normalScales[i] * hoverScaleMultiplier;
        }
    }



    /// <summary>
    /// Called when hover exits - sets target scales back to normal
    /// </summary>
    /// <pre-condition> 
    ///     -   linkedObjects must exist and not be null
    /// </pre-condition>
    /// <post-condition> 
    ///     -   Target scales goes back to normal scale
    /// </post-condition>
    public void OnHoverExit()
    {
        isHovering = false;
        
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            /// Debug log for linked object at index i
            if (linkedObjects[i] == null)
            {
                /// Stop processing if a linked object is null
                Debug.LogError($"Linked object at index {i} is null");
                return;
            }
            /// Assertion for linked object at index i
            assert.IsNotNull(linkedObjects[i], $"Linked object at index {i} cannot be null");

            targetScales[i] = normalScales[i];
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
