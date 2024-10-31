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

    private Coroutine hand1, hand2;

    public void SetHand1()
    {
        hand1Detected = true;

        if (hand1 != null)
            StopCoroutine(hand1);

        hand1 = StartCoroutine(ResetHandAfterTime(() => hand1Detected = false));

        CheckHands();
    }

    public void SetHand2()
    {
        hand2Detected = true;

        if (hand2 != null)
            StopCoroutine(hand2);
        
        hand2 = StartCoroutine(ResetHandAfterTime(() => hand2Detected = false));
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
