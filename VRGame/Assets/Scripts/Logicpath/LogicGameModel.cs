using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LogicGameModel : MonoBehaviour, IGridModel
{
    // please note that the panel's array layout is not going to be the orthodox [row, collumn] layout for ease of visualization
    // instead, we'll be using [x, y] where x is the horizontal axis and y is the vertical axis
    private Panel[,] panelGrid;
    private Dictionary<PanelColour, (Panel start, Panel end)> endpoints;

    public void Awake()
    {
        panelGrid = new Panel[10,10]; // look, there ain't no way that we're gonna have puzzles larger than 10x10
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
        }
        foreach((PanelColour colour, (Panel start, Panel end) pair) in endpoints)
        {
            Assert.IsNotNull(pair.start, $"Missing {colour}'s start endpoint");
            Assert.IsNotNull(pair.end, $"Missing {colour}'s end endpoint");
        }
    }

    public void ClearGrid()
    {
        throw new NotImplementedException();
    }

    public Panel GetPanel(int x, int y)
    {
        throw new NotImplementedException();
    }

    public bool IsGridFilled()
    {
        throw new NotImplementedException();
    }

    public bool IsPanelOccupied(int x, int y)
    {
        throw new NotImplementedException();
    }
}
