using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerData : MonoBehaviour
{
    private static PlayerData instance { get; set; }
    
    /// <summary>
    /// Ensures this component follows a singleton pattern and persists across scene loads.
    /// </summary>
    /// <remarks>
    /// <pre-condition>
    ///     -   This script is attached to a GameObject in the scene
    /// </pre-condition>
    /// <post-condition>
    ///     -   Only one instance of PlayerData exists and it persists across scene loads
    /// </post-condition>
    /// </remarks>
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
