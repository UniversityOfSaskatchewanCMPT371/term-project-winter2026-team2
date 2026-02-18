using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Represents a data model for tooltip content, including a title and description, that can be configured as a Unity
/// ScriptableObject asset.
/// </summary>
/// <remarks>Use this type to define reusable tooltip data for UI elements in Unity projects. The asset can be
/// created and edited in the Unity Editor via the 'VR/Data' menu.</remarks>
[CreateAssetMenu(fileName ="Data",menuName ="VR/Data")]
public class ToolTipModel : ScriptableObject
{
    public string title;
    [TextArea]public string description;
}
   