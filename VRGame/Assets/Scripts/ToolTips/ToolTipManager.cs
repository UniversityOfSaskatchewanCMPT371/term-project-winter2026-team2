using UnityEngine;

/// <summary>
/// Ensures only one tooltip is visible at a time by managing show/hide requests.
/// </summary>
public static class ToolTipManager
{
    private static GameObject activeTooltip;

    /// <summary>
    /// Shows a tooltip. If another tooltip is already visible, it hides that one first.
    /// </summary>
    /// <param name="tooltip">The tooltip GameObject to show. Must not be null.</param>
    /// <remarks>
    /// Preconditions:
    /// - <c>tooltip</c> must not be null.
    /// Postconditions:
    /// - Your tooltip becomes the active one and is made visible.
    /// - Any other tooltip that was visible gets hidden.
    /// </remarks>
    public static void ShowToolTip(GameObject tooltip)
    {
        if (tooltip == null)
        {
            Debug.LogError("ToolTipManager.ShowToolTip called with null tooltip.");
        }
        Debug.Assert(tooltip != null, "tooltip cannot be null.");

        // Hide any previous active tooltip
        if (activeTooltip != null)
        {
            activeTooltip.SetActive(false);
        }

        // Show the new tooltip
        tooltip.SetActive(true);
        Debug.Assert(tooltip.activeSelf, "Tooltip should be active after ShowToolTip.");
        activeTooltip = tooltip;
    }

    /// <summary>
    /// Hides a tooltip, but only if it's the one that's currently visible.
    /// </summary>
    /// <param name="tooltip">The tooltip GameObject to hide. Must not be null.</param>
    /// <remarks>
    /// Preconditions:
    /// - <c>tooltip</c> must not be null.
    /// Postconditions:
    /// - If tooltip is currently showing, it gets hidden.
    /// </remarks>
    public static void HideToolTip(GameObject tooltip)
    {
        if (tooltip == null)
        {
            Debug.LogError("ToolTipManager.HideToolTip called with null tooltip.");
        }
        Debug.Assert(tooltip != null, "tooltip cannot be null.");

        // Only hide if this is the active tooltip
        if (activeTooltip == tooltip)
        {
            tooltip.SetActive(false);
            Debug.Assert(!tooltip.activeSelf, "Tooltip should be inactive after HideToolTip.");
            activeTooltip = null;
        }
    }
}