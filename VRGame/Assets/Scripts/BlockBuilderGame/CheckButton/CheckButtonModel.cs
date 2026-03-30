using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model component of CheckButton.
/// Sets up an area container for checking player's built blocks.
/// </summary>
public class CheckButtonModel : Model, ICheckButtonModel
{
    /// <summary>
    /// The collider container that defines the area in which built blocks are checked
    [SerializeField] private Collider checkArea;
    
    /// <inheritdoc/>
    public Collider CheckArea
    {
        get
        {
            return checkArea;
        }
        set
        {
            Assert.IsNotNull(value, "CheckArea must not be null");
            checkArea = value;
        }
    }


    /// <inheritdoc/>
    public override void Init()
    {
        Assert.IsNotNull(checkArea, "CheckArea must be assigned in the Inspector");
    }
}
