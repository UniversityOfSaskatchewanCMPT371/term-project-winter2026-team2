using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Manages pipe textures and pathing for the logic path minigame
/// </summary>
public class PipeManager : MonoBehaviour
{

    private const string TEXTURE_PATH = "Textures/LogicGame/";


    /// <summary>
    /// Gets the appropriate pipe texture for a given panel based on its entry and exit directions and pipe color
    /// </summary>
    /// <param name="panel">The panel containing the pipe</param>
    /// <remarks>
    /// <preconditions>
    ///     - panel must not be null
    ///     - panel must have valid entry and exit directions (not None)
    ///     - panel must have a pipe color assigned
    /// </preconditions>
    /// <postconditions>
    ///     - Returns the correct pipe texture based on the panel's pipe color and directions
    ///     - Returns null if the texture fails to load
    /// </postconditions>
    public Texture2D GetPipeTexture(Panel panel)
    {
        Assert.IsNotNull(panel, "Panel cannot be null");
        //Assert.IsTrue(panel.PipeColor.HasValue, "Panel must have a pipe colour");

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
    private string GetTexturePath(Color color, Direction entryDirection, Direction exitDirection)
    {
        string colorName = GetColorName(color);
        string visualDirection = GetVisualDirection(entryDirection, exitDirection);
        return TEXTURE_PATH + colorName + "_" + visualDirection;
    }

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
    private string GetColorName(Color color)
    {
        if (color == Color.red)
        {
            return "red";
        }
        else if (color == Color.green)
        {
            return "green";
        }
        else if (color == Color.blue)
        {
            return "blue";
        }
        else if (color == Color.yellow)
        {
            return "yellow";
        }
        else
        {
            Debug.LogWarning($"Unknown pipe color: {color}. Defaulting to white.");
            return "white";
        }
    }

    /// <summary>
    /// Converts a Direction enum value to its string name
    /// </summary>
    /// <param name="direction">The direction to convert</param>
    /// <remarks>
    /// <preconditions>
    ///     - direction must be a valid Direction enum value (Up, Down, Left, Right)
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string name for the direction used in texture naming
    /// </postconditions>
    private string GetDirectionName(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return "up";
            case Direction.Down:
                return "down";
            case Direction.Left:
                return "left";
            case Direction.Right:
                return "right";
            default:
                Debug.LogWarning($"Unknown direction: {direction}. Defaulting to 'none'.");
                return "none";
        }
    }
}