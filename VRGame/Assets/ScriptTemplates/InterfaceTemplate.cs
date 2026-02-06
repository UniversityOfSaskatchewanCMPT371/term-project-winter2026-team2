/// <summary>
/// Base internal interface for all layers.
/// </summary>
public interface InternalInterface
{
    /*
    Write methods here that is exclusive ONLY to this layer.
    */

    /// <summary>
    /// Example method exclusive to this layer.
    /// </summary>
    void ExclusiveMethod();
}

/// <summary>
/// Base external interface for all layers.
/// </summary>
public interface ExternalInterface
{
    /*
    Write methods here that you will exposing to OTHER layers
    */

    /// <summary>
    /// Example method exposed to other layer.
    /// </summary>
    void ExposedMethod();
}