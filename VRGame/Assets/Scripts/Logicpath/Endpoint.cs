using System.Text;
using UnityEngine;

public class Endpoint
{
    
    /// <summary>
    /// X coordinate of the endpoint
    /// </summary>
    public int gridX
    {
        get;
        set;
    }

    /// <summary>
    /// Y coordinate of the endpoint
    /// </summary>
    public int gridY
    {
        get;
        set;
    }

    /// <summary>
    /// Color of the endpoint
    /// </summary>
    public Color color
    {
        get;
        set;
    }

    /// <summary>
    /// ID of the colour pair this endpoint belongs to
    /// </summary>
    public int pairId
    {
        get;
        set;
    }

    /// <summary>
    /// Whether this endpoint is currently connected to its pair
    /// Default value is false
    /// </summary>
    public bool isConnected
    {
        get;
        set;
    } = false;

}