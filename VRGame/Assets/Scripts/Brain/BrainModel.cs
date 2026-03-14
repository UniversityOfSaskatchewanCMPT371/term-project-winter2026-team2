using UnityEngine;

/// <summary>
/// Model component of BrainModel.
/// </summary>
public class BrainModel : Model, IBrainModel
{
    private Animator animator;

    /// <inheritdoc/>
    public override void Init()
    {
        this.animator = GetComponent<Animator>();
        if (animator == null) {
            Debug.LogError("Animator did not initialize properly in BrainModel Init()");
        }
        Assert.IsNotNull(animator, "Animator is null on Init()");
    }

    /// <inheritdoc/>
    public void pause()
    {
        animator.speed = 0f;
    }

    /// <inheritdoc/>
    public void resume()
    {
        animator.speed = 1.0f;
    }
}
