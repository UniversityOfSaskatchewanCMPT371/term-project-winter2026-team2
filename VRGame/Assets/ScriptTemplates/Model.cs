using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Model layer
/// </summary>
public class Model
{
    /// <summary>
    /// Verifies that the caller is of type Controller. Used to enforce Model layer contract.
    /// </summary>
    /// <param name="caller">The caller to be verified.</param>
    /// <returns>A boolean validating the type of the caller.</returns>
    private bool VerifyCaller(Object caller)
    {
        if (caller == null)
        {
            Assert.IsNotNull(caller, "Caller must pass itself as an arguement.");
            return false;
        }

        bool validate = caller is Controller;
        Assert.IsTrue(validate, $"Caller must be of type Controller. Got {caller.GetType()}");
        return validate;
    }
}