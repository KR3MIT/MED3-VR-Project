using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBarrier : MonoBehaviour

{
    private Transform yellowBarrierTransform;
 

    public void BarrierActive()
    {
        
            yellowBarrierTransform = gameObject.transform;
            yellowBarrierTransform.position += new Vector3(0, 15, 0);
        
        
    }

}
