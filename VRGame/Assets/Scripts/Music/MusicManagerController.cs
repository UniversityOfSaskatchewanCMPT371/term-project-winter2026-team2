using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Controller for the MusicManager component. Manages persistent music accross scenes
/// </summary>
public class MusicManagerController : MonoBehaviour, IMusicManagerController
{

    /// <summary>
    /// Singleton instance of the MusicManagerController
    /// </summary>
    private static MusicManagerController instance;

    /// <summary>
    /// The audio clip to be played as background music
    /// </summary>
    [SerializeField] private AudioClip musicClip;

    /// <summary>
    /// The volume of the music, from 0 (silent) to 1 (full volume)
    /// </summary>
    [SerializeField] [Range(0f, 1f)] private float volume = 0.2f;

    /// <summary>
    /// The AudioSource component used to play the music
    /// </summary>
    private AudioSource audioSource;


    /// <inheritdoc/>
    public void Awake()
    {
        Init();
    }

    /// <inheritdoc/>
    public void Init()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("MusicManagerController duplicate destroyed");
            Destroy(gameObject);
            return;
        }

        if (musicClip == null)
        {
            Debug.LogError("musicClip is not assigned");
        }
        Assert.IsNotNull(musicClip, "musicClip must be assigned in the Inspector");

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();

        Debug.Log("MusicManagerController initialized successfully");
    }

    /// <inheritdoc/>
    public void ResetInstance()
    {
        instance = null;
    }
}
