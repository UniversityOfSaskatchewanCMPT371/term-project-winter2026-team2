using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// View component for knob visuals - handles rotating the visual handle
    /// Extracted from XRKnob.cs to separate visual updates from interaction logic
    /// </summary>
    public class KnobVisualView : MonoBehaviour
    {
        [SerializeField, Tooltip("The transform that will be rotated to show knob position")]
        Transform m_HandleTransform;

        /// <summary>
        /// Set the visual rotation of the knob handle
        /// </summary>
        public void SetRotation(float angle)
        {
            if (m_HandleTransform != null)
            {
                m_HandleTransform.localEulerAngles = new Vector3(0.0f, angle, 0.0f);
            }
        }

        /// <summary>
        /// Get the current visual rotation
        /// </summary>
        public float GetRotation()
        {
            if (m_HandleTransform != null)
            {
                return m_HandleTransform.localEulerAngles.y;
            }
            return 0f;
        }

        /// <summary>
        /// Set the handle transform reference
        /// </summary>
        public void SetHandleTransform(Transform handleTransform)
        {
            m_HandleTransform = handleTransform;
        }

        /// <summary>
        /// Smoothly interpolate to a target rotation
        /// </summary>
        public void SmoothRotateTo(float targetAngle, float speed)
        {
            if (m_HandleTransform != null)
            {
                var currentEuler = m_HandleTransform.localEulerAngles;
                var newY = Mathf.LerpAngle(currentEuler.y, targetAngle, Time.deltaTime * speed);
                m_HandleTransform.localEulerAngles = new Vector3(currentEuler.x, newY, currentEuler.z);
            }
        }
    }
}
