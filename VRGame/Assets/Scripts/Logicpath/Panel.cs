using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents a single panel's attribute in the logic path minigame
/// </summary>
public enum PanelAttribute
{
    Normal,
    Start,
    Exit,
    Block
}

/// <summary>
/// Represents a single panel in the logic path minigame - is essentially the "view" of the game
/// </summary>
public class Panel : MonoBehaviour, IEquatable<Panel>
{
    /// <summary>
    /// The entry direction of this Panel, if any
    /// </summary>
    private Direction entryDirection;
    /// <summary>
    /// The exit direction of this Panel, if any
    /// </summary>
    private Direction exitDirection;
    /// <summary>
    /// The upper neighbor of this Panel, if there is one
    /// </summary>
    private Panel topNeighbor;
    /// <summary>
    /// The right neighbor of this Panel, if there is one
    /// </summary>
    private Panel rightNeighbor;
    /// <summary>
    /// The lower neighbor of this Panel, if there is one
    /// </summary>
    private Panel downNeighbor;
    /// <summary>
    /// The left neighbor of this Panel, if there is one
    /// </summary>
    private Panel leftNeighbor;
    /// <summary>
    /// Marks whether this Panel is a Block, start endpoint, end endpoint, or a regular Panel
    /// </summary>

    [SerializeField]
    private PanelAttribute attribute;
    /// <summary>
    /// What colour this Panel has (if there is a path going through it or it is an endpoint)
    /// </summary>
    [SerializeField]
    private PanelColour panelColour;
    /// <summary>
    /// The X-coordinate of this Panel
    /// </summary>
    [SerializeField]
    private int gridX;
    /// <summary>
    /// The Y-coordinate of this Panel
    /// </summary>
    [SerializeField]
    private int gridY;
    /// <summary>
    /// The texture manager for this Panel
    /// </summary>
    private PanelTextureManager panelTextureManager;
    /// <summary>
    /// The Interactable used for hover events
    /// </summary>
    private XRSimpleInteractable xRSimpleInteractable;
    /// <summary>
    /// The parenting game controller
    /// </summary>
    private LogicGameController logicGameController;

    /// <summary>
    /// Getter for entry direction
    /// </summary>
    /// <returns>The entry direction of this Panel</returns>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The entry direction is returned
    /// </remarks>
    public Direction GetEntryDirection()
    {
            return entryDirection;
    }

    /// <summary>
    /// Setter for entry direction
    /// </summary>
    /// <param name="entryDirection">The new entry direction</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - This panel's entryDirection is changed
    ///     - This panel's texture updates to match the new direction
    /// </remarks>
    public void SetEntryDirection(Direction entryDirection)
    {
        this.entryDirection = entryDirection;
        Debug.Log("Entry direction is being changed");
        panelTextureManager.RefreshTexture();
    }

    /// <summary>
    /// Getter for exit direction
    /// </summary>
    /// <returns>The exit direction of this Panel</returns>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The exit direction is returned
    /// </remarks>
    public Direction GetExitDirection()
    {
        return exitDirection;
    }

    /// <summary>
    /// Setter for exit direction
    /// </summary>
    /// <param name="exitDirection">The new exit direction</param>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - This panel's exitDirection is changed
    ///     - This panel's texture updates to match the new direction
    /// </remarks>

    public void SetExitDirection(Direction exitDirection)
    {
        this.exitDirection = exitDirection;
        Debug.Log("Exit direction is being changed");
        panelTextureManager.RefreshTexture();
    }

    /// <summary>
    /// Accessor for top neighbour panel
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the top neighbor of this Panel is returned if it exists, null otherwise
    ///     - If setting, the top neighbor of this Panel is written
    /// </remarks>
    public Panel TopNeighbor
    {
        get
        {
            return topNeighbor;
        }

        set
        {
            topNeighbor = value;
        }
    }

    /// <summary>
    /// Accessor for right neighbour panel
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the right neighbor of this Panel is returned if it exists, null otherwise
    ///     - If setting, the right neighbor of this Panel is written
    /// </remarks>
    public Panel RightNeighbor
    {
        get
        {
            return rightNeighbor;
        }

        set
        {
            rightNeighbor = value;
        }
    }

    /// <summary>
    /// Accessor for down neighbour panel
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the lower neighbor of this Panel is returned if it exists, null otherwise
    ///     - If setting, the lower neighbor of this Panel is written
    /// </remarks>
    public Panel DownNeighbor
    {
        get
        {
            return downNeighbor;
        }

        set 
        {
            downNeighbor = value;
        }
    }

    /// <summary>
    /// Accessor for left neighbour panel
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the left neighbor of this Panel is returned if it exists, null otherwise
    ///     - If setting, the left neighbor of this Panel is written
    /// </remarks>
    public Panel LeftNeighbor
    {
        get
        {
            return leftNeighbor;
        }

        set
        {
            leftNeighbor = value;
        }
    }

    /// <summary>
    /// Accessor for panel attribute (normal, start, exit, block)
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the attribute of this Panel is returned
    ///     - If setting, the attribute of this Panel is overwritten, and the texture is refreshed to reflect the state
    /// </remarks>
    public PanelAttribute Attribute
    {
        get
        {
            return attribute;
        }

        set
        {
            attribute = value;
            panelTextureManager.RefreshTexture();
        }
    }

