using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.GestureSample;

public class samarbejdeFix : MonoBehaviour
{
    public AICompanion companion1;
    public AICompanion companion2;

    public HandGesture handGesture;

    public void switchCompanion()
    {
        companion1.enabled = !companion1.enabled;
        companion2.enabled = !companion2.enabled;
    }

    public void fixCompanion()
    {
        companion1.StopAllCoroutines();
        companion1.carryingObject = null;
        companion1.currentTypes = new List<ObjectInfo.ObjectType>();
        companion1.canDefine = true;
        companion1.actionRunning = false;
        companion1.noPath = false;
        companion1.state = AICompanion.State.Idle;

    }

    public void fixHandGesture()
    {
        handGesture.minimumHoldTime = 0.1f;
        handGesture.gestureDetectionInterval = 0.05f;
    }
}
