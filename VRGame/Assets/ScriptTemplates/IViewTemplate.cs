/// <summary>
/// Template interface for View layer.
/// </summary>
public interface IViewTemplate
{
    /*
    =================== PUBLIC METHODS SECTION ===================
    */

    /// <summary>
    /// Invokes all functions connected to OnExampleEvent with the updated Example.
    /// </summary>
    /// <param name="amount">The updated value of Example.</param>
    public void OnExampleUpdate(int amount);
}