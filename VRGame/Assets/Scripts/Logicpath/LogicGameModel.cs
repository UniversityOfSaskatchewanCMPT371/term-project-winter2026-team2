using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// The model of the LogicGame itself. Manages the initial setup of panels
/// </summary>
public class LogicGameModel : MonoBehaviour, ILogicGameModel
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

    /// <inheritdoc/>
    public void Init()
    {
        panelGrid = new Panel[MAX_GRID_SIZE,MAX_GRID_SIZE];
        endpoints = new Dictionary<PanelColour, (Panel start, Panel end)>();

        foreach(Transform childTransform in transform)
        {
            Panel panel = childTransform.gameObject.GetComponent<Panel>();
            if(panel == null)
            {
                Debug.LogError("Could not find a panel script attached to one of my children!");
            }
            Assert.IsNotNull(panel, "Could not find a panel script attached to one of my children!");
            if(panelGrid[panel.GridX,panel.GridY] != null)
            {
                Debug.LogError($"There is already a panel at ({panel.GridX},{panel.GridY})");
            }
            Assert.IsNull(panelGrid[panel.GridX,panel.GridY], $"There is already a panel at ({panel.GridX},{panel.GridY})");
            panelGrid[panel.GridX,panel.GridY] = panel;
            if(panel.Attribute == PanelAttribute.Start)
            {
                if(!endpoints.ContainsKey(panel.PanelColour))
                {
                    endpoints[panel.PanelColour] = (null, null);
                }
                if(endpoints[panel.PanelColour].start != null)
                {
                    Debug.LogError($"Duplicate start endpoint of colour {panel.PanelColour}");
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
                if(endpoints[panel.PanelColour].end != null)
                {
                    Debug.LogError($"Duplicate end endpoint of colour {panel.PanelColour}");
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
            if(pair.start == null)
            {
                Debug.LogError($"Missing {colour}'s start endpoint");
            }
            Assert.IsNotNull(pair.start, $"Missing {colour}'s start endpoint");
            if(pair.end == null)
            {
                Debug.LogError($"Missing {colour}'s end endpoint");
            }
            Assert.IsNotNull(pair.end, $"Missing {colour}'s end endpoint");
        }
    }

    /// <summary>
    /// Unity Start() method - initialize the game state
    /// </summary>
    public void Start()
    {
        Init();
    }

    /// <inheritdoc/>
    public void ClearGrid()
    {
        foreach(Panel panel in panelGrid)
        {
            panel?.ClearPanel();
        }
    }

    /// <inheritdoc/>
    public Panel GetPanel(int x, int y)
    {
        return panelGrid[x,y];
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public bool IsPanelOccupied(int x, int y)
    {
        return GetPanel(x, y).IsOccupied();
    }
}
