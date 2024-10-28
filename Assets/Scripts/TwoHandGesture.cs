using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// One of this script to one double gesture, works by using sethand1 and 2 from the gestures fired events
/// </summary>
public class TwoHandGesture : MonoBehaviour
{
    [Tooltip("Time in seconds that a hand is considered active after being detected.")]
    public float handActiveTime = 2.0f;
    [Tooltip("Time in seconds that a double gesture is active before end event is activvated")]
    public float doubleGestureTimeWindow = 2.0f;

    bool hand1Detected = false;
    bool hand2Detected = false;

    public UnityEvent BothHandsDetected;
    public UnityEvent BothHandsEnded;

    public void SetHand1()
    {
        hand1Detected = true;
        StartCoroutine(ResetHandAfterTime(() => hand1Detected = false));

        CheckHands();
    }

    public void SetHand2()
    {
        hand2Detected = true;
        StartCoroutine(ResetHandAfterTime(() => hand2Detected = false));
        CheckHands();
    }

    private void CheckHands()
    {
        if (hand1Detected && hand2Detected)
        {
            BothHandsDetected.Invoke();
            Invoke("EndEvent", doubleGestureTimeWindow);
        }
    }

    private void EndEvent()
    {
        BothHandsEnded.Invoke();
    }

    private IEnumerator ResetHandAfterTime(System.Action resetAction)
    {
        yield return new WaitForSeconds(handActiveTime);
        resetAction();
    }
}
