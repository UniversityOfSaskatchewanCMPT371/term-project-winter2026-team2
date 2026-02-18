
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Wrapper class for AsyncOperations. Made for mocking purposes
/// </summary>
public class AsyncOperationWrapper : IAsyncOperationWrapper
{
    /// <summary>
    /// The event action that is invoked when the async operation is completed
    /// </summary>
    public event Action<IAsyncOperationWrapper> Completed;

    /// <summary>
    /// Wrapper for Async operation.
    /// Works exactly the same as a regular AsyncOperation, but is wrapped
    /// in an interface meaning it can be mocked out for unit testing.
    /// </summary>
    /// <param name="asyncOperation">AsyncOperation to be executed</param>
    /// <remarks>
    /// PreConditions:
    /// asyncOperation must not be null
    /// PostCondtions:
    /// asyncOperations is carried out, subscribers notified when complete
    public AsyncOperationWrapper(UnityEngine.AsyncOperation asyncOperation)
    {
        Assert.IsNotNull(asyncOperation, "asyncOperation cannot be null");
        asyncOperation.completed += (o) => {

            // event is null if nothing is subscribed to it.
            // if this is the case, nothing should happen, no exception thrown
            if (Completed != null) 
            {
                Completed.Invoke(this);
            }
        };
    }
}