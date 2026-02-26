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
public class ToolTipModel : ScriptableObject, IToolTipModel

{
    /// <summary>
    /// The title associated with this object.
    /// </summary>
    [SerializeField] private string title;
    /// <summary>
    /// The description associated with this object.
    /// </summary>
    [SerializeField, TextArea] private string description;
    /// <summary>
    /// <para>Gets or sets the title associated with the object.</para>
    /// 
    /// </summary>
    public string Title 
    { 
        get => title;
        set => title = value;
    }

    /// <summary>
    /// <para>Gets or sets the description associated with the object.</para>
    /// </summary>
    public string Description 
    { 
        get => description; 
        set => description = value; 
    }

    
}
   