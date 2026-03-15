using System;
using UnityEngine.Assertions;
using UnityEngine;

/// <summary>
/// Wrapper class for AsyncOperations. Made for mocking purposes
/// </summary>
public class AsyncOperationWrapper : IAsyncOperationWrapper
{

    private UnityEngine.AsyncOperation operation;

    /// <inheritdoc />
    public event Action<IAsyncOperationWrapper> Completed;


    /// <inheritdoc/>
    public AsyncOperationWrapper(UnityEngine.AsyncOperation asyncOperation)
    {
        Assert.IsNotNull(asyncOperation, "asyncOperation cannot be null");
        operation = asyncOperation;

        operation.completed += (o) =>
        {

            // event is null if nothing is subscribed to it.
            // if this is the case, nothing should happen, no exception thrown
            if (Completed != null)
            {
                Completed.Invoke(this);
            }
        };
    }

    /// <inheritdoc/> 
    public bool IsDone()
    {
        if (operation == null)
        {
            Debug.LogError("'operation' vairable is null.");
        }
        Assert.IsNotNull(operation, "'operation' variable must be non-null.");

        return operation.isDone;
    }
}