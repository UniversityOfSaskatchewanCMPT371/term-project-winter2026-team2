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
    /// Accessor for entry direction
    /// </summary>
    public Direction GetEntryDirection()
    {
            return entryDirection;
    }

    /// <summary>
    /// Setter for entry direction
    /// </summary>
    /// <param name="entryDirection">The new entry direction</param>
    /// <postconditions>
    ///     - This panel's entryDirection is changed
    ///     - This panel's texture updates to match the new direction
    /// </postconditions>
    public void SetEntryDirection(Direction entryDirection)
    {
        this.entryDirection = entryDirection;
        Debug.Log("Entry direction is being changed");
        panelTextureManager.RefreshTexture();
    }

    /// <summary>
    /// Accessor for exit direction
    /// </summary>
    public Direction GetExitDirection()
    {
        return exitDirection;
    }

    /// <summary>
    /// Setter for exit direction
    /// </summary>
    /// <param name="exitDirection">The new exit direction</param>
    /// <postconditions>
    ///     - This panel's exitDirection is changed
    ///     - This panel's texture updates to match the new direction
    /// </postconditions>

    public void SetExitDirection(Direction exitDirection)
    {
        this.exitDirection = exitDirection;
        Debug.Log("Exit direction is being changed");
        panelTextureManager.RefreshTexture();
    }

    /// <summary>
    /// Accessor for top neighbour panel
    /// </summary>
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
    /// <remarks>
    /// <preconditions>
    /// </preconditions>
    /// <postconditions>
    ///     - Returns true if the panel is occupied, false otherwise
    /// </postconditions>
    /// </remarks>
    public bool IsOccupied()
    {
        return entryDirection != Direction.None || exitDirection != Direction.None || attribute == PanelAttribute.Block;
    }

    /// <summary>
    /// Clears any line status from this panel, resetting entry and exit directions
    /// </summary>
    /// <remarks>
    /// <preconditions>
    ///     - None
    /// </preconditions>
    /// <postconditions>
    ///     - Resets entry and exit directions to None
    ///     - Texture is refreshed
    /// </postconditions>
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
    /// <preconditions>
    ///     - There is a PanelTextureManager and XRSimpleInteractable attached to this GameObject
    ///     - There is a parenting GameObject with a LogicGameController attached to it
    /// </preconditions>
    /// <postconditions>
    /// </postconditions>
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
    public override string ToString()
    {
        return $"({gridX},{gridY}), {panelColour}, {Attribute}, Entry {entryDirection}, Exit {exitDirection}";
    }

    /// <summary>
    /// Checks if this Panel is functionally equivalent to another Panel
    /// </summary>
    /// <param name="other">Another Panel to compare with</param>
    /// <returns>true if the Panels are equivalent, false otherwise</returns>
    /// <preconditions>
    ///     - other != null
    /// </preconditions>
    public bool Equals(Panel other)
    {
        return this.gridX == other.gridX && this.gridY == other.GridY && this.Attribute == other.Attribute && this.entryDirection == other.entryDirection && this.exitDirection == other.exitDirection && this.panelColour == other.panelColour;
    }

}
