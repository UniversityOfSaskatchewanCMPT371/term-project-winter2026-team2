using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// TODO: Add a description here.
/// </summary>
public class ViewTemplate : View, IViewTemplate
{
    /*
    ======================== DATA SECTION ========================
    */

    /// <summary>
    /// Reference to the Controller layer.
    /// </summary>
    [SerializeField] private ControllerTemplate ControllerRef;

    /// <summary>
    /// Invoked when Example changes value.
    /// </summary>
    public UnityEvent<int> OnExampleEvent;

    /*
    =================== PRIVATE METHODS SECTION ==================
    */

    /// <summary>
    /// Example of a user input (such as button press or collision)
    /// </summary>
    /// <exception cref="MissingReferenceException"></exception>
    private void ExampleUserInput()
    {
        try
        {
            if (ControllerRef == null)
                throw new MissingReferenceException("Reference to Controller layer is missing");

            ControllerRef.Count();
        } catch (MissingReferenceException err)
        {
            Debug.LogWarning(err.Message);
        } catch (Exception err)
        {
            Debug.LogError(err.Message);
        }
    }

    /*
    =================== PUBLIC METHODS SECTION ===================
    */

    /// <summary>
    /// Invokes all functions connected to OnExampleEvent with the updated Example.
    /// </summary>
    /// <param name="amount">The updated value of Example.</param>
    public void OnExampleUpdate(int amount)
    {
        // All attached functions will be invoked
        OnExampleEvent.Invoke(amount);
    }

    /*
    =================== RUNTIME METHODS SECTION ===================
    */

    /// <summary>
    /// Verify layer wirings.
    /// </summary>
    void Awake()
    {
        Debug.Assert(ControllerRef != null, "Field ControllerRef cannot be null");
    }

    void Start()
    {
        InvokeRepeating("ExampleUserInput", 0, 1);

        // TODO: Your code here. (can be deleted)
    }

    void Update()
    {
        // TODO: Your code here. (can be deleted)
    }
}