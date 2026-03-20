using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Manages panel textures and pathing for the logic path minigame
/// </summary>
public class PanelTextureManager : MonoBehaviour
{

    private const string TEXTURE_PATH = "Materials/LogicGame/";
    private Panel panel;

    /// <summary>
    /// Unity's Awake() method for the texture manager
    /// </summary>
    public void Awake()
    {
        panel = gameObject.GetComponent<Panel>();
        Assert.IsNotNull(panel, "Panel cannot be null");
    }
    
    /// <summary>
    /// Refresh the texture for this panel
    /// </summary>
    /// <preconditions>
    ///     - The panel's state must be valid so that its state can point to a valid texture
    /// </preconditions>
    /// <postconditions>
    ///     - The panel's texture is updated accordingly
    /// </postconditions>
    public void RefreshTexture()
    {
        string textureName = GetTexturePath();
        Material newTexture = Resources.Load(textureName, typeof(Material)) as Material;
        Assert.IsNotNull(newTexture, $"\"{textureName}\" does not point to a valid material!");
        Renderer renderer = GetComponent<Renderer>();
        Assert.IsNotNull(renderer, "Could not find the Renderer for this panel! Something has gone horribly, terribly wrong");
        renderer.material = newTexture;
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
        if(panel.Attribute == PanelAttribute.Normal && panel.GetEntryDirection() == Direction.None && panel.GetExitDirection() == Direction.None)
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
                Assert.AreEqual(panel.GetEntryDirection(), Direction.None, "Start endpoint's entry direction must be None");
                maybeEndpoint = "_start";
                break;
            case PanelAttribute.Exit:
                Assert.AreEqual(panel.GetExitDirection(), Direction.None, "End endpoint's exit direction must be None");
                maybeEndpoint = "_end";
                break;
            case PanelAttribute.Block:
                throw new AssertionException("Block textures cannot be coloured! Is the control flow wrong?","Block textures cannot be coloured! Is the control flow wrong?");
            default:
                throw new AssertionException($"Unknown PanelAttribute \"{panel.Attribute}\"",$"Unknown PanelAttribute \"{panel.Attribute}\"");
        }
        string directionName = GetDirectionName(panel.GetEntryDirection(), panel.GetExitDirection());
        return $"{TEXTURE_PATH}{colourName}{maybeEndpoint}_{directionName}";
    }

    /// <summary>
    /// Maps entry and exit directions of the panel's line to visual direction for texture lookup
    /// </summary>
    /// <param name="entry">The entry direction</param>
    /// <param name="exit">The exit direction</param>
    /// <remarks>
    /// <preconditions>
    ///     - entry != exit && entry != Direction.None && exit != Direction.None
    /// </preconditions>
    /// <postconditions>
    ///     - Returns a string representing the visual orientation of the pipe for texture naming
    ///     - Examples: "up_down", "left_right", "left_down", etc.
    
    private string GetDirectionName(Direction entry, Direction exit)
    {
        if(entry == exit && entry != Direction.None && exit != Direction.None)
        {
            throw new AssertionException("Panel's entry and exit cannot point in the same non-none direction","Panel's entry and exit cannot point in the same non-none direction");
        }
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
                    case Direction.None:
                        // note: this should only display if and only if the panel is an endpoint
                        return "blank";
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