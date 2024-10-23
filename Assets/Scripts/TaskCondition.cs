using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCondition : MonoBehaviour
{
    public GameObject continuepannel;
    public ParticleSystem confetti;

    public bool StartActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (StartActive)
            continuepannel.SetActive(true);
        else
            continuepannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskCompleted()
    {
        confetti.Play();

        continuepannel.SetActive(true);
    }
}