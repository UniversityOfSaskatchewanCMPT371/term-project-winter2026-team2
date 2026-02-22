using System;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Data model for toggle state - stores the on/off state of a boolean toggle
    /// New model to separate state data from visual controller logic
    /// </summary>
    [Serializable]
    public class ToggleStateModel
    {
        [SerializeField]
        bool m_IsOn;

        /// <summary>
        /// Event fired when the toggle state changes
        /// </summary>
        public event Action<bool> OnStateChanged;

        /// <summary>
        /// Whether the toggle is currently on
        /// </summary>
        public bool IsOn
        {
            get => m_IsOn;
            set
            {
                if (m_IsOn == value)
                    return;

                m_IsOn = value;
                OnStateChanged?.Invoke(m_IsOn);
            }
        }

        /// <summary>
        /// Toggle the state
        /// </summary>
        public void Toggle()
        {
            IsOn = !IsOn;
        }

        /// <summary>
        /// Initialize the model with a starting state
        /// </summary>
        public ToggleStateModel(bool initialState = false)
        {
            m_IsOn = initialState;
        }
    }
}
