using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Coordinates memory game interactions between the view and model.
/// </summary>
/// <remarks>
/// Preconditions:
/// - Required references and sequences are assigned before runtime initialization.
/// Postconditions:
/// - Controller initializes the model and starts sequence playback flow.
/// </remarks>
public class MemoryGameController : MonoBehaviour
{
    /// <summary>
    /// View reference used for UI/audio feedback and input forwarding.
    /// </summary>
    public MemoryGameView view;

    /// <summary>
    /// Ordered sound sequence for the memory game.
    /// </summary>
    public AudioClip[] sounds;

    /// <summary>
    /// Ordered object sequence that corresponds to expected player selections.
    /// </summary>
    public GameObject[] objects;

    /// <summary>
    /// Sound played when the player selects the correct object.
    /// </summary>
    public AudioClip correctSound;

    /// <summary>
    /// Sound played when the player selects the wrong object.
    /// </summary>
    public AudioClip wrongSound;

    /// <summary>
    /// Sound played when the game sequence is successfully completed.
    /// </summary>
    public AudioClip winSound;

    /// <summary>
    /// Model containing sequence state and answer validation logic.
    /// </summary>
    private MemoryGameModel model;

    /// <summary>
    /// Initializes model data and starts the sequence playback flow.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `view` is non-null.
    /// - `sounds` and `objects` are non-null and non-empty.
    /// Postconditions:
    /// - `model` is initialized and receives `sounds`/`objects` references.
    /// - `PlayCurrentSound()` is invoked to begin or continue sequence playback.
    /// </remarks>
    void Start()
    {
        Assert.IsNotNull(view, "MemoryGameView reference must be assigned on MemoryGameController.");
        Assert.IsNotNull(sounds, "Sounds sequence must be assigned on MemoryGameController.");
        Assert.IsTrue(sounds.Length > 0, "Sounds sequence must contain at least one clip.");
        Assert.IsNotNull(objects, "Objects sequence must be assigned on MemoryGameController.");
        Assert.IsTrue(objects.Length > 0, "Objects sequence must contain at least one object.");

        model = new MemoryGameModel();

        model.sounds = sounds;
        model.objects = objects;

        PlayCurrentSound();
    }

    /// <summary>
    /// Plays the sound cue at the model's current sequence index.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `view` and `model` are non-null.
    /// - `model.sounds` is non-null and non-empty.
    /// - `model.currentIndex` is within `model.sounds` bounds.
    /// Postconditions:
    /// - `view.PlaySound(...)` is called with the current sequence clip.
    /// </remarks>
    void PlayCurrentSound()
    {
        Assert.IsNotNull(view, "View must be assigned before calling PlayCurrentSound.");
        Assert.IsNotNull(model, "Model must be initialized before calling PlayCurrentSound.");
        Assert.IsNotNull(model.sounds, "Model sounds must be assigned before calling PlayCurrentSound.");
        Assert.IsTrue(model.sounds.Length > 0, "Model sounds must contain at least one clip.");
        Assert.IsTrue(model.currentIndex >= 0 && model.currentIndex < model.sounds.Length, "Current index is out of bounds for model sounds.");

        view.PlaySound(model.sounds[model.currentIndex]);
    }
    /// <summary>
    /// Handles a player-selected object and updates memory-game feedback/state.
    /// </summary>
    /// <param name="selectedObject">The object selected by the player.</param>
    /// <remarks>
    /// Preconditions:
    /// - `model`, `view`, and `selectedObject` are non-null.
    /// - Feedback clips (`correctSound`, `wrongSound`, `winSound`) are assigned.
    /// Postconditions:
    /// - Correct selections are marked green and play correct feedback.
    /// - Wrong selections are marked red and play wrong feedback.
    /// - If the game is completed, win feedback is played; otherwise the next cue is played.
    /// </remarks>
    public void ObjectSelected(GameObject selectedObject)
    {
        Assert.IsNotNull(model, "Model must be initialized before handling object selection.");
        Assert.IsNotNull(view, "View must be assigned before handling object selection.");
        Assert.IsNotNull(selectedObject, "Selected object cannot be null.");
        Assert.IsNotNull(correctSound, "Correct feedback sound must be assigned.");
        Assert.IsNotNull(wrongSound, "Wrong feedback sound must be assigned.");
        Assert.IsNotNull(winSound, "Win feedback sound must be assigned.");

        bool correct = model.CheckAnswer(selectedObject);

        if (correct)
        {
            view.SetCorrect(selectedObject);
            view.PlayOneShot(correctSound);

            if (!model.IsGameComplete())
            {
                PlayCurrentSound();
            }
            else
            {
                view.PlayOneShot(winSound);
                Debug.Log("Memory Restored!");
            }
        }
        else
        {
            view.SetWrong(selectedObject);
            view.PlayOneShot(wrongSound);
        }
    }
}
