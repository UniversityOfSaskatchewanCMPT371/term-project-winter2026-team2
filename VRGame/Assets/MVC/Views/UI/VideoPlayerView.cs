using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.VRTemplate
{
    /// <summary>
    /// View component for video player UI - handles visual updates only
    /// Extracted from VideoTimeScrubControl.cs to separate view logic from controller logic
    /// </summary>
    public class VideoPlayerView : MonoBehaviour
    {
        [SerializeField, Tooltip("Play/pause button GameObject")]
        GameObject m_ButtonPlayOrPause;

        [SerializeField, Tooltip("Slider that displays video progress")]
        Slider m_Slider;

        [SerializeField, Tooltip("Play icon sprite")]
        Sprite m_IconPlay;

        [SerializeField, Tooltip("Pause icon sprite")]
        Sprite m_IconPause;

        [SerializeField, Tooltip("Play or pause button image.")]
        Image m_ButtonPlayOrPauseIcon;

        [SerializeField, Tooltip("Text that displays the current time of the video.")]
        TextMeshProUGUI m_VideoTimeText;

        void Start()
        {
            if (m_ButtonPlayOrPause != null)
                m_ButtonPlayOrPause.SetActive(false);
        }

        /// <summary>
        /// Update the slider value (0-1 normalized progress)
        /// </summary>
        public void UpdateSliderValue(float normalizedProgress)
        {
            if (m_Slider != null)
            {
                m_Slider.value = normalizedProgress;
            }
        }

        /// <summary>
        /// Update the time text display
        /// </summary>
        public void UpdateTimeText(string timeString)
        {
            if (m_VideoTimeText != null)
            {
                m_VideoTimeText.SetText(timeString);
            }
        }

        /// <summary>
        /// Show the play button visuals
        /// </summary>
        public void ShowPlayButton()
        {
            if (m_ButtonPlayOrPauseIcon != null)
            {
                m_ButtonPlayOrPauseIcon.sprite = m_IconPlay;
            }

            if (m_ButtonPlayOrPause != null)
            {
                m_ButtonPlayOrPause.SetActive(true);
            }
        }

        /// <summary>
        /// Show the pause button visuals
        /// </summary>
        public void ShowPauseButton()
        {
            if (m_ButtonPlayOrPauseIcon != null)
            {
                m_ButtonPlayOrPauseIcon.sprite = m_IconPause;
            }

            if (m_ButtonPlayOrPause != null)
            {
                m_ButtonPlayOrPause.SetActive(false);
            }
        }

        /// <summary>
        /// Get the slider component (for controller to add listeners)
        /// </summary>
        public Slider Slider => m_Slider;

        /// <summary>
        /// Get the slider's current value
        /// </summary>
        public float GetSliderValue()
        {
            return m_Slider != null ? m_Slider.value : 0f;
        }

        /// <summary>
        /// Enable or disable the slider
        /// </summary>
        public void SetSliderEnabled(bool enabled)
        {
            if (m_Slider != null)
            {
                m_Slider.interactable = enabled;
            }
        }
    }
}
