using UnityEngine;

/// <summary>
/// Controller component of CheckArea.
/// Tracks colliders entering and exiting the check area
/// </summary>
public class CheckAreaController : Controller<ICheckAreaModel, ICheckAreaView>, ICheckAreaController
{
    /// <inheritdoc/>
    public void Awake()
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckModelRef();
        this.CheckViewRef();
    }

    /// <inheritdoc/>
    public void OnEnter(Collider collider)
    {
        modelInstance.InsideColliders.Add(collider);
    }

    /// <inheritdoc/>
    public void OnExit(Collider collider)
    {
        modelInstance.InsideColliders.Remove(collider);
    }

    /// <inheritdoc/>
    public Collider[] GetInside()
    {
        // Return a copy of the colliders currently inside the check area
        var colliders = new Collider[modelInstance.InsideColliders.Count];
        modelInstance.InsideColliders.CopyTo(colliders);
        return colliders;
    }
}
