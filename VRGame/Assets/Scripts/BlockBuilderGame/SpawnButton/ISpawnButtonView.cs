using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Interface for the SpawnButton View
/// Handles visual representation and interaction events for the spawn button
/// </summary>
public interface ISpawnButtonView
{
    /// <summary>
    /// Subscribes to button interaction events
    /// </summary>
    /// <param name="onButtonPressed">Callback to invoke when button is pressed</param>
    /// <remarks>
    /// pre-condition:
    ///     - onButtonPressed callback is not null
    ///     - XRSimpleInteractable component exists on the GameObject
    /// post-condition:
    ///     - Button press events are subscribed and will trigger the callback
    /// </remarks>
    void Subscribe(System.Action<SelectEnterEventArgs> onButtonPressed);

    /// <summary>
    /// Unsubscribes from button interaction events
    /// </summary>
    /// <param name="onButtonPressed">Callback to remove from button press events</param>
    /// <remarks>
    /// pre-condition:
    ///     - onButtonPressed callback was previously subscribed
    /// post-condition:
    ///     - Button press events are unsubscribed and will no longer trigger the callback
    /// </remarks>
    void Unsubscribe(System.Action<SelectEnterEventArgs> onButtonPressed);
}
