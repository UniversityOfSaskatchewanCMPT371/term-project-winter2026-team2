using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// View class for SpawnButton
/// Handles visual representation and interaction events for the spawn button
/// </summary>
public class SpawnButtonView :  View<ISpawnButtonController>, // TODO reminder to switch the generic to the one you've implemented
    ISpawnButtonView
{
    /// <summary>
    /// Reference to the XRSimpleInteractable component
    /// </summary>
    private XRSimpleInteractable interactable;

    /// <summary>
    /// Awake method to initialize the interactable component
    /// </summary>
    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        
        if (interactable == null)
        {
            Debug.LogWarning("XRSimpleInteractable component is missing (XR interactions will not work)");
        }
    }

    /// <inheritdoc/>
    public void Subscribe(System.Action<SelectEnterEventArgs> onButtonPressed)
    {
        if (onButtonPressed == null)
        {
            Debug.LogError("SpawnButtonView Cannot subscribe to events with null callback");
            Assert.IsNotNull(onButtonPressed, "onButtonPressed callback cannot be null");
            return;
        }

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(onButtonPressed.Invoke);
        }
        else
        {
            Debug.LogWarning("SpawnButtonView Cannot subscribe to XR events, interactable is null");
        }
    }

    /// <inheritdoc/>
    public void Unsubscribe(System.Action<SelectEnterEventArgs> onButtonPressed)
    {
        if (onButtonPressed == null)
        {
            Debug.LogError("SpawnButtonView Cannot unsubscribe from events with null callback");
            return;
        }

        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(onButtonPressed.Invoke);
        }
        else
        {
            Debug.LogWarning("SpawnButtonView Cannot unsubscribe from XR events, interactable is null");
        }
    }
}
