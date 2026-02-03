using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Data model for step/tutorial state - stores current step and step configuration
    /// Extracted from StepManager.cs to separate data from UI control logic
    /// </summary>
    [Serializable]
    public class StepStateModel
    {
        [Serializable]
        public class StepData
        {
            [SerializeField]
            public GameObject stepObject;
            
            [SerializeField]
            public string buttonText;

            public StepData(GameObject obj, string text)
            {
                stepObject = obj;
                buttonText = text;
            }
        }

        [SerializeField]
        List<StepData> m_Steps = new List<StepData>();

        int m_CurrentStepIndex = 0;

        /// <summary>
        /// Event fired when the current step changes
        /// </summary>
        public event Action<int, StepData> OnStepChanged;

        /// <summary>
        /// The index of the current step
        /// </summary>
        public int CurrentStepIndex
        {
            get => m_CurrentStepIndex;
            private set
            {
                if (m_CurrentStepIndex == value)
                    return;

                m_CurrentStepIndex = value;
                OnStepChanged?.Invoke(m_CurrentStepIndex, GetCurrentStep());
            }
        }

        /// <summary>
        /// Total number of steps
        /// </summary>
        public int StepCount => m_Steps.Count;

        /// <summary>
        /// List of all steps
        /// </summary>
        public List<StepData> Steps
        {
            get => m_Steps;
            set => m_Steps = value;
        }

        /// <summary>
        /// Get the current step data
        /// </summary>
        public StepData GetCurrentStep()
        {
            if (m_CurrentStepIndex >= 0 && m_CurrentStepIndex < m_Steps.Count)
                return m_Steps[m_CurrentStepIndex];
            
            return null;
        }

        /// <summary>
        /// Advance to the next step (wraps around to beginning)
        /// </summary>
        public void NextStep()
        {
            if (m_Steps.Count == 0)
                return;

            CurrentStepIndex = (m_CurrentStepIndex + 1) % m_Steps.Count;
        }

        /// <summary>
        /// Go to a specific step by index
        /// </summary>
        public void GoToStep(int index)
        {
            if (index < 0 || index >= m_Steps.Count)
            {
                Debug.LogWarning($"Invalid step index: {index}. Valid range is 0-{m_Steps.Count - 1}");
                return;
            }

            CurrentStepIndex = index;
        }

        /// <summary>
        /// Reset to the first step
        /// </summary>
        public void Reset()
        {
            CurrentStepIndex = 0;
        }

        /// <summary>
        /// Check if there is a next step available
        /// </summary>
        public bool HasNextStep()
        {
            return m_Steps.Count > 0;
        }

        /// <summary>
        /// Get the button text for the current step
        /// </summary>
        public string GetCurrentButtonText()
        {
            var currentStep = GetCurrentStep();
            return currentStep?.buttonText ?? string.Empty;
        }
    }
}
