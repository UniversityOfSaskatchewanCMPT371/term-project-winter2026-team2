using TMPro;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// View component for step/tutorial manager - handles visual updates only
    /// Extracted from StepManager.cs to separate view logic from controller logic
    /// </summary>
    public class StepManagerView : MonoBehaviour
    {
        [SerializeField, Tooltip("Text field that displays the button text for the current step")]
        TextMeshProUGUI m_StepButtonTextField;

        /// <summary>
        /// Activate a specific step GameObject and deactivate all others
        /// </summary>
        public void ShowStep(GameObject stepObject, GameObject[] allSteps)
        {
            // Deactivate all steps
            foreach (var step in allSteps)
            {
                if (step != null)
                    step.SetActive(false);
            }

            // Activate the current step
            if (stepObject != null)
                stepObject.SetActive(true);
        }

        /// <summary>
        /// Update the button text field
        /// </summary>
        public void UpdateButtonText(string buttonText)
        {
            if (m_StepButtonTextField != null)
            {
                m_StepButtonTextField.text = buttonText;
            }
        }

        /// <summary>
        /// Hide all steps
        /// </summary>
        public void HideAllSteps(GameObject[] allSteps)
        {
            foreach (var step in allSteps)
            {
                if (step != null)
                    step.SetActive(false);
            }
        }

        /// <summary>
        /// Set the text field component
        /// </summary>
        public void SetTextField(TextMeshProUGUI textField)
        {
            m_StepButtonTextField = textField;
        }
    }
}
