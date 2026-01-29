using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Data model for knob state - stores value, angle constraints, and settings
    /// Extracted from XRKnob.cs to separate data from interaction logic
    /// </summary>
    [Serializable]
    public class KnobStateModel
    {
        [Serializable]
        public class ValueChangeEvent : UnityEvent<float> { }

        [SerializeField]
        [Tooltip("The value of the knob")]
        [Range(0.0f, 1.0f)]
        float m_Value = 0.5f;

        [SerializeField]
        [Tooltip("Whether this knob's rotation should be clamped by the angle limits")]
        bool m_ClampedMotion = true;

        [SerializeField]
        [Tooltip("Rotation of the knob at value '1'")]
        float m_MaxAngle = 90.0f;

        [SerializeField]
        [Tooltip("Rotation of the knob at value '0'")]
        float m_MinAngle = -90.0f;

        [SerializeField]
        [Tooltip("Angle increments to support, if greater than '0'")]
        float m_AngleIncrement = 0.0f;

        [SerializeField]
        [Tooltip("The position of the interactor controls rotation when outside this radius")]
        float m_PositionTrackedRadius = 0.1f;

        [SerializeField]
        [Tooltip("How much controller rotation affects the knob")]
        float m_TwistSensitivity = 1.5f;

        [SerializeField]
        [Tooltip("Events to trigger when the knob value changes")]
        ValueChangeEvent m_OnValueChange = new ValueChangeEvent();

        /// <summary>
        /// The current value of the knob (0-1)
        /// </summary>
        public float Value
        {
            get => m_Value;
            set
            {
                var newValue = value;
                
                if (m_ClampedMotion)
                    newValue = Mathf.Clamp01(newValue);

                if (m_AngleIncrement > 0)
                {
                    var angleRange = m_MaxAngle - m_MinAngle;
                    var angle = Mathf.Lerp(0.0f, angleRange, newValue);
                    angle = Mathf.Round(angle / m_AngleIncrement) * m_AngleIncrement;
                    newValue = Mathf.InverseLerp(0.0f, angleRange, angle);
                }

                if (Mathf.Approximately(m_Value, newValue))
                    return;

                m_Value = newValue;
                m_OnValueChange?.Invoke(m_Value);
            }
        }

        public bool ClampedMotion
        {
            get => m_ClampedMotion;
            set => m_ClampedMotion = value;
        }

        public float MaxAngle
        {
            get => m_MaxAngle;
            set => m_MaxAngle = value;
        }

        public float MinAngle
        {
            get => m_MinAngle;
            set => m_MinAngle = value;
        }

        public float AngleIncrement
        {
            get => m_AngleIncrement;
            set => m_AngleIncrement = value;
        }

        public float PositionTrackedRadius
        {
            get => m_PositionTrackedRadius;
            set => m_PositionTrackedRadius = value;
        }

        public float TwistSensitivity
        {
            get => m_TwistSensitivity;
            set => m_TwistSensitivity = value;
        }

        public ValueChangeEvent OnValueChange => m_OnValueChange;

        /// <summary>
        /// Convert the current value to a rotation angle
        /// </summary>
        public float ValueToRotation()
        {
            return m_ClampedMotion 
                ? Mathf.Lerp(m_MinAngle, m_MaxAngle, m_Value) 
                : Mathf.LerpUnclamped(m_MinAngle, m_MaxAngle, m_Value);
        }

        /// <summary>
        /// Convert a rotation angle to a value (0-1)
        /// </summary>
        public float RotationToValue(float rotation)
        {
            return (rotation - m_MinAngle) / (m_MaxAngle - m_MinAngle);
        }

        /// <summary>
        /// Get the snapped rotation angle based on angle increments
        /// </summary>
        public float GetSnappedAngle(float angle)
        {
            if (m_AngleIncrement > 0)
            {
                var normalizeAngle = angle - m_MinAngle;
                return (Mathf.Round(normalizeAngle / m_AngleIncrement) * m_AngleIncrement) + m_MinAngle;
            }
            return angle;
        }
    }
}
