
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
    /// <inheritdoc />
    public event Action<IAsyncOperationWrapper> Completed;

    /// <inheritdoc/>
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