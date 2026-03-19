using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Manages pipe textures and pathing for the logic path minigame
/// </summary>
public class PanelTextureManager : MonoBehaviour
{

    private const string TEXTURE_PATH = "Textures/LogicGame/";
    private Panel panel;

    public void Awake()
    {
        panel = gameObject.GetComponent<Panel>();
        Assert.IsNotNull(panel, "Panel cannot be null");
    }

    /// <summary>
    /// Gets the appropriate pipe texture for a given panel based on its entry and exit directions and pipe color
    /// </summary>
    /// <param name="panel">The panel containing the pipe</param>
    /// <remarks>
    /// <preconditions>
    ///     - panel must not be null
    /// </preconditions>
    /// <postconditions>
    ///     - Returns the correct pipe texture based on the panel's pipe color and directions
    ///     - Returns null if the texture fails to load
    /// </postconditions>
    public Texture2D GetPipeTexture()
    {

        //string texturePath = GetTexturePath(panel.PipeColor.Value, panel.EntryDirection, panel.ExitDirection);
        //Texture2D texture = Resources.Load<Texture2D>(texturePath);
    
        /*if (texture == null)
        {
            Debug.LogWarning("Failed to load pipe texture from path: " + texturePath);
        }

        return texture;*/
        return null;
    }

    /// <summary>
    /// Constructs the texture path for a pipe based on its color and entry/exit directions
    /// </summary>
    /// <param name="color">The color of the pipe</param>
    /// <param name="entryDirection">The entry direction</param>
    /// <param name="exitDirection">The exit direction</param>
    /// <remarks>
    /// <preconditions>
    ///     - color must be a valid pipe color
    ///     - entryDirection and exitDirection must be valid directions (not None)
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string representing the correct texture path
    ///     - Filename is based on the visual orientation of the pipe
    /// </postconditions>
    /*private string GetTexturePath(Color color, Direction entryDirection, Direction exitDirection)
    {
        string colorName = GetColorName(color);
        string visualDirection = GetVisualDirection(entryDirection, exitDirection);
        return TEXTURE_PATH + colorName + "_" + visualDirection;
    }*/

    /// <summary>
    /// Maps entry and exit directions to visual direction for texture lookup
    /// </summary>
    /// <param name="entryDirection">The entry direction</param>
    /// <param name="exitDirection">The exit direction</param>
    /// <remarks>
    /// <preconditions>
    ///     - entryDirection and exitDirection must be valid directions (not None)
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string representing the visual orientation of the pipe for texture naming
    ///     - Examples: "up_down", "left_right", "left_down", etc.
    private string GetVisualDirection(Direction entryDirection, Direction exitDirection)
    {
        Direction d1 = entryDirection;
        Direction d2 = exitDirection;

        if ((d1 == Direction.Up && d2 == Direction.Down) || (d1 == Direction.Down && d2 == Direction.Up))
        {
            return "up_down";
        }

        if ((d1 == Direction.Left && d2 == Direction.Right) || (d1 == Direction.Right && d2 == Direction.Left))
        {
            return "left_right";
        }

        if ((d1 == Direction.Left && d2 == Direction.Down) || (d1 == Direction.Down && d2 == Direction.Left))
        {
            return "left_down";
        }

        if ((d1 == Direction.Left && d2 == Direction.Up) || (d1 == Direction.Up && d2 == Direction.Left))
        {
            return "left_up";
        }

        if ((d1 == Direction.Right && d2 == Direction.Down) || (d1 == Direction.Down && d2 == Direction.Right))
        {
            return "right_down";
        }

        if ((d1 == Direction.Right && d2 == Direction.Up) || (d1 == Direction.Up && d2 == Direction.Right))
        {
            return "right_up";
        }

        Debug.LogWarning($"Unknown direction combination: {d1} to {d2}");
        return "down";
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