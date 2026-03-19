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
    private Panel topNeighbor;
    private Panel rightNeighbor;
    private Panel downNeighbor;
    private Panel leftNeighbor;
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
    private LogicGameController logicGameController;

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
            panelTextureManager.RefreshTexture();
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
            panelTextureManager.RefreshTexture();
        }
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
        return entryDirection != Direction.None || attribute == PanelAttribute.Block;
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
    /// </postconditions>
    /// </remarks>
    public void ClearPanel()
    {
        entryDirection = Direction.None;
        exitDirection = Direction.None;
        panelTextureManager.RefreshTexture();
    }

    /// <summary>
    /// Initializes the panel with grid coordinates and world position
    /// </summary>
    /// <remarks>
    /// <preconditions>
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

    public void OnDestroy()
    {
        xRSimpleInteractable.hoverEntered.RemoveListener(OnHoverEntered);
        xRSimpleInteractable.hoverExited.AddListener(OnHoverExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"Panel at ({gridX},{gridY}) is hovered over");
        logicGameController.HandleHover(gridX, gridY);
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"Panel at ({gridX},{gridY}) is no longer being hovered over");
        logicGameController.HandleUnhover(gridX, gridY);
    }
}
