using UnityEngine;

/// <summary>
/// Base class for model component.
/// </summary>
public abstract class Model : MonoBehaviour, IModel
{
    /// </inheritdoc>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls. Inherited from MonoBehaviour.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - 'Init()' method is implemented by the subclass.
    ///
    /// Postcondition:
    /// - Calls 'Init()' method after all Awake() calls have completed.
    /// </remarks>
    public virtual void Start()
    {
        Init();
    }
}