    /// <summary>
    /// Accessor for panel colour
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the colour of this Panel is returned
    ///     - If setting, the colour of this Panel is overwritten, and the texture is refreshed to reflect the state
    /// </remarks>
    public PanelColour PanelColour
    {
        get
        {
            return panelColour;
        }
        set
        {
            panelColour = value;
            panelTextureManager.RefreshTexture();
        }
    }

    /// <summary>
    /// Accessor for grid X coordinate
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the X-coordinate of this Panel is returned
    ///     - If setting, the X-coordinate of this Panel is overwritten
    /// </remarks>
    public int GridX
    {
        get
        {
            return gridX;
        }

        set
        {
            gridX = value;
        }
    }

    /// <summary>
    /// Accessor for grid Y coordinate
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - If getting, the Y-coordinate of this Panel is returned
    ///     - If setting, the Y-coordinate of this Panel is overwritten
    /// </remarks>
    public int GridY
    {
        get
        {
            return gridY;
        }

        set
        {
            gridY = value;
        }
    }

    /// <summary>
    /// Checks if the panel is occupied, whether by a line or by its own block
    /// </summary>
    /// <returns>true if the panel is occupied, false otherwise</returns>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The truth value of whether or not this panel is occupied is returned
    /// </remarks>
    public bool IsOccupied()
    {
        return entryDirection != Direction.None || exitDirection != Direction.None || attribute == PanelAttribute.Block;
    }

    /// <summary>
    /// Clears any line status from this panel, resetting entry and exit directions
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - Resets entry and exit directions to None
    ///     - Texture is refreshed
    /// </remarks>
    public void ClearPanel()
    {
        entryDirection = Direction.None;
        exitDirection = Direction.None;
        panelTextureManager.RefreshTexture();
    }

    /// <summary>
    /// Unity Awake() method, initializes the panel with grid coordinates, texture manager, XR interactable, and parenting game controller
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - There is a PanelTextureManager and XRSimpleInteractable attached to this GameObject
    ///     - There is a parenting GameObject with a LogicGameController attached to it
    /// postconditions:
    ///     - all variables (besides *Neighbors) are initialized
    ///     - Texture is refreshed to reflect state
    ///     - Hover event listener functions are mapped
    /// </remarks>
    public void Awake()
    {
        Assert.IsTrue(this.gridX >= 0, "Grid X coordinate cannot be negative");
        Assert.IsTrue(this.gridY >= 0, "Grid Y coordinate cannot be negative");
    
        entryDirection = Direction.None;
        exitDirection = Direction.None;
        topNeighbor = null;
        rightNeighbor = null;
        downNeighbor = null;
        leftNeighbor = null;

        panelTextureManager = GetComponent<PanelTextureManager>();
        Assert.IsNotNull(panelTextureManager, "Could not find texture manager!");
        panelTextureManager.RefreshTexture();

        xRSimpleInteractable = GetComponent<XRSimpleInteractable>();
        Assert.IsNotNull(xRSimpleInteractable, "Could not find the XR interactable!");
        xRSimpleInteractable.hoverEntered.AddListener(OnHoverEntered);
        xRSimpleInteractable.hoverExited.AddListener(OnHoverExited);

        Assert.IsNotNull(transform.parent, "There is no parent object for this panel!");
        logicGameController = transform.parent.gameObject.GetComponent<LogicGameController>();
        Assert.IsNotNull(logicGameController, "Could not find LogicGameController!");
    }

    /// <summary>
    /// Unity OnDestroy() method, tears down hover event listeners
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - Hover event listener functions are unmapped
    /// </remarks>
    public void OnDestroy()
    {
        xRSimpleInteractable.hoverEntered.RemoveListener(OnHoverEntered);
        xRSimpleInteractable.hoverExited.AddListener(OnHoverExited);
    }

    /// <summary>
    /// Event listener function for hover start events
    /// </summary>
    /// <param name="args">Arguments for this event</param>
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        logicGameController.HandleHover(gridX, gridY);
    }

    /// <summary>
    /// Event listener function for hover end events
    /// </summary>
    /// <param name="args">Arguments for this event</param>
    private void OnHoverExited(HoverExitEventArgs args)
    {
        logicGameController.HandleUnhover(gridX, gridY);
    }

    /// <summary>
    /// Creates a string representation of this Panel
    /// </summary>
    /// <returns>A string representation of this Panel</returns>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - A string representation of this Panel is returned
    /// </remarks>
    public override string ToString()
    {
        return $"({gridX},{gridY}), {panelColour}, {Attribute}, Entry {entryDirection}, Exit {exitDirection}";
    }

    /// <summary>
    /// Checks if this Panel is functionally equivalent to another Panel
    /// </summary>
    /// <param name="other">Another Panel to compare with</param>
    /// <returns>true if the Panels are equivalent, false otherwise</returns>
    /// <remarks>
    /// preconditions:
    ///     - other != null
    /// postconditions:
    ///     - The truth value of whether the two Panels are equivalent is returned
    /// </remarks>
    public bool Equals(Panel other)
    {
        return this.gridX == other.gridX && this.gridY == other.GridY && this.Attribute == other.Attribute && this.entryDirection == other.entryDirection && this.exitDirection == other.exitDirection && this.panelColour == other.panelColour;
    }

}
