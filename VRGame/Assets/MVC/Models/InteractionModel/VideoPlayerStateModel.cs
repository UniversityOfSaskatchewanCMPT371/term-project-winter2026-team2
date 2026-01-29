using System;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Data model for video player state - stores playback state and time information
    /// Extracted from VideoTimeScrubControl.cs to separate data from UI control logic
    /// </summary>
    [Serializable]
    public class VideoPlayerStateModel
    {
        bool m_IsPlaying;
        bool m_IsDragging;
        bool m_VideoJumpPending;
        long m_CurrentFrame;
        long m_LastFrameBeforeScrub;
        float m_CurrentTime;
        float m_TotalLength;
        long m_FrameCount;

        /// <summary>
        /// Whether the video is currently playing
        /// </summary>
        public bool IsPlaying
        {
            get => m_IsPlaying;
            set => m_IsPlaying = value;
        }

        /// <summary>
        /// Whether the user is currently dragging the scrub slider
        /// </summary>
        public bool IsDragging
        {
            get => m_IsDragging;
            set => m_IsDragging = value;
        }

        /// <summary>
        /// Whether a video jump/seek operation is pending
        /// </summary>
        public bool VideoJumpPending
        {
            get => m_VideoJumpPending;
            set => m_VideoJumpPending = value;
        }

        /// <summary>
        /// The current frame of the video
        /// </summary>
        public long CurrentFrame
        {
            get => m_CurrentFrame;
            set => m_CurrentFrame = value;
        }

        /// <summary>
        /// The frame before a scrub operation started
        /// </summary>
        public long LastFrameBeforeScrub
        {
            get => m_LastFrameBeforeScrub;
            set => m_LastFrameBeforeScrub = value;
        }

        /// <summary>
        /// Current playback time in seconds
        /// </summary>
        public float CurrentTime
        {
            get => m_CurrentTime;
            set => m_CurrentTime = value;
        }

        /// <summary>
        /// Total video length in seconds
        /// </summary>
        public float TotalLength
        {
            get => m_TotalLength;
            set => m_TotalLength = value;
        }

        /// <summary>
        /// Total number of frames in the video
        /// </summary>
        public long FrameCount
        {
            get => m_FrameCount;
            set => m_FrameCount = value;
        }

        /// <summary>
        /// Get the current playback progress as a normalized value (0-1)
        /// </summary>
        public float GetNormalizedProgress()
        {
            if (m_FrameCount <= 0)
                return 0f;
            
            return (float)m_CurrentFrame / m_FrameCount;
        }

        /// <summary>
        /// Calculate the target frame from a normalized slider value (0-1)
        /// </summary>
        public long CalculateTargetFrame(float normalizedValue)
        {
            return (long)(m_FrameCount * normalizedValue);
        }

        /// <summary>
        /// Get formatted time string for current time
        /// </summary>
        public string GetFormattedCurrentTime()
        {
            var timeSpan = TimeSpan.FromSeconds(m_CurrentTime);
            return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }

        /// <summary>
        /// Get formatted time string for total length
        /// </summary>
        public string GetFormattedTotalTime()
        {
            var timeSpan = TimeSpan.FromSeconds(m_TotalLength);
            return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }

        /// <summary>
        /// Get combined time display string (current / total)
        /// </summary>
        public string GetFormattedTimeDisplay()
        {
            return $"{GetFormattedCurrentTime()} / {GetFormattedTotalTime()}";
        }

        /// <summary>
        /// Reset to initial state
        /// </summary>
        public void Reset()
        {
            m_IsPlaying = false;
            m_IsDragging = false;
            m_VideoJumpPending = false;
            m_CurrentFrame = 0;
            m_LastFrameBeforeScrub = long.MinValue;
            m_CurrentTime = 0f;
        }
    }
}
