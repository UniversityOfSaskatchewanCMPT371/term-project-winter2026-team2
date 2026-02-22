using System;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Data model for callout state - stores gaze and visibility state
    /// New model to track whether a callout is being gazed at and should be visible
    /// </summary>
    [Serializable]
    public class CalloutStateModel
    {
        bool m_IsGazing;
        bool m_IsVisible;
        float m_GazeDuration;
        float m_DwellTimeRequired;

        /// <summary>
        /// Event fired when gaze state changes
        /// </summary>
        public event Action<bool> OnGazeStateChanged;

        /// <summary>
        /// Event fired when visibility state changes
        /// </summary>
        public event Action<bool> OnVisibilityChanged;

        /// <summary>
        /// Whether the user is currently gazing at the callout
        /// </summary>
        public bool IsGazing
        {
            get => m_IsGazing;
            set
            {
                if (m_IsGazing == value)
                    return;

                m_IsGazing = value;
                OnGazeStateChanged?.Invoke(m_IsGazing);

                if (!m_IsGazing)
                {
                    m_GazeDuration = 0f;
                }
            }
        }

        /// <summary>
        /// Whether the callout is currently visible
        /// </summary>
        public bool IsVisible
        {
            get => m_IsVisible;
            set
            {
                if (m_IsVisible == value)
                    return;

                m_IsVisible = value;
                OnVisibilityChanged?.Invoke(m_IsVisible);
            }
        }

        /// <summary>
        /// How long the user has been gazing at the callout (in seconds)
        /// </summary>
        public float GazeDuration
        {
            get => m_GazeDuration;
            set => m_GazeDuration = value;
        }

        /// <summary>
        /// The required dwell time before showing the callout (in seconds)
        /// </summary>
        public float DwellTimeRequired
        {
            get => m_DwellTimeRequired;
            set => m_DwellTimeRequired = value;
        }

        /// <summary>
        /// Check if the dwell time threshold has been met
        /// </summary>
        public bool HasMetDwellThreshold()
        {
            return m_IsGazing && m_GazeDuration >= m_DwellTimeRequired;
        }

        /// <summary>
        /// Update the gaze duration (call this every frame when gazing)
        /// </summary>
        public void UpdateGazeDuration(float deltaTime)
        {
            if (m_IsGazing)
            {
                m_GazeDuration += deltaTime;
            }
        }

        /// <summary>
        /// Reset the state
        /// </summary>
        public void Reset()
        {
            m_IsGazing = false;
            m_IsVisible = false;
            m_GazeDuration = 0f;
        }

        /// <summary>
        /// Initialize with a required dwell time
        /// </summary>
        public CalloutStateModel(float dwellTimeRequired = 1f)
        {
            m_DwellTimeRequired = dwellTimeRequired;
            Reset();
        }
    }
}
