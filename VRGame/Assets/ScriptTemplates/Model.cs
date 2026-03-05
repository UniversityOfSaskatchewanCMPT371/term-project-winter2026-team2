using UnityEngine;

/// <summary>
/// MonoBehaviour wrapper. This used specifically for serialized layer fields
/// so that it shows in the inspector that a component requires a 'ModelComponent' instead of 'MonoBehaviour'.
/// </summary>
public class ModelComponent : MonoBehaviour {}

/// <summary>
/// Base class for model component.
/// </summary>
public abstract class Model : ModelComponent, IModel
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