using UnityEditor.XR.Interaction.Toolkit.Utilities;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class Services : MonoBehaviour
{
    private static Services instance { get; set; }          // Singleton instance of the _Services

    public SceneChanger sceneChanger;
    public PlayerSpawner playerSpawner;

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
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        DontDestroyOnLoad(FindObjectOfType<XRUIInputModule>());
        DontDestroyOnLoad(FindObjectOfType<XRInteractionManager>());
    }
}
