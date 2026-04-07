using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// View component of SpawnButtonView.
/// </summary>
public class SpawnButtonView : View<ISpawnButtonController>, ISpawnButtonView
{
    /// <inheritdoc/>
    public override void Init()
    {
        this.CheckControllerRef();

        SetupXREvents();
    }

    /// <inheritdoc/>
    public void SetupXREvents()
    {
        var components = GetComponentsInChildren<XRBaseInteractable>();
        if (components.Length == 0)
        {
            Debug.LogWarning("Cannot get components, none exist");
        }
        foreach (var c in components)
        {
            Assert.IsNotNull(c, "Null component found in components");
            if (c == null)
            {
                Debug.LogError("Null component detected");
            }
            c.selectEntered.AddListener(OnXRClick);
        }
    }

    // <inheritdoc/>
    private void OnXRClick(SelectEnterEventArgs args)
    {
        controllerInstance.OnButtonPressed();
    }
}
