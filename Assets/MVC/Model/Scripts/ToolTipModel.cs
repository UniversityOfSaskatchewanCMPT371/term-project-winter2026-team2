using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CreateAssetMenu(fileName ="Data",menuName ="VR/Data")]
public class ToolTipModel : ScriptableObject
{
    public string title;
    [TextArea]public string description;
}
   