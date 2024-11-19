using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action OnDoorOpen;

    public Transform doorOpenPosition;
    public float doorSpeed = 1f;
    public bool doorOpen = false;

    private void Start()
    {
        //StartCoroutine(test());
    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(3.5f);
        OpenDoor();
    }

    public void OpenDoor()
    {
        Debug.Log("Door opening");
        StartCoroutine(OpenDoorCoroutine());
    }

    IEnumerator OpenDoorCoroutine()
    {
        while (Vector3.Distance(transform.position, doorOpenPosition.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, doorOpenPosition.position, Time.deltaTime * doorSpeed);
            yield return null;
        }

        Debug.Log("Door opened");
        doorOpen = true;
        OnDoorOpen?.Invoke();
    }
}
