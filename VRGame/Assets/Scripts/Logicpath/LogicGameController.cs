using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGameController : MonoBehaviour
{
    private bool isDragging;
    private LogicGameModel logicGameModel;
    private CoordinateRef targetedPanel;

    public void Awake()
    {
        isDragging = false;
        logicGameModel = gameObject.GetComponent<LogicGameModel>();
        targetedPanel = null;
    }
}
