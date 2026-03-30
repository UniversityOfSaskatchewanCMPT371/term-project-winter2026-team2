using UnityEngine;
using UnityEngine.Assertions;
/// <summary>
/// Model component of CheckButton.
/// No data needed — CheckArea owns the collider tracking.
/// </summary>
public class CheckButtonModel : Model, ICheckButtonModel
{
    /// <inheritdoc/>
    public override void Init() { }

    /// <summary>
    /// Reference to the CheckArea's scanner animator
    /// </summary>
    [SerializeField] private Animator scanner;

    /// <inheritdoc/>
    public Animator Scanner
    {
        get
        {
            return this.scanner;
        }
        set
        {
            Assert.IsNotNull(value, "Scanner reference cannot be null");
            this.scanner = value;
        }
    }
}
