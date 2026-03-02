/// <summary>
/// Interface for view component.
/// </summary>
public interface IView
{   
    /// <summary>
    /// Called by Start() method at runtime after CheckLayerRefs() is called.
    /// This method is to be overriden by the subclass that implements it
    /// and its docstring should be changed accordingly.
    /// </summary>
    void Init();
}
