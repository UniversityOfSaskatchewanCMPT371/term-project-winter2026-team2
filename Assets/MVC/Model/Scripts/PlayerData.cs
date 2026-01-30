using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static PlayerData instance { get; set; }
    
    /// <summary>
    /// Ensures this component follows a singleton pattern and persists across scene loads.
    /// </summary>
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
