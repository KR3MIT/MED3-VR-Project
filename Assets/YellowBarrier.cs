using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBarrier : MonoBehaviour

{
    private Transform yellowBarrierTransform;
    Vector3 pos = new Vector3(0, 0, 0);
    private int count = 0;

    public void BarrierActive()
    {

        yellowBarrierTransform = gameObject.transform;
        pos = yellowBarrierTransform.position;
        pos = new Vector3(0, 10, 0);
        
        
    }

}
