using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model component of BrainModel.
/// </summary>
public class BrainModel : Model, IBrainModel
{
    /// <summary>
    /// Reference to the Animator component (used to control brain spin animation)
    /// </summary>
    private Animator animator;

    /// <inheritdoc/>
    public void Awake()
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Start() { }

    /// <inheritdoc/>
    public override void Init()
    {
        if (animator != null) {
            Debug.LogError("Animator already exists");
        }
        Assert.IsNull(animator, "Animator must be null prior to initialize");
        this.animator = GetComponent<Animator>();
        Assert.IsNotNull(animator, "Animator failed to initialize in BrainModel");
    }

    /// <inheritdoc/>
    public void pause()
    {
        Assert.IsNotNull(animator, "Animator must be initialized before pause() is called");
        if (animator.speed == 0)
        {
            Debug.Log("Animation is already on pause");
        }
        animator.speed = 0f;
        Assert.IsTrue(animator.speed == 0f, "Animation speed failed to set to 0 on pause()");
    }

    /// <inheritdoc/>
    public void resume()
    {
        Assert.IsNotNull(animator, "Animator must be initialized before resume() is called");
        if (animator.speed > 0)
        {
            Debug.Log("Animation is already running, cannot resume further");
        }
        animator.speed = 1.0f;
        Assert.IsTrue(animator.speed == 1.0f, "Animation failed to set to 1.0f on resume()");
    }
}
