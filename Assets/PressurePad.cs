using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    public GameObject objectToRotate;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))//aiguy is object ig
        {
            Debug.Log("Object on pressure pad");
            //do the thing!!
        }
    }
}
