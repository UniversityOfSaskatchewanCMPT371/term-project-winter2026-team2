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
    private void CountUpdate()
    {
        if (ViewRef == null)
            throw new MissingReferenceException("Reference to the View layer is missing.");

        ViewRef.OnExampleUpdate(ModelRef.GetExample());
    }

    /*
    =================== PUBLIC METHODS SECTION ===================
    */

    /// <summary>
    /// Updates the data Model's counter.
    /// </summary>
    /// <exception cref="MissingReferenceException"></exception>
    public void Count()
    {
        if (ModelRef == null)
            throw new MissingReferenceException("Reference to the Model layer is missing.");
        
        try
        {
            ModelRef.IncrementExample();
            CountUpdate();
        } catch (InvalidOperationException err)
        {
            Debug.LogError(err);

            // Exit early since there's no new update
            return;
        } catch (MissingReferenceException err) {
            Debug.LogWarning(err);
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
        Assert.IsNotNull<Model>(ModelRef, "Field ModelRef cannot be null.");
        Assert.IsNotNull<View>(ViewRef, "Field ViewRef cannot be null.");
    }
}