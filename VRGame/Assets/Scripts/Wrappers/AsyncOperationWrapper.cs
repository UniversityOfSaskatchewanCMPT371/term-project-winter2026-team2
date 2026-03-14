using System;
using System.ComponentModel;
using UnityEngine.Assertions;

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
        return operation.isDone;
    }
}