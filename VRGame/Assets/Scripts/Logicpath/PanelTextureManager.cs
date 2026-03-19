using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Manages panel textures and pathing for the logic path minigame
/// </summary>
public class PanelTextureManager : MonoBehaviour
{

    private const string TEXTURE_PATH = "Materials/LogicGame/";
    private Panel panel;

    public void Awake()
    {
        panel = gameObject.GetComponent<Panel>();
        Assert.IsNotNull(panel, "Panel cannot be null");
    }

    public void RefreshTexture()
    {
        
    }

    /// <summary>
    /// Constructs the texture path for a pipe based on its color and entry/exit directions
    /// </summary>
    /// <remarks>
    /// <preconditions>
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string representing the correct texture path
    /// </postconditions>
    private string GetTexturePath()
    {
        if(panel.Attribute == PanelAttribute.Block)
        {
            return TEXTURE_PATH + "block";
        }
        if(panel.EntryDirection == Direction.None && panel.ExitDirection == Direction.None)
        {
            return TEXTURE_PATH + "blank";
        }
        string colourName = GetColorName(panel.PanelColour);

        //return TEXTURE_PATH + colorName + "_" + visualDirection;
        return "";
    }

    /// <summary>
    /// Maps entry and exit directions to visual direction for texture lookup
    /// </summary>
    /// <remarks>
    /// <preconditions>
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string representing the visual orientation of the pipe for texture naming
    ///     - Examples: "up_down", "left_right", "left_down", etc.
    
    private string GetDirectionName()
    {
        return "";
    }

    /// <summary>
    /// Converts a Color to a string name for texture lookup
    /// </summary>
    /// <param name="color">The color to convert</param>
    /// <remarks>
    /// <preconditions>
    ///     - color must be a valid pipe color (red, green, blue, yellow)
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string name for the color used in texture naming
    ///     - Defaults to "white" if the color is unknown
    /// </postconditions>
    private string GetColorName(PanelColour colour)
    {
        switch(colour)
        {
            case PanelColour.Red:
                return "red";
            case PanelColour.Green:
                return "green";
            case PanelColour.Blue:
                return "blue";
            case PanelColour.Yellow:
                return "yellow";
            default:
                Debug.LogWarning($"Unknown pipe color: {colour}. Defaulting to red.");
                return "red";
        }
    }
}