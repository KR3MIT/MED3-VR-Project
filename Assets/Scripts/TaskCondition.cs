using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCondition : MonoBehaviour
{
    public GameObject continuepannel;
    public ParticleSystem confetti;

    public bool UIStartActive = false;
    public bool TutMode = false;
    public bool TutModeStartActive = false;
    public GameObject HandGesureManager;


    // Start is called before the first frame update
    void Start()
    {
        if (UIStartActive)
            continuepannel.SetActive(true);
        else
            continuepannel.SetActive(false);

        if (TutModeStartActive)
            TutMode = true;
        else
            TutMode = false;

        HandGesureManager.SetActive(TutMode);
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

    public void ToggleTutMode()
    {
        TutMode = !TutMode;
        HandGesureManager.SetActive(TutMode);
    }
}