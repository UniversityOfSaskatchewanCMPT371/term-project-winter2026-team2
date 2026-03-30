using UnityEngine;

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
    [SerializeField] private Collider collider;


    /// </inheritdoc/>
    public Collider BlockCollider
    {
        get
        {
            return this.collider;
        }
        set
        {
            Assert.IsNotNull(value, "Collider reference cannot be null");
            this.collider = value;
        }

    }
}
