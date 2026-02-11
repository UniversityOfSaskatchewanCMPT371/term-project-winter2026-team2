using UnityEngine;
using System;
using UnityEngine.Assertions;

/// <summary>
/// TODO: Add a description here.
/// </summary>
public class ControllerTemplate : Controller, IControllerTemplate
{
    /*
    ======================== DATA SECTION ========================
    */

    /// <summary>
    /// Reference to the Model layer.
    /// </summary>
    [SerializeField] private ModelTemplate ModelRef;

    /// <summary>
    /// Reference to the View layer.
    /// </summary>
    [SerializeField] private ViewTemplate ViewRef;

    /*
    =================== PRIVATE METHODS SECTION ==================
    */

    /// <summary>
    /// Updates the View with the new counter value.
    /// </summary>
    /// <exception cref="MissingReferenceException"></exception>
    private void CountUpdate(int amount)
    {
        if (ViewRef == null)
            throw new MissingReferenceException("Reference to the View layer is missing");
        ViewRef.OnExampleUpdate(amount);
    }

    /*
    =================== PUBLIC METHODS SECTION ===================
    */

    /// <summary>
    /// Updates the data Example in Model layer.
    /// </summary>
    public void Count()
    {
        try
        {
            if (ModelRef == null)
                throw new MissingReferenceException("Reference to the Model layer is missing");
            ModelRef.IncrementExample();
        } catch (InvalidOperationException err) {
            Debug.LogWarning(err.Message);
        } catch (MissingReferenceException err) {
            Debug.LogWarning(err.Message);
        } catch (Exception err)
        {
            Debug.LogError(err.Message);
        } 
        
        try
        {
            if (ModelRef == null)
                throw new MissingReferenceException("Reference to the Model layer is missing");
            CountUpdate(ModelRef.GetExample());
        } catch (MissingReferenceException err) {
            Debug.LogWarning(err.Message);
        } catch (Exception err)
        {
            Debug.LogError(err.Message);
        } finally
        {
            CountUpdate(-1);
        }
    }

    /*
    =================== RUNTIME METHODS SECTION ===================
    */

    /// <summary>
    /// Verify all layer wirings.
    /// </summary>
    void Awake()
    {
        Debug.Assert(ModelRef != null, "Field ModelRef cannot be null");
        Debug.Assert(ViewRef != null, "Field ViewRef cannot be null");
    }
}