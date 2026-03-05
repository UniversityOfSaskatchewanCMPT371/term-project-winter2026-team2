using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelAttribute
{
    Normal,
    Start,
    Exit
}

public class Panel : MonoBehaviour
{
    private Direction entryDirection;
    private Direction exitDirection;
    private Panel topNeighbor;
    private Panel rightNeighbor;
    private Panel downNeighbor;
    private Panel leftNeighbor;
    private PanelAttribute attribute;

    void Awake()
    {
        entryDirection = Direction.None;
        exitDirection = Direction.None;
        topNeighbor = null;
        rightNeighbor = null;
        downNeighbor = null;
        leftNeighbor = null;
        
    }

}
