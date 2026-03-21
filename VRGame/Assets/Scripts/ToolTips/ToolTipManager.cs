using UnityEngine;

/// <summary>
/// Ensures only one tooltip is visible at a time by managing show/hide requests.
/// </summary>
public static class ToolTipManager
{
    private static GameObject activeTooltip;

    public static void ShowToolTip(GameObject tooltip)
    {
        if (tooltip == null)
        {
            Debug.LogError("ToolTipManager.ShowToolTip called with null tooltip.");
            Debug.Assert(tooltip != null, "tooltip cannot be null.");
        }

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

    public static void HideToolTip(GameObject tooltip)
    {
        if (tooltip == null)
        {
            Debug.LogError("ToolTipManager.HideToolTip called with null tooltip.");
            Debug.Assert(tooltip != null, "tooltip cannot be null.");
        }

        // Only hide if this is the active tooltip
        if (activeTooltip == tooltip)
        {
            tooltip.SetActive(false);
            Debug.Assert(!tooltip.activeSelf, "Tooltip should be inactive after HideToolTip.");
            activeTooltip = null;
        }
    }
}