/// <summary>
/// Interface for controller component.
/// </summary>
public interface IController
{   
    /// <summary>
    /// Called by Start() method at runtime after CheckLayerRefs() is called.
    /// This method is to be overriden by the subclass that implements it
    /// and its docstring should be changed accordingly.
    /// </summary>
    void Init();
}