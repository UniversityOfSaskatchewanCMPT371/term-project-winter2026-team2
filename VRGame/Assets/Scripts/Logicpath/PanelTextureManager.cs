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
    ///     - The panel's state must be valid (ex: no entry direction for a start panel)
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
        string colourName = GetColourName(panel.PanelColour);
        string maybeEndpoint;
        switch (panel.Attribute)
        {
            case PanelAttribute.Normal:
                maybeEndpoint = "";
                break;
            case PanelAttribute.Start:
                Assert.AreEqual(panel.EntryDirection, Direction.None, "Start endpoint's entry direction must be None");
                maybeEndpoint = "_start";
                break;
            case PanelAttribute.Exit:
                Assert.AreEqual(panel.ExitDirection, Direction.None, "End endpoint's exit direction must be None");
                maybeEndpoint = "_end";
                break;
            case PanelAttribute.Block:
                throw new AssertionException("Block textures cannot be coloured! Is the control flow wrong?","Block textures cannot be coloured! Is the control flow wrong?");
            default:
                throw new AssertionException($"Unknown PanelAttribute \"{panel.Attribute}\"",$"Unknown PanelAttribute \"{panel.Attribute}\"");
        }
        string directionName = GetDirectionName(panel.EntryDirection, panel.ExitDirection);
        return $"{colourName}{maybeEndpoint}_{directionName}";
    }

    /// <summary>
    /// Maps entry and exit directions of the panel's line to visual direction for texture lookup
    /// </summary>
    /// <param name="entry">The entry direction</param>
    /// <param name="exit">The exit direction</param>
    /// <remarks>
    /// <preconditions>
    ///     - entry != exit
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string representing the visual orientation of the pipe for texture naming
    ///     - Examples: "up_down", "left_right", "left_down", etc.
    
    private string GetDirectionName(Direction entry, Direction exit)
    {
        Assert.AreNotEqual(entry, exit, "Entry and exit directions cannot be equal");
        switch(entry)
        {
            case Direction.Left:
                switch(exit)
                {
                    case Direction.Right:
                        return "left_right";
                    case Direction.Up:
                        return "left_up";
                    case Direction.Down:
                        return "left_down";
                    case Direction.None:
                        return "left";
                    default:
                        throw new AssertionException($"Unknown exit direction \"{exit}\"",$"Unknown entry direction \"{exit}\"");
                }
            case Direction.Right:
                switch(exit)
                {
                    case Direction.Left:
                        return "left_right";
                    case Direction.Up:
                        return "right_up";
                    case Direction.Down:
                        return "right_down";
                    case Direction.None:
                        return "right";
                    default:
                        throw new AssertionException($"Unknown exit direction \"{exit}\"",$"Unknown entry direction \"{exit}\"");
                }
            case Direction.Up:
                switch(exit)
                {
                    case Direction.Left:
                        return "left_up";
                    case Direction.Right:
                        return "right_up";
                    case Direction.Down:
                        return "up_down";
                    case Direction.None:
                        return "up";
                    default:
                        throw new AssertionException($"Unknown exit direction \"{exit}\"",$"Unknown entry direction \"{exit}\"");
                }
            case Direction.Down:
                switch(exit)
                {
                    case Direction.Left:
                        return "left_down";
                    case Direction.Right:
                        return "right_down";
                    case Direction.Up:
                        return "up_down";
                    case Direction.None:
                        return "down";
                    default:
                        throw new AssertionException($"Unknown exit direction \"{exit}\"",$"Unknown entry direction \"{exit}\"");
                }
            case Direction.None:
                switch(exit)
                {
                    case Direction.Left:
                        return "left";
                    case Direction.Right:
                        return "right";
                    case Direction.Up:
                        return "up";
                    case Direction.Down:
                        return "down";
                    default:
                        throw new AssertionException($"Unknown exit direction \"{exit}\"",$"Unknown entry direction \"{exit}\"");
                }
            default:
                throw new AssertionException($"Unknown entry direction \"{entry}\"",$"Unknown entry direction \"{entry}\"");
        }
    }

    /// <summary>
    /// Converts a PanelColour to a string name for texture lookup
    /// </summary>
    /// <param name="colour">The Panelcolour to convert</param>
    /// <remarks>
    /// <preconditions>
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string name for the color used in texture naming
    ///     - Defaults to "red" if the color is unknown
    /// </postconditions>
    private string GetColourName(PanelColour colour)
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