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
    /// Accessor for top neighbour Panel
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
        /// <summary>
        /// Getter for the top neighbor Panel
        /// </summary>
        /// <returns>A reference to the top neighbor Panel if it exists, null otherwise</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - either null or the top neighbor Panel is returned
        /// </remarks>
        get
        {
            return topNeighbor;
        }

        /// <summary>
        /// Setter for the top neighbor Panel
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - the reference to the top neighbor Panel is updated
        /// </remarks>
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
        /// <summary>
        /// Getter for the right neighbor Panel
        /// </summary>
        /// <returns>A reference to the right neighbor Panel if it exists, null otherwise</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - either null or the right neighbor Panel is returned
        /// </remarks>
        get
        {
            return rightNeighbor;
        }

        /// <summary>
        /// Setter for the right neighbor Panel
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - the reference to the right neighbor Panel is updated
        /// </remarks>
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
        /// <summary>
        /// Getter for the down neighbor Panel
        /// </summary>
        /// <returns>A reference to the down neighbor Panel if it exists, null otherwise</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - either null or the down neighbor Panel is returned
        /// </remarks>
        get
        {
            return downNeighbor;
        }

        /// <summary>
        /// Setter for the down neighbor Panel
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - the reference to the down neighbor Panel is updated
        /// </remarks>
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
        /// <summary>
        /// Getter for the left neighbor Panel
        /// </summary>
        /// <returns>A reference to the left neighbor Panel if it exists, null otherwise</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - either null or the left neighbor Panel is returned
        /// </remarks>
        get
        {
            return leftNeighbor;
        }

        /// <summary>
        /// Setter for the left neighbor Panel
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - the reference to the left neighbor Panel is updated
        /// </remarks>
        set
        {
            leftNeighbor = value;
        }
    }

    /// <summary>
    /// Accessor for Panel attribute (normal, start, exit, block)
    /// </summary>
    /// </remarks>
    public PanelAttribute Attribute
    {
        /// <summary>
        /// Getter for this Panel's attribute
        /// </summary>
        /// <returns>This Panel's attribute</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - this Panel's attribute is returned
        /// </remarks>
        get
        {
            return attribute;
        }

        /// <summary>
        /// Setter for this Panel's attribute
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - this Panel's attribute is overwritten with the new value
        ///     - this Panel's texture is updated to match the change
        /// </remarks>
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
        /// <summary>
        /// Getter for this Panel's colour
        /// </summary>
        /// <returns>This Panel's colour</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - this Panel's colour is returned
        /// </remarks>
        get
        {
            return panelColour;
        }

        /// <summary>
        /// Setter for this Panel's colour
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - this Panel's colour is overwritten with the new value
        ///     - this Panel's texture is updated to match the change
        /// </remarks>
        set
        {
            panelColour = value;
            panelTextureManager.RefreshTexture();
        }
    }

    /// <summary>
    /// Accessor for this Panel's X-coordinate
    /// </summary>
    public int GridX
    {
        /// <summary>
        /// Getter for the X-coordinate
        /// </summary>
        /// <returns>This Panel's X-coordinate</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - The X-coordinate is returned
        /// </remarks>
        get
        {
            return gridX;
        }

        /// <summary>
        /// Setter for the X-coordinate
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - when writing, X must be non-negative and less than the LogicGameModel's grid size
        /// postconditions:
        ///     - the X-coordinate is updated to the new value
        /// </remarks>
        set
        {
            if(value < 0)
            {
                Debug.LogError("Panel cannot have a negative X-coordinate");
            }
            Assert.IsTrue(value >= 0, "X coordinate must be greater than 0");
            if(value >= LogicGameModel.MAX_GRID_SIZE)
            {
                Debug.LogError("Panel cannot have an X-coordinate larger than the LogicGameModel's max grid size");
            }
            Assert.IsTrue(value <= LogicGameModel.MAX_GRID_SIZE - 1, "X coordinate must be less than the LogicGameModel's max grid size");
            gridX = value;
        }
    }

    /// <summary>
    /// Accessor for grid Y coordinate
    /// </summary>
    public int GridY
    {
        /// <summary>
        /// Getter for the Y-coordinate
        /// </summary>
        /// <returns>This Panel's Y-coordinate</returns>
        /// <remarks>
        /// preconditions:
        ///     - None
        /// postconditions:
        ///     - The Y-coordinate is returned
        /// </remarks>
        get
        {
            return gridY;
        }

        /// <summary>
        /// Setter for the Y-coordinate
        /// </summary>
        /// <remarks>
        /// preconditions:
        ///     - when writing, Y must be non-negative and less than the LogicGameModel's grid size
        /// postconditions:
        ///     - the Y-coordinate is updated to the new value
        /// </remarks>
        set
        {
            if(value < 0)
            {
                Debug.LogError("Panel cannot have a negative Y-coordinate");
            }
            Assert.IsTrue(value >= 0, "Y coordinate must be greater than 0");
            if(value >= LogicGameModel.MAX_GRID_SIZE)
            {
                Debug.LogError("Panel cannot have an X-coordinate larger than the LogicGameModel's max grid size");
            }
            Assert.IsTrue(value <= LogicGameModel.MAX_GRID_SIZE - 1, "Y coordinate must be less than the LogicGameModel's max grid size");
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
