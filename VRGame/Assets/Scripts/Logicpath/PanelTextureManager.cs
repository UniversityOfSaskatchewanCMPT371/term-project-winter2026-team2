using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Manages panel textures and pathing for the logic path minigame
/// </summary>
public class PanelTextureManager : MonoBehaviour
{
    /// <summary>
    /// The relative path to the materials needed (starting from the Resources folder)
    /// </summary>
    private static string TEXTURE_PATH = "Materials/LogicGame/";
    /// <summary>
    /// The Panel this manager is associated with
    /// </summary>
    private Panel panel;


    /// <summary>
    /// Unity's Awake() method for the texture manager, saves the Panel this manager is associated with
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - There is a Panel attached to the same GameObject
    /// postconditions:
    ///     - The manager has its associated Panel saved
    /// </remarks>
    public void Init()
    {
        panel = gameObject.GetComponent<Panel>();
        if(panel == null)
        {
            Debug.LogError("There is no panel attached to this GameObject!");
        }
        Assert.IsNotNull(panel, "There is no panel attached to this GameObject!");
    }

    /// <summary>
    /// Unity's Awake() method for the texture manager, saves the Panel this manager is associated with
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - There is a Panel attached to the same GameObject
    /// postconditions:
    ///     - The manager has its associated Panel saved
    /// </remarks>
    public void Awake()
    {
        Init();
    }
    
    /// <summary>
    /// Refresh the texture for this panel
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - The panel's state must be valid so that its state can point to a valid texture
    /// postconditions:
    ///     - The panel's texture is updated accordingly
    /// </remarks>
    public void RefreshTexture()
    {
        string textureName = GetTexturePath();
        Material newTexture = Resources.Load(textureName, typeof(Material)) as Material;
        if(newTexture == null)
        {
            Debug.LogError($"\"{textureName}\" does not point to a valid material!");
        }
        Assert.IsNotNull(newTexture, $"\"{textureName}\" does not point to a valid material!");
        Renderer renderer = GetComponent<Renderer>();
        if(renderer == null)
        {
            Debug.LogError("Could not find the Renderer for this Panel! Something has gone horribly, terribly wrong");
        }
        Assert.IsNotNull(renderer, "Could not find the Renderer for this Panel! Something has gone horribly, terribly wrong");
        renderer.material = newTexture;
    }

    /// <summary>
    /// Constructs the texture path for a Panel based on its state
    /// </summary>
    /// <returns>A string representing the texture path</returns>
    /// <remarks>
    /// preconditions:
    ///     - The panel's state must be valid (ex: no entry direction for a start panel)
    /// postconditions:
    ///     - A string representing the texture path is returned
    /// </remarks>
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
                if(panel.GetEntryDirection() != Direction.None)
                {
                    Debug.LogError("Start endpoint's entry direction must be None");
                }
                Assert.AreEqual(panel.GetEntryDirection(), Direction.None, "Start endpoint's entry direction must be None");
                maybeEndpoint = "_start";
                break;
            case PanelAttribute.Exit:
                if(panel.GetExitDirection() != Direction.None)
                {
                    Debug.LogError("End endpoint's exit direction must be None");
                }
                Assert.AreEqual(panel.GetExitDirection(), Direction.None, "End endpoint's exit direction must be None");
                maybeEndpoint = "_end";
                break;
            case PanelAttribute.Block:
                Debug.LogError("Block textures cannot be coloured! Is the control flow wrong?");
                throw new AssertionException("Block textures cannot be coloured! Is the control flow wrong?","Block textures cannot be coloured! Is the control flow wrong?");
            default:
                Debug.LogError($"Unknown PanelAttribute \"{panel.Attribute}\"");
                throw new AssertionException($"Unknown PanelAttribute \"{panel.Attribute}\"",$"Unknown PanelAttribute \"{panel.Attribute}\"");
        }
        string directionName = GetDirectionName(panel.GetEntryDirection(), panel.GetExitDirection());
        return $"{TEXTURE_PATH}{colourName}{maybeEndpoint}_{directionName}";
    }

    /// <summary>
    /// Maps entry and exit directions of the panel's path direction to visual direction for texture lookup
    /// </summary>
    /// <param name="entry">The entry direction</param>
    /// <param name="exit">The exit direction</param>
    /// <returns>A string representation of the entry and exit directions provided</returns>
    /// <remarks>
    /// preconditions:
    ///     - entry != exit && entry != Direction.None && exit != Direction.None
    /// postconditions:
    ///     - Returns a string representing the visual orientation of the pipe for texture naming (ex: "up_down", "left_right", "left_down", etc.)
    /// </remarks>
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
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - Returns a string name for the color used in texture naming
    ///     - Defaults to "red" if the color is unknown
    /// </remarks>
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