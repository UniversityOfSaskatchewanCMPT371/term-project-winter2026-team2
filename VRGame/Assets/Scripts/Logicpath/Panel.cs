#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents a single panel in the logic path minigame
/// </summary>
public enum PanelAttribute
{
    Normal,
    Start,
    Exit,
    Block
}

/// <summary>
/// Represents a single panel in the logic path minigame
/// Manages pipe connections
/// </summary>
public class Panel : MonoBehaviour
{
    private Direction entryDirection;
    private Direction exitDirection;
    private Panel? topNeighbor;
    private Panel? rightNeighbor;
    private Panel? downNeighbor;
    private Panel? leftNeighbor;
    [SerializeField]
    private PanelAttribute attribute;
    [SerializeField]
    private PanelColour panelColour;
    [SerializeField]
    private int gridX;
    [SerializeField]
    private int gridY;
    private PanelTextureManager panelTextureManager;
    private XRSimpleInteractable xRSimpleInteractable;

    /// <summary>
    /// Accessor for entry direction
    /// </summary>
    public Direction EntryDirection
    {
        get 
        {
            return entryDirection;
        }

        set
        {
            entryDirection = value;
        }
    }

    /// <summary>
    /// Accessor for exit direction
    /// </summary>
    public Direction ExitDirection
    {
        get
        {
            return exitDirection;
        }

        set 
        {
            exitDirection = value;
        }
    }

    /// <summary>
    /// Accessor for top neighbour panel
    /// </summary>
    public Panel? TopNeighbor
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
    public Panel? RightNeighbor
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
    /// Accessor for panel attribute (normal, start, exit)
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
        }
    }

    /// <summary>
    /// Accessor for panel color - null if irrelevant
    /// </summary>
    public PanelColour? getPanelColour()
    {
            if (entryDirection == Direction.None || attribute == PanelAttribute.Block)
            {
                return null;
            }
            else
            {
                return panelColour;
            }
    }

    public void setPanelColour(PanelColour panelColour)
    {
        this.panelColour = panelColour;
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
    /// Checks if the panel has a pipe placed in it
    /// </summary>
    /// <remarks>
    /// <preconditions>
    ///     - None
    /// </preconditions>
    /// <postconditions>
    ///     - Returns true if there is a pipe in the panel, false otherwise
    /// </postconditions>
    /// </remarks>
    public bool IsOccupied()
    {
        return entryDirection != Direction.None || attribute == PanelAttribute.Block;
    }

    /// <summary>
    /// Clears the pipe from this panel, resetting entry and exit directions
    /// </summary>
    /// <remarks>
    /// <preconditions>
    ///     - None
    /// </preconditions>
    /// <postconditions>
    ///     - Resets entry and exit directions to None
    ///     - Resets pipe color to white (default)
    /// </postconditions>
    /// </remarks>
    public void ClearPanel()
    {
        entryDirection = Direction.None;
        exitDirection = Direction.None;
        //pipeColor = Color.white;
    }

    /// <summary>
    /// Initializes the panel with grid coordinates and world position
    /// </summary>
    /// <param name="x">Grid X coordinate</param>
    /// <param name="y">Grid Y coordinate</param>
    /// <param name="worldPos">World position of the panel</param>
    /// <remarks>
    /// <preconditions>
    ///     - x and y must be non-negative
    ///     - worldPos must be a valid Vector3
    /// </preconditions>
    /// <postconditions>
    ///     - Panel is initialized with coordinates and position
    ///     - Directions are set to None
    ///     - Neighbours are null
    /// </postconditions>
    /// </remarks>
    public void Initialize()
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
    }

    public void Awake()
    {
        xRSimpleInteractable = GetComponent<XRSimpleInteractable>();

        xRSimpleInteractable.hoverEntered.AddListener(OnHoverEntered);
        xRSimpleInteractable.hoverExited.AddListener(OnHoverExited);
    }

    public void OnDestroy()
    {
        xRSimpleInteractable.hoverExited.RemoveListener(OnHoverExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"Panel at ({this.gridX},{this.gridY}) is hovered over");
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"Panel at ({this.gridX},{this.gridY}) is no longer being hovered over");
    }
}
