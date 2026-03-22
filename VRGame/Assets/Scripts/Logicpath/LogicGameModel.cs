using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// The model of the LogicGame itself. Manages the initial setup of panels
/// </summary>

public class LogicGameModel : MonoBehaviour, IGridModel
{
    /// <summary>
    /// The bounds of any logic game grid
    /// </summary>
    public static int MAX_GRID_SIZE = 10; // look, there ain't no way that we're gonna have puzzles larger than 10x10

    // please note that the panel's array layout is not going to be the orthodox [row, collumn] layout for ease of visualization
    // instead, we'll be using [x, y] where x is the horizontal axis and y is the vertical axis
    /// <summary>
    /// Every panel associated with this game
    /// </summary>
    private Panel[,] panelGrid;
    /// <summary>
    /// A dictionary of all the endpoints in this game
    /// </summary>
    private Dictionary<PanelColour, (Panel start, Panel end)> endpoints;

    /// <summary>
    /// Unity Start() method, initializes the game's data
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - The only children beneath this GameObject are Panels
    ///     - No two Panels have the same coordinates
    ///     - All Panel coordinates are non-negative and are less than MAX_GRID_SIZE
    ///     - Every start endpoint has an end endpoint, and vice versa
    ///     - There are no duplicate endpoints
    /// postconditions:
    ///     - All Panels are saved in this model
    ///     - Adjacent Panels have their *Neighbor fields set where necessary
    /// </remarks>
    public void Start()
    {
        panelGrid = new Panel[MAX_GRID_SIZE,MAX_GRID_SIZE];
        endpoints = new Dictionary<PanelColour, (Panel start, Panel end)>();

        foreach(Transform childTransform in transform)
        {
            Panel panel = childTransform.gameObject.GetComponent<Panel>();
            Assert.IsNotNull(panel, "Could not find a panel script attached to one of my children!");
            Assert.IsNull(panelGrid[panel.GridX,panel.GridY], $"There is already a panel at ({panel.GridX},{panel.GridY})");
            panelGrid[panel.GridX,panel.GridY] = panel;
            if(panel.Attribute == PanelAttribute.Start)
            {
                if(!endpoints.ContainsKey(panel.PanelColour))
                {
                    endpoints[panel.PanelColour] = (null, null);
                }
                Assert.IsNull(endpoints[panel.PanelColour].start, $"Duplicate start endpoint of colour {panel.PanelColour}");
                endpoints[panel.PanelColour] = (panel, endpoints[panel.PanelColour].end);
            }
            if(panel.Attribute == PanelAttribute.Exit)
            {
                if(!endpoints.ContainsKey(panel.PanelColour))
                {
                    endpoints[panel.PanelColour] = (null, null);
                }
                Assert.IsNull(endpoints[panel.PanelColour].end, $"Duplicate end endpoint of colour {panel.PanelColour}");
                endpoints[panel.PanelColour] = (endpoints[panel.PanelColour].start, panel);
            }

            if(panel.GridX > 0 && panelGrid[panel.GridX-1, panel.GridY] != null)
            {
                panel.LeftNeighbor = panelGrid[panel.GridX-1, panel.GridY];
                panelGrid[panel.GridX-1, panel.GridY].RightNeighbor = panel;
            }
            if(panel.GridY > 0 && panelGrid[panel.GridX, panel.GridY-1] != null)
            {
                panel.DownNeighbor = panelGrid[panel.GridX, panel.GridY-1];
                panelGrid[panel.GridX, panel.GridY-1].TopNeighbor = panel;
            }
            if(panel.GridX < MAX_GRID_SIZE-1 && panelGrid[panel.GridX+1, panel.GridY] != null)
            {
                panel.RightNeighbor = panelGrid[panel.GridX+1, panel.GridY];
                panelGrid[panel.GridX+1, panel.GridY].LeftNeighbor = panel;
            }
            if(panel.GridY < MAX_GRID_SIZE-1 && panelGrid[panel.GridX, panel.GridY+1] != null)
            {
                panel.TopNeighbor = panelGrid[panel.GridX, panel.GridY+1];
                panelGrid[panel.GridX, panel.GridY+1].DownNeighbor = panel;
            }
        }
        foreach((PanelColour colour, (Panel start, Panel end) pair) in endpoints)
        {
            Assert.IsNotNull(pair.start, $"Missing {colour}'s start endpoint");
            Assert.IsNotNull(pair.end, $"Missing {colour}'s end endpoint");
        }
    }

    /// <summary>
    /// Clears the state of all Panels
    /// </summary>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - Every Panel has its state reset
    /// </remarks>
    public void ClearGrid()
    {
        foreach(Panel panel in panelGrid)
        {
            panel?.ClearPanel();
        }
    }

    /// <summary>
    /// Gets a panel at specific coordinates
    /// </summary>
    /// <param name="x">The X-coordinate of the panel you want</param>
    /// <param name="y">The Y-coordinate of the panel you want</param>
    /// <returns>The panel with the XY coordinates</returns>
    /// <remarks>
    /// preconditions:
    ///     - X and Y are valid coordinates
    /// postconditions:
    ///     - None
    /// </remarks>
    public Panel GetPanel(int x, int y)
    {
        return panelGrid[x,y];
    }

    /// <summary>
    /// Is the current grid filled? (I.e, is the game complete?)
    /// </summary>
    /// <returns>true if every Panel is occupied, false otherwise</returns>
    /// <remarks>
    /// preconditions:
    ///     - None
    /// postconditions:
    ///     - The truth value of whether the current grid is filled or not is returned
    /// </remarks>
    public bool IsGridFilled()
    {
        foreach(Panel panel in panelGrid)
        {
            if(panel == null)
            {
                continue;
            }
            if (!panel.IsOccupied())
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Checks if a Panel is occupied
    /// </summary>
    /// <param name="x">The X-coordinate of the panel</param>
    /// <param name="y">The Y-coordinate of the panel</param>
    /// <returns>true if the Panel is occupied, false otherwise</returns>
    /// <remarks>
    /// preconditions:
    ///     - X and Y point to a valid Panel
    /// postcondidions:
    ///     - The truth value of if the Panel is occupied or not
    /// </remarks>
    public bool IsPanelOccupied(int x, int y)
    {
        return GetPanel(x, y).IsOccupied();
    }
}
