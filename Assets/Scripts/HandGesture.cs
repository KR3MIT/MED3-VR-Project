using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Hands.Gestures;

namespace UnityEngine.XR.Hands.Samples.GestureSample
{
    /// <summary>
    /// A gesture that detects when a hand is held in a static shape and orientation for a minimum amount of time.
    /// </summary>
    public class HandGesture : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The hand tracking events component to subscribe to receive updated joint data to be used for gesture detection.")]
        XRHandTrackingEvents m_HandTrackingEvents;

        [SerializeField]
        [Tooltip("The hand shape or pose that must be detected for the gesture to be performed.")]
        ScriptableObject m_HandShapeOrPose;

        [SerializeField]
        [Tooltip("The target Transform to user for target conditions in the hand shape or pose.")]
        Transform m_TargetTransform;

        [SerializeField]
        [Tooltip("The event fired when the gesture is performed.")]
        UnityEvent m_GesturePerformed;

        [SerializeField]
        [Tooltip("The event fired when the gesture is ended.")]
        UnityEvent m_GestureEnded;

        [SerializeField]
        [Tooltip("The minimum amount of time the hand must be held in the required shape and orientation for the gesture to be performed.")]
        float m_MinimumHoldTime = 0.2f;

        [SerializeField]
        [Tooltip("The interval at which the gesture detection is performed.")]
        float m_GestureDetectionInterval = 0.1f;

        [Header("Second Hand Gesture")]
        [SerializeField]
        ScriptableObject m_SecondHandShapeOrPose;

        [SerializeField]
        float m_SecondGestureTimeWindow = 2.0f;

        [SerializeField]
        bool m_UseTwoPartDetection = false;


        XRHandShape m_HandShape;
        XRHandPose m_HandPose;
        XRHandShape m_SecondHandShape;//2
        XRHandPose m_SecondHandPose;//2
        bool m_WasDetected;
        bool m_PerformedTriggered;
        bool m_WaitingForSecondGesture;//2
        float m_TimeOfLastConditionCheck;
        float m_HoldStartTime;
        float m_FirstGesturePerformedTime;//2

        /// <summary>
        /// The hand tracking events component to subscribe to receive updated joint data to be used for gesture detection.
        /// </summary>
        public XRHandTrackingEvents handTrackingEvents
        {
            get => m_HandTrackingEvents;
            set => m_HandTrackingEvents = value;
        }

        /// <summary>
        /// The hand shape or pose that must be detected for the gesture to be performed.
        /// </summary>
        public ScriptableObject handShapeOrPose
        {
            get => m_HandShapeOrPose;
            set => m_HandShapeOrPose = value;
        }

        /// <summary>
        /// The target Transform to user for target conditions in the hand shape or pose.
        /// </summary>
        public Transform targetTransform
        {
            get => m_TargetTransform;
            set => m_TargetTransform = value;
        }

        /// <summary>
        /// The event fired when the gesture is performed.
        /// </summary>
        public UnityEvent gesturePerformed
        {
            get => m_GesturePerformed;
            set => m_GesturePerformed = value;
        }

        /// <summary>
        /// The event fired when the gesture is ended.
        /// </summary>
        public UnityEvent gestureEnded
        {
            get => m_GestureEnded;
            set => m_GestureEnded = value;
        }

        /// <summary>
        /// The minimum amount of time the hand must be held in the required shape and orientation for the gesture to be performed.
        /// </summary>
        public float minimumHoldTime
        {
            get => m_MinimumHoldTime;
            set => m_MinimumHoldTime = value;
        }

        /// <summary>
        /// The interval at which the gesture detection is performed.
        /// </summary>
        public float gestureDetectionInterval
        {
            get => m_GestureDetectionInterval;
            set => m_GestureDetectionInterval = value;
        }

        void OnEnable()
        {
            m_HandTrackingEvents.jointsUpdated.AddListener(OnJointsUpdated);

            m_HandShape = m_HandShapeOrPose as XRHandShape;
            m_HandPose = m_HandShapeOrPose as XRHandPose;
            m_SecondHandShape = m_SecondHandShapeOrPose as XRHandShape;
            m_SecondHandPose = m_SecondHandShapeOrPose as XRHandPose;

            if (m_HandPose != null && m_HandPose.relativeOrientation != null)
                m_HandPose.relativeOrientation.targetTransform = m_TargetTransform;

            if (m_SecondHandPose != null && m_SecondHandPose.relativeOrientation != null)
                m_SecondHandPose.relativeOrientation.targetTransform = m_TargetTransform;
        }

        void OnDisable() => m_HandTrackingEvents.jointsUpdated.RemoveListener(OnJointsUpdated);
        
        void OnJointsUpdated(XRHandJointsUpdatedEventArgs eventArgs)
        {
            if (!isActiveAndEnabled || Time.timeSinceLevelLoad < m_TimeOfLastConditionCheck + m_GestureDetectionInterval)
                return;

            var detected =
                m_HandTrackingEvents.handIsTracked &&
                m_HandShape != null && m_HandShape.CheckConditions(eventArgs) ||
                m_HandPose != null && m_HandPose.CheckConditions(eventArgs);

            if (!m_WasDetected && detected)
            {
                m_HoldStartTime = Time.timeSinceLevelLoad;
            }
            else if (m_WasDetected && !detected)
            {
                m_PerformedTriggered = false;
                m_GestureEnded?.Invoke();
            }

            m_WasDetected = detected;

            if (!m_PerformedTriggered && detected)
            {
                var holdTimer = Time.timeSinceLevelLoad - m_HoldStartTime;
                if (holdTimer > m_MinimumHoldTime)
                {
                    if (m_UseTwoPartDetection)
                    {
                        m_FirstGesturePerformedTime = Time.timeSinceLevelLoad;
                        m_PerformedTriggered = true;
                        m_WaitingForSecondGesture = true;
                    }
                    else
                    {
                        m_GesturePerformed?.Invoke();
                        m_PerformedTriggered = true;
                    }
                }
            }


            //second gesture detection
            if (m_WaitingForSecondGesture)
            {
                var secondGestureDetected =
                    m_HandTrackingEvents.handIsTracked &&
                    m_SecondHandShape != null && m_SecondHandShape.CheckConditions(eventArgs) ||
                    m_SecondHandPose != null && m_SecondHandPose.CheckConditions(eventArgs);

                if (secondGestureDetected && Time.timeSinceLevelLoad - m_FirstGesturePerformedTime <= m_SecondGestureTimeWindow)
                {
                    m_GesturePerformed?.Invoke();
                    m_WaitingForSecondGesture = false;
                }
                else if (!secondGestureDetected && m_PerformedTriggered)
                {
                    m_GestureEnded?.Invoke();
                    m_PerformedTriggered = false;
                    m_WaitingForSecondGesture = false;
                }
                else if (Time.timeSinceLevelLoad - m_FirstGesturePerformedTime > m_SecondGestureTimeWindow)
                {
                    m_WaitingForSecondGesture = false;
                    m_GestureEnded?.Invoke();
                }
            }

            m_TimeOfLastConditionCheck = Time.timeSinceLevelLoad;
        }
    }
}
