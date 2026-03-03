using UnityEngine;

/// <summary>
/// Base class for model component.
/// </summary>
public abstract class Model : MonoBehaviour, IModel
{
    /// <inheritdoc/>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls. Invokes Init() method.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'Init()' is implemented by the subclass.
    /// Postcondition:
    /// - 'Init()' is invoked.
    /// </remarks>
    public virtual void Start()
    {
        Init();
    }
}