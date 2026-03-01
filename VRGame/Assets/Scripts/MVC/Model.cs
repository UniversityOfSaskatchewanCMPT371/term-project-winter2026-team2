using UnityEngine;

/// <summary>
/// Base class for model component.
/// </summary>
public abstract class Model : MonoBehaviour
{
    /// </inheritdoc>
    public abstract void Init();

    /// <summary>
    /// Called once after all Awake() calls.
    /// Inherited from MonoBehaviourl.
    /// </summary>
    /// <remarks>
    /// Precondition:
    /// - Init() method is implemented.
    /// Postcondition:
    /// - Calls Init() method.
    /// <remarks>
    public virtual void Start()
    {
        Init();
    }
}