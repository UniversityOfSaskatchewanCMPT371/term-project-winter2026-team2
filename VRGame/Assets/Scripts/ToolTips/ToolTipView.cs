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
    public TextMeshProUGUI title;

    /// <summary>
    /// Text component for the description.
    /// </summary>
    /// <remarks>
    /// Must be assigned in the Unity Editor.
    /// </remarks>
    public TextMeshProUGUI description;

    /// <summary>
    /// Data model containing the title and description to display in the tooltip.
     /// </summary>
     /// <remarks>
     /// Must be assigned in the Unity Editor. The tooltip will display the title and description from this model.
     /// </remarks>
    /// </summary>
    public ToolTipModel data;

    
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
        Assert.IsNotNull(title, "Title TextMeshProUGUI component is not assigned in the inspector.");
        Assert.IsNotNull(description, "Description TextMeshProUGUI component is not assigned in the inspector.");
        Assert.IsNotNull(data, "ToolTipModel data is not assigned in the inspector.");
        
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
            Assert.IsNotNull(model, "ToolTipModel cannot be null.");
            return;
        }
        if (title == null || description == null)
        {
            Debug.LogError("UpdateContent called before UI components are initialized.");
            Assert.IsNotNull(title, "Title component is null.");
            Assert.IsNotNull(description, "Description component is null.");
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
