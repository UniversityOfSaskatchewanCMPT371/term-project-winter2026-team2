using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Handles memory game audio playback for player feedback.
/// </summary>
/// <remarks>
/// Preconditions:
/// - `audioSource` and `controller` are assigned before runtime use.
/// Postconditions:
/// - Audio feedback is produced based on controller/model actions.
/// </remarks>
public class MemoryGameView : MonoBehaviour
{
    /// <summary>
    /// Audio output component used to play memory sequence clips.
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// Controller reference used to coordinate memory game flow.
    /// </summary>
    public MemoryGameController controller;

    /// <summary>
    /// Validates required component references for this view.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - Scene object has this view component initialized.
    /// Postconditions:
    /// - Required references are verified through assertions.
    /// </remarks>

    private void Awake()
    {

        if (audioSource == null)
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(audioSource, "AudioSource reference must be assigned on MemoryGameView.");
        Assert.IsNotNull(controller, "Controller reference must be assigned on MemoryGameView.");
    }

    /// <summary>
    /// Plays a memory-game sound clip through this view's audio source.
    /// </summary>
    /// <param name="clip">The clip to play.</param>
    /// <remarks>
    /// Preconditions:
    /// - `audioSource` is non-null.
    /// - `clip` is non-null.
    /// Postconditions:
    /// - `audioSource.clip` is set to `clip`.
    /// - Playback is started by calling `audioSource.Play()`.
    /// </remarks>
    public void PlaySound(AudioClip clip)
    {
        Assert.IsNotNull(audioSource, "AudioSource must be assigned before calling PlaySound.");
        Assert.IsNotNull(clip, "Audio clip cannot be null when calling PlaySound.");

        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// Plays a clip one time without replacing the current `audioSource.clip`.
    /// </summary>
    /// <param name="clip">The clip to play once.</param>
    /// <remarks>
    /// Preconditions:
    /// - `audioSource` is non-null.
    /// - `clip` is non-null.
    /// Postconditions:
    /// - A one-shot playback request is sent through `audioSource.PlayOneShot(clip)`.
    /// - `audioSource.clip` remains unchanged by this method.
    /// </remarks>
    public void PlayOneShot(AudioClip clip)
    {
        Assert.IsNotNull(audioSource, "AudioSource must be assigned before calling PlayOneShot.");
        Assert.IsNotNull(clip, "Audio clip cannot be null when calling PlayOneShot.");

        audioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Marks an object as correctly selected by changing its material color to green.
    /// </summary>
    /// <param name="obj">The selected object to mark as correct.</param>
    /// <remarks>
    /// Preconditions:
    /// - `obj` is non-null.
    /// - `obj` has a non-null `Renderer` component.
    /// - `Renderer.material` is non-null.
    /// Postconditions:
    /// - The object's material color is set to `Color.green`.
    /// </remarks>
    public void SetCorrect(GameObject obj)
    {
        Assert.IsNotNull(obj, "Object cannot be null when calling SetCorrect.");

        Renderer renderer = obj.GetComponent<Renderer>();
        Assert.IsNotNull(renderer, "SetCorrect requires the object to have a Renderer component.");
        Assert.IsNotNull(renderer.material, "Renderer material cannot be null in SetCorrect.");

        renderer.material.color = Color.green;
    }

    /// <summary>
    /// Marks an object as incorrectly selected by changing its material color to red.
    /// </summary>
    /// <param name="obj">The selected object to mark as wrong.</param>
    /// <remarks>
    /// Preconditions:
    /// - `obj` is non-null.
    /// - `obj` has a non-null `Renderer` component.
    /// - `Renderer.material` is non-null.
    /// Postconditions:
    /// - The object's material color is set to `Color.red`.
    /// </remarks>
    public void SetWrong(GameObject obj)
    {
        Assert.IsNotNull(obj, "Object cannot be null when calling SetWrong.");

        Renderer renderer = obj.GetComponent<Renderer>();
        Assert.IsNotNull(renderer, "SetWrong requires the object to have a Renderer component.");
        Assert.IsNotNull(renderer.material, "Renderer material cannot be null in SetWrong.");

        renderer.material.color = Color.red;
    }


    /// <summary>
    /// Processes per-frame input and forwards valid object selections to the controller.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `controller` is non-null.
    /// - A main camera exists when mouse-click selection is expected.
    /// Postconditions:
    /// - On left mouse click, a raycast is performed from the cursor position.
    /// - If a collider is hit, `controller.ObjectSelected` is invoked with the hit object.
    /// </remarks>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("MemoryObject"))
                {
                    controller.ObjectSelected(hit.collider.gameObject);
                    }
            }
        }
    }
}
