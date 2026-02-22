using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controller for step/tutorial management - handles step progression logic
    /// Extracted from StepManager.cs to separate controller logic from view
    /// </summary>
    public class StepController : MonoBehaviour
    {
        [SerializeField, Tooltip("The model that stores step state")]
        StepStateModel m_StateModel;

        [SerializeField, Tooltip("The view that handles visual updates")]
        StepManagerView m_ManagerView;

        void Start()
        {
            if (m_StateModel == null)
            {
                Debug.LogError("StepController requires a StepStateModel!", this);
                enabled = false;
                return;
            }

            // Subscribe to state changes
            m_StateModel.OnStepChanged += OnStepChanged;

            // Initialize first step
            if (m_StateModel.StepCount > 0)
            {
                ShowCurrentStep();
            }
        }

        void OnDestroy()
        {
            if (m_StateModel != null)
            {
                m_StateModel.OnStepChanged -= OnStepChanged;
            }
        }

        /// <summary>
        /// Advance to the next step
        /// </summary>
        public void NextStep()
        {
            if (m_StateModel == null)
                return;

            m_StateModel.NextStep();
        }

        /// <summary>
        /// Go to a specific step by index
        /// </summary>
        public void GoToStep(int index)
        {
            if (m_StateModel == null)
                return;

            m_StateModel.GoToStep(index);
        }

        /// <summary>
        /// Reset to the first step
        /// </summary>
        public void ResetSteps()
        {
            if (m_StateModel == null)
                return;

            m_StateModel.Reset();
        }

        void OnStepChanged(int stepIndex, StepStateModel.StepData stepData)
        {
            ShowCurrentStep();
        }

        void ShowCurrentStep()
        {
            if (m_ManagerView == null || m_StateModel == null)
                return;

            var currentStep = m_StateModel.GetCurrentStep();
            if (currentStep == null)
                return;

            // Get all step GameObjects
            var allStepObjects = m_StateModel.Steps
                .Where(s => s != null && s.stepObject != null)
                .Select(s => s.stepObject)
                .ToArray();

            // Update view
            m_ManagerView.ShowStep(currentStep.stepObject, allStepObjects);
            m_ManagerView.UpdateButtonText(currentStep.buttonText);
        }

        /// <summary>
        /// Set the state model (useful for runtime configuration)
        /// </summary>
        public void SetStateModel(StepStateModel stateModel)
        {
            if (m_StateModel != null)
            {
                m_StateModel.OnStepChanged -= OnStepChanged;
            }

            m_StateModel = stateModel;

            if (m_StateModel != null)
            {
                m_StateModel.OnStepChanged += OnStepChanged;
                ShowCurrentStep();
            }
        }

        /// <summary>
        /// Set the view (useful for runtime configuration)
        /// </summary>
        public void SetView(StepManagerView view)
        {
            m_ManagerView = view;
            ShowCurrentStep();
        }
    }
}
