using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controller for XR knob interaction - handles interaction logic
    /// Extracted from XRKnob.cs to separate controller logic from data (Model) and visuals (View)
    /// </summary>
    public class KnobController : XRBaseInteractable
    {
        const float k_ModeSwitchDeadZone = 0.1f;

        struct TrackedRotation
        {
            float m_BaseAngle;
            float m_CurrentOffset;
            float m_AccumulatedAngle;

            public float totalOffset => m_AccumulatedAngle + m_CurrentOffset;

            public void Reset()
            {
                m_BaseAngle = 0.0f;
                m_CurrentOffset = 0.0f;
                m_AccumulatedAngle = 0.0f;
            }

            public void SetBaseFromVector(Vector3 direction)
            {
                m_AccumulatedAngle += m_CurrentOffset;
                m_BaseAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                m_CurrentOffset = 0.0f;
            }

            public void SetTargetFromVector(Vector3 direction)
            {
                var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                m_CurrentOffset = ShortestAngleDistance(m_BaseAngle, targetAngle, 360.0f);

                if (Mathf.Abs(m_CurrentOffset) > 90.0f)
                {
                    m_BaseAngle = targetAngle;
                    m_AccumulatedAngle += m_CurrentOffset;
                    m_CurrentOffset = 0.0f;
                }
            }

            static float ShortestAngleDistance(float start, float end, float max)
            {
                var angleDelta = end - start;
                var angleSign = Mathf.Sign(angleDelta);
                angleDelta = Math.Abs(angleDelta) % max;
                
                if (angleDelta > (max * 0.5f))
                    angleDelta = -(max - angleDelta);

                return angleDelta * angleSign;
            }
        }

        [SerializeField, Tooltip("The model that stores knob state and settings")]
        KnobStateModel m_KnobModel;

        [SerializeField, Tooltip("The view that handles visual updates")]
        KnobVisualView m_KnobView;

        [SerializeField, Tooltip("The object that is visually grabbed and manipulated")]
        Transform m_Handle;

        IXRSelectInteractor m_Interactor;
        bool m_PositionDriven = false;
        bool m_UpVectorDriven = false;
        TrackedRotation m_PositionAngles = new TrackedRotation();
        TrackedRotation m_UpVectorAngles = new TrackedRotation();
        TrackedRotation m_ForwardVectorAngles = new TrackedRotation();
        float m_BaseKnobRotation = 0.0f;

        void Start()
        {
            if (m_KnobModel == null)
            {
                Debug.LogError("KnobController requires a KnobStateModel!", this);
                enabled = false;
                return;
            }

            if (m_KnobView != null)
            {
                m_KnobView.SetRotation(m_KnobModel.ValueToRotation());
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            selectEntered.AddListener(StartGrab);
            selectExited.AddListener(EndGrab);
        }

        protected override void OnDisable()
        {
            selectEntered.RemoveListener(StartGrab);
            selectExited.RemoveListener(EndGrab);
            base.OnDisable();
        }

        void StartGrab(SelectEnterEventArgs args)
        {
            m_Interactor = args.interactorObject;
            m_PositionAngles.Reset();
            m_UpVectorAngles.Reset();
            m_ForwardVectorAngles.Reset();
            UpdateBaseKnobRotation();
            UpdateRotation(true);
        }

        void EndGrab(SelectExitEventArgs args)
        {
            m_Interactor = null;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (isSelected)
                {
                    UpdateRotation();
                }
            }
        }

        public override Transform GetAttachTransform(IXRInteractor interactor)
        {
            return m_Handle;
        }

        void UpdateRotation(bool freshCheck = false)
        {
            if (m_Interactor == null)
                return;

            var interactorTransform = m_Interactor.GetAttachTransform(this);
            var localOffset = transform.InverseTransformVector(interactorTransform.position - m_Handle.position);
            localOffset.y = 0.0f;
            var radiusOffset = transform.TransformVector(localOffset).magnitude;
            localOffset.Normalize();

            var localForward = transform.InverseTransformDirection(interactorTransform.forward);
            var localY = Math.Abs(localForward.y);
            localForward.y = 0.0f;
            localForward.Normalize();

            var localUp = transform.InverseTransformDirection(interactorTransform.up);
            localUp.y = 0.0f;
            localUp.Normalize();

            if (m_PositionDriven && !freshCheck)
                radiusOffset *= (1.0f + k_ModeSwitchDeadZone);

            if (radiusOffset >= m_KnobModel.PositionTrackedRadius)
            {
                if (!m_PositionDriven || freshCheck)
                {
                    m_PositionAngles.SetBaseFromVector(localOffset);
                    m_PositionDriven = true;
                }
            }
            else
            {
                m_PositionDriven = false;
            }

            if (!freshCheck)
            {
                if (!m_UpVectorDriven)
                    localY *= (1.0f - (k_ModeSwitchDeadZone * 0.5f));
                else
                    localY *= (1.0f + (k_ModeSwitchDeadZone * 0.5f));
            }

            if (localY > 0.707f)
            {
                if (!m_UpVectorDriven || freshCheck)
                {
                    m_UpVectorAngles.SetBaseFromVector(localUp);
                    m_UpVectorDriven = true;
                }
            }
            else
            {
                if (m_UpVectorDriven || freshCheck)
                {
                    m_ForwardVectorAngles.SetBaseFromVector(localForward);
                    m_UpVectorDriven = false;
                }
            }

            if (m_PositionDriven)
                m_PositionAngles.SetTargetFromVector(localOffset);

            if (m_UpVectorDriven)
                m_UpVectorAngles.SetTargetFromVector(localUp);
            else
                m_ForwardVectorAngles.SetTargetFromVector(localForward);

            var knobRotation = m_BaseKnobRotation - 
                ((m_UpVectorAngles.totalOffset + m_ForwardVectorAngles.totalOffset) * m_KnobModel.TwistSensitivity) - 
                m_PositionAngles.totalOffset;

            if (m_KnobModel.ClampedMotion)
                knobRotation = Mathf.Clamp(knobRotation, m_KnobModel.MinAngle, m_KnobModel.MaxAngle);

            if (m_KnobView != null)
            {
                m_KnobView.SetRotation(m_KnobModel.GetSnappedAngle(knobRotation));
            }

            var knobValue = m_KnobModel.RotationToValue(knobRotation);
            m_KnobModel.Value = knobValue;
        }

        void UpdateBaseKnobRotation()
        {
            m_BaseKnobRotation = m_KnobModel.ValueToRotation();
        }

        void OnDrawGizmosSelected()
        {
            const int k_CircleSegments = 16;
            const float k_SegmentRatio = 1.0f / k_CircleSegments;

            if (m_KnobModel == null || m_KnobModel.PositionTrackedRadius <= Mathf.Epsilon)
                return;

            var knobTransform = transform;
            var circleCenter = m_Handle != null ? m_Handle.position : knobTransform.position;
            var circleX = knobTransform.right;
            var circleY = knobTransform.forward;

            Gizmos.color = Color.green;
            var segmentCounter = 0;
            while (segmentCounter < k_CircleSegments)
            {
                var startAngle = segmentCounter * k_SegmentRatio * 2.0f * Mathf.PI;
                segmentCounter++;
                var endAngle = segmentCounter * k_SegmentRatio * 2.0f * Mathf.PI;

                Gizmos.DrawLine(
                    circleCenter + (Mathf.Cos(startAngle) * circleX + Mathf.Sin(startAngle) * circleY) * m_KnobModel.PositionTrackedRadius,
                    circleCenter + (Mathf.Cos(endAngle) * circleX + Mathf.Sin(endAngle) * circleY) * m_KnobModel.PositionTrackedRadius);
            }
        }
    }
}
