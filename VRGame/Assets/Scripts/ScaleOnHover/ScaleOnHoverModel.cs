using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model class holds the data and state for scale-on-hover functionality
/// </summary>
public class ScaleOnHoverModel : MonoBehaviour, IScaleOnHoverModel
{
    /// <summary>
    /// Array of linked objects that will be scaled when hovered
    /// </summary>
    [SerializeField] private Transform[] linkedObjects;

    /// <inheritdoc/>
    public Transform[] LinkedObjects
    {
        /// <inheritdoc/>
        get
        {
            return linkedObjects;
        }

        /// <inheritdoc/>
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

    /// <inheritdoc/>
    public float HoverScaleMultiplier
    {
        /// <inheritdoc/>
        get
        {
            return hoverScaleMultiplier;
        }

        /// <inheritdoc/>
        set
        {
            if (value < 0)
            {
                Debug.LogError("Hover scale multiplier must be zero or positive");
                return;
            }
            Assert.IsTrue(value <= 0, "Hover scale multiplier must be greater than zero");
            hoverScaleMultiplier = value;
        }
    }



    /// <summary>
    /// Float speed for how quickly the linked objects should transition to their target scale
    /// </summary>
    [SerializeField] private float scaleSpeed = 10f;

    /// <inheritdoc/>
    public float ScaleSpeed
    {
        /// <inheritdoc/>
        get
        {
            return scaleSpeed;
        }

        /// <inheritdoc/>
        set
        {
            if (value <= 0)
            {
                Debug.LogError("Scale speed must be greater than zero");
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

    /// <inheritdoc/>
    public Vector3[] NormalScales
    {
        /// <inheritdoc/>
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

    /// <inheritdoc/>
    public Vector3[] TargetScales
    {
        /// <inheritdoc/>
        get
        {
            return targetScales;
        }

        /// <inheritdoc/>
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

    /// <inheritdoc/>
    public bool IsHovering
    {
        /// <inheritdoc/>
        get
        {
            return isHovering;
        }
        /// Note: No setter for isHovering since it should only be changed 
        ///         through the OnHoverEnter and OnHoverExit methods
    }


    /// <inheritdoc/>
    public void Initialize(Transform[] linkedObjects, float hoverScaleMultiplier, float scaleSpeed)
    {
        // Debug logs for LinkedObjects parameter
        if (linkedObjects == null || linkedObjects.Length == 0)
        {
            Debug.LogError("Linked objects array cannot be null or empty");
            return;
        }
        // Assertions for linkedObjects parameter
        Assert.IsNotNull(linkedObjects, "Linked objects array cannot be null");
        Assert.IsTrue(linkedObjects.Length > 0, "Linked objects array must have at least one element");
        this.linkedObjects = linkedObjects;

        // Debug log for hoverScaleMultiplier parameter
        if (hoverScaleMultiplier <= 0)
        {
            Debug.LogError("Hover scale multiplier must be non-negative");
            return;
        }
        // Assertion for hoverScaleMultiplier parameter
        Assert.IsTrue(hoverScaleMultiplier >= 0, "Hover scale multiplier must be non-negative");
        this.hoverScaleMultiplier = hoverScaleMultiplier;

        // Debug log for scaleSpeed parameter
        if (scaleSpeed <= 0)
        {
            Debug.LogError("Scale speed must be greater than zero");
            return;
        }
        // Assertion for scaleSpeed parameter
        Assert.IsTrue(scaleSpeed > 0, "Scale speed must be greater than zero");
        this.scaleSpeed = scaleSpeed;


        // Initialize scales based on the linked objects' original scales
        InitializeScales();
    }



    /// <inheritdoc/>
    public void InitializeScales()
    {
        // Debug log for linkedObjects array
        if (linkedObjects == null || linkedObjects.Length == 0)
        {
            Debug.LogError("Cannot initialize scales: linked objects array is null or empty");
            return;
        }
        // Assertions for linkedObjects array
        Assert.IsNotNull(linkedObjects, "Linked objects array cannot be null");
        Assert.IsTrue(linkedObjects.Length > 0, "Linked objects array must have at least one element");

        // Initialize normal and target/bigger scale
        normalScales = new Vector3[linkedObjects.Length];
        targetScales = new Vector3[linkedObjects.Length];

        // Initialize scales 
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            if (linkedObjects[i] == null)
            {
                Debug.LogError($"Linked object at index {i} is null");
                return;
            }

            // Assertion for linked object at index i
            Assert.IsNotNull(linkedObjects[i], $"Linked object at index {i} cannot be null");

            // All checks passed, initialize normal and target scales for linked object at index i
            normalScales[i] = linkedObjects[i].localScale;
            targetScales[i] = normalScales[i];
        }
    }



    /// <inheritdoc/>
    public void OnHoverEnter()
    {
        isHovering = true;

        for (int i = 0; i < linkedObjects.Length; i++)
        {
            // Debug log for linked object at index i
            if (linkedObjects[i] == null)
            {
                // Stop processing if a linked object is null
                Debug.LogError($"Linked object at index {i} is null");
                return;
            }
            // Assertion for linked object at index i
            Assert.IsNotNull(linkedObjects[i], $"Linked object at index {i} cannot be null");

            // All checks passed, set target scale to be bigger
            targetScales[i] = normalScales[i] * hoverScaleMultiplier;
        }
    }



    /// <inheritdoc/>
    public void OnHoverExit()
    {
        isHovering = false;

        for (int i = 0; i < linkedObjects.Length; i++)
        {
            // Debug log for linked object at index i
            if (linkedObjects[i] == null)
            {
                // Stop processing if a linked object is null
                Debug.LogError($"Linked object at index {i} is null");
                return;
            }
            // Assertion for linked object at index i
            Assert.IsNotNull(linkedObjects[i], $"Linked object at index {i} cannot be null");

            // All checks passed, set target scale back to normal
            targetScales[i] = normalScales[i];
        }
    }

    /// <inheritdoc/>
    public void Awake()
    {
        // Initialize scales on linkedObjects
        Assert.IsTrue(linkedObjects != null, "Linked objects shouldn't be null on Awake()");
        if (linkedObjects != null && linkedObjects.Length > 0)
        {
            InitializeScales();

        }
    }

}