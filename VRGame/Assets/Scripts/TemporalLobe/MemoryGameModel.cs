using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Stores data and answer-validation logic for the memory game sequence.
/// </summary>
/// <remarks>
/// Preconditions:
/// - `objects` must be configured before gameplay starts.
/// Postconditions:
/// - Game state changes only through `currentIndex` progression on correct answers.
/// </remarks>
public class MemoryGameModel : MonoBehaviour
{
    /// <summary>
    /// Ordered audio sequence used by the memory game.
    /// </summary>
    public AudioClip[] sounds;

    /// <summary>
    /// Ordered object sequence the player must select.
    /// </summary>
    public GameObject[] objects;

    /// <summary>
    /// Tracks the next expected object index in the sequence.
    /// </summary>
    public int currentIndex = 0;

    /// <summary>
    /// Validates the player's selected object against the current expected object.
    /// </summary>
    /// <param name="selectedObject">Object selected by the player.</param>
    /// <returns>
    /// `true` when the selection matches the expected object; otherwise `false`.
    /// </returns>
    /// <remarks>
    /// Preconditions:
    /// - `selectedObject` is non-null.
    /// - `objects` is non-null and contains at least one element.
    /// - `currentIndex` is within `objects` bounds.
    /// Postconditions:
    /// - If return value is `true`, `currentIndex` is incremented by 1.
    /// - If return value is `false`, `currentIndex` remains unchanged.
    /// </remarks>
    public bool CheckAnswer(GameObject selectedObject)
    {
        Assert.IsNotNull(selectedObject, "Selected object cannot be null.");
        Assert.IsNotNull(objects, "Objects sequence must be assigned before checking answers.");
        Assert.IsTrue(objects.Length > 0, "Objects sequence must contain at least one object.");
        Assert.IsTrue(currentIndex >= 0 && currentIndex < objects.Length, "Current index is out of bounds.");

        if (selectedObject == objects[currentIndex])
        {
            // Correct answer advances the expected sequence position.
            currentIndex++;
            return true;
        }

        return false;
    }
}
