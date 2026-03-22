using System;

public interface IAsyncOperationWrapper
{

    /// <summary>
    /// The event action that is invoked when the async operation is completed
    /// </summary>
    public event Action<IAsyncOperationWrapper> Completed;

    /// <summary>
    /// Checks if async operation held by wrapper is complete
    /// </summary>
    /// <returns>true if operation is complete, false otherwise</returns>
    /// <remarks>
    /// Preconditions
    /// - Wrapper must currently hold non-null async operation
    /// Posconditions
    /// - Status of async operation completeness is returnesda
    /// </remarks>
    public bool IsDone();


    /// <summary>
    /// Wrapper for Async operation.
    /// Works exactly the same as a regular AsyncOperation, but is wrapped
    /// in an interface meaning it can be mocked out for unit testing.
    /// </summary>
    /// <param name="asyncOperation">asyncOperation to be executed</param>
    /// <remarks>
    /// PreConditions:
    /// asyncOperation must not be null
    /// PostCondtions:
    /// asyncOperations is carried out, subscribers notified when complete
    //public AsyncOpWrapper(AsyncOperation asyncOperation);

}