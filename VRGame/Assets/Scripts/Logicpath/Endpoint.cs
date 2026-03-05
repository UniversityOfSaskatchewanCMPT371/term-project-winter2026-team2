using System.Text;
using UnityEngine;

public class Endpoint
{
    
    /// <summary>
    /// X coordinate of the endpoint
    /// </summary>
    public int GridX
    {
        get;
        set;
    }

    /// <summary>
    /// Y coordinate of the endpoint
    /// </summary>
    public int GridY
    {
        get;
        set;
    }

    /// <summary>
    /// Color of the endpoint
    /// </summary>
    public Color EndColor
    {
        get;
        set;
    }

    /// <summary>
    /// ID of the colour pair this endpoint belongs to
    /// </summary>
    public int PairId
    {
        get;
        set;
    }

    /// <summary>
    /// Whether this endpoint is currently connected to its pair
    /// Default value is false
    /// </summary>
    public bool IsConnected
    {
        get;
        set;
    } = false;

}