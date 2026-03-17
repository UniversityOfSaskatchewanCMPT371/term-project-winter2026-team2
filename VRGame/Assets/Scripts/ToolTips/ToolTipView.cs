using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// View component that displays tooltip content (title and description) using TextMeshPro. 
/// Implements IToolTipView for testability.
/// </summary>
public class ToolTipView : MonoBehaviour, IToolTipView
{
    /// <summary>
    /// Text component for the title.
    /// </summary>
    /// <remarks>
    /// Must be assigned in the Unity Editor.
    /// </remarks>
    [SerializeField] private TextMeshProUGUI title;

    /// <summary>
    /// Text component for the description.
    /// </summary>
    /// <remarks>
    /// Must be assigned in the Unity Editor.
    /// </remarks>
    [SerializeField] private TextMeshProUGUI description;

    /// <summary>
    /// Data model containing the title and description to display in the tooltip.
     /// </summary>
     /// <remarks>
     /// Must be assigned in the Unity Editor. The tooltip will display the title and description from this model.
     /// </remarks>
    /// </summary>
    [SerializeField] private ToolTipModel data;

    
    /// <summary>
    /// <Initializes the view by populating text from the data model.
    /// </summary>
    /// <remarks>
    /// Preconditions:
    /// - `title`, `description, and `data` must be assign in the Unity Editor.
    /// Postconditions:
    /// - The title and description text are set to the values from `data`.
    /// - If any required reference is missing, an assertion fails in editor.
    /// </remarks>
    void Start()
    {
        Debug.Assert(title != null, "Title TextMeshProUGUI component is not assigned in the inspector.");
        Debug.Assert(description != null, "Description TextMeshProUGUI component is not assigned in the inspector.");
        Debug.Assert(data != null, "ToolTipModel data is not assigned in the inspector.");

        title.SetText(data.Title);
        description.SetText(data.Description);
    }

    /// <inheritdoc/>
    public void UpdateContent(IToolTipModel model)
    {
        // Validate input and component references before updating content
        if (model == null)
        {
            Debug.LogError("ToolTipModel cannot be null.");
            Debug.Assert(model != null, "ToolTipModel cannot be null.");
            return;
        }
        if (title == null || description == null)
        {
            Debug.LogError("UpdateContent called before UI components are initialized.");
            Debug.Assert(title != null, "Title component is null.");
            Debug.Assert(description != null, "Description component is null.");
            return;
        }
        title.SetText(model.Title);
        description.SetText(model.Description);
    }

    /// <inheritdoc/>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    
}
