using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCondition : MonoBehaviour
{
    public GameObject continuepannel;
    public ParticleSystem confetti;
    public Animator door;
    public bool UIStartActive = false;
    public bool TutMode = false;
    public bool TutModeStartActive = false;
    public GameObject HandGesureManager;
    public Soundmanager soundmanager;


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

        if(HandGesureManager != null)
        HandGesureManager?.SetActive(!TutMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskCompleted()
    {
        confetti.Play();
        continuepannel.SetActive(true);
        if(door != null)
        door.SetTrigger("open");
        Soundmanager.instance.PlaySound(0);
    }

    public void ToggleTutMode()
    {
        TutMode = !TutMode;
        if(HandGesureManager != null)
        HandGesureManager.SetActive(!TutMode);
    }
}