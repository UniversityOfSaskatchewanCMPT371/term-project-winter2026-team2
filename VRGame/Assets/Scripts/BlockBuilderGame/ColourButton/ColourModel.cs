using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model component of ColourModel.
/// </summary>
public class ColourModel : Model, IColourModel
{
    /// <summary>
    /// Array of colours to cycle through on colour change button press
    /// </summary>
    [SerializeField] private Material[] colours;

    /// <inheritdoc/>
    public Material[] Colours
    {
        get 
        { 
            return colours; 
        }
        set
        {
            Assert.IsNotNull(value, "value to set for colours must not be null");
            Assert.IsTrue(value.Length > 0, "colours array must have at least 1 element");
            colours = value;
        }
    }

    /// <summary>
    /// Current index in the colours cycle.
    /// </summary>
    private int currentIndex;

    /// <inheritdoc/>
    public int CurrentIndex
    {
        get 
        { 
            return currentIndex; 
        }
        set
        {
            Assert.IsTrue(value >= 0, "currentIndex for colours cannot be negative");
            currentIndex = value;
        }
    }

    /// <inheritdoc/>
    public override void Init()
    {
        currentIndex = 0;
        Assert.AreEqual(currentIndex, 0, "currentIndex for colours failed to set on Init");
    }
}
