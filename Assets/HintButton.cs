using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintButton : MonoBehaviour
{
    public int delay = 60;
    public bool startDelayOnStart =false;

    private void Start()
    {
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(false);
        }

        if (startDelayOnStart)
        {
            StartDelay();
        }
    }

    public void StartDelay()
    {
        StartCoroutine(HintDelay(delay));
    }

    private IEnumerator HintDelay(int time)
    {
        yield return new WaitForSeconds(time);
        
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(true);
        }
    }
}
