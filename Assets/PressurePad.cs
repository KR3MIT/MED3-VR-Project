using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
 public List<GameObject> objectToRotate;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("hahaha");
        if (other.CompareTag("AI"))//aiguy is object ig
        {
            foreach (GameObject obj in objectToRotate) 
            {
                obj.transform.Rotate(0, 15, 0); 
            }
            //objectToRotate.transform.Rotate(0, 15, 0);
            //do the thing!!
            Debug.Log("Pressure pad activated");
        }
    }
}
