using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance { get; set; }
    
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
