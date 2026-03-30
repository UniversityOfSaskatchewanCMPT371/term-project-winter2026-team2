using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model component of DeleteBlockModel.
/// </summary>
public class DeleteBlockModel : Model, IDeleteBlockModel
{

    /// <inheritdoc/>
    public override void Init()
    {

    }

    /// <summary>
    /// Reference to the block's collider component
    /// </summary>
    [SerializeField] private Collider blockCollider;


    /// </inheritdoc/>
    public Collider BlockCollider
    {
        get
        {
            return this.blockCollider;
        }
        set
        {
            Assert.IsNotNull(value, "Collider reference cannot be null");
            this.blockCollider = value;
        }

    }
}
