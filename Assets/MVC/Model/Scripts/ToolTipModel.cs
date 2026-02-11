using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CreateAssetMenu(fileName ="NewToolTipData",menuName ="UI/Tooltip")]
public class ToolTipModel : ScriptableObject
{
    public string toolTipText;
    public Sprite toolTipSprite;
    public Vector3 toolTipOffset;
    public float toolTipDisplayTime;
}