using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

/// <summary>
/// Controller component of CheckButton.
/// </summary>
public class CheckButtonController : Controller<ICheckButtonModel, ICheckButtonView>, ICheckButtonController
{
    // References to other controllers
    [SerializeField] private ControllerComponent TargetBlockController;
    [SerializeField] private ControllerComponent CheckAreaController;

    // Rederences to the controllers as interfaces
    private ITargetBlockController targetBlockController;
    private ICheckAreaController checkAreaController;

    /// <inheritdoc/>
    public void Awake()
    {
        Init();
    }

    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckModelRef();
        this.CheckViewRef();

        // Set up reference to TargetBlockController 
        if (TargetBlockController != null)
        {
            targetBlockController = TargetBlockController as ITargetBlockController;
        }
        Assert.IsNotNull(targetBlockController, "'targetBlockController' must not be null.");

        // Set up reference to CheckAreaController
        if (CheckAreaController != null)
        {
            checkAreaController = CheckAreaController as ICheckAreaController;
        }
        Assert.IsNotNull(checkAreaController, "'checkAreaController' must not be null.");
    }

    /// <inheritdoc/>
    public void OnButtonPressed()
    {
        // Trigger scan animation on the check area
        modelInstance.Scanner.SetTrigger("scan");
        // Count colliders inside the check area
        var found = new HashSet<SnapFitController>();
        foreach (var collider in checkAreaController.GetInside())
        {
            var c = collider.GetComponent<SnapFitController>();
            if (c != null)
            {
                found.Add(c);
            }
        }

        Debug.Log($"[CheckButton] {found.Count} blocks found in area.");
        // Call CheckCompletion on found blocks in the check area
        targetBlockController.CheckCompletion(new List<SnapFitController>(found).ToArray());
    }
}
