using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model component of ColourModel.
/// </summary>
public class ColourModel : Model, IColourModel
{
    /// <summary>
    /// Array of materials to cycle through on colour change button press
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
            Assert.IsNotNull(value, "value to set for materials must not be null");
            Assert.IsTrue(value.Length > 0, "materials array must have at least 1 element");
            colours = value;
        }
    }

    /// <summary>
    /// Current index in the materials cycle.
    /// </summary>
    private int currentMaterialIndex;

    /// <inheritdoc/>
    public int CurrentMaterialIndex
    {
        get 
        { 
            return currentMaterialIndex; 
        }
        set
        {
            Assert.IsTrue(value >= 0, "currentMaterialIndex cannot be negative");
            currentMaterialIndex = value;
        }
    }

    /// <inheritdoc/>
    public override void Init()
    {
        currentMaterialIndex = 0;
        Assert.AreEqual(currentMaterialIndex, 0, "currentMaterialIndex failed to set on Init");
    }
}
