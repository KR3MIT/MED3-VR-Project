using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public GameObject objectToRotate;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hahaha");
        if (other.CompareTag("AI"))//aiguy is object ig
        {

            objectToRotate.transform.Rotate(0, 15, 0);
            //do the thing!!
            Debug.Log("Pressure pad activated");
        }
    }
}
