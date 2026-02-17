using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private static PlayerSpawner instance { get; set; }          // Singleton instance of the _Services
    public GameObject XRplayerRigPrefab;

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

        SpawnPlayer();
        instance = this;
        DontDestroyOnLoad(instance);
    }

    public void SpawnPlayer()
    {
        Instantiate(XRplayerRigPrefab, new Vector3(), new Quaternion(), null);
    }
}
