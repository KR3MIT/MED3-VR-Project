using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HintSystem : MonoBehaviour
{
    public AICompanion aiCompanion;
    public GameObject hint1;
    public GameObject hint2;
    public GameObject hintPanel;

    bool carryingObject = false;
    public bool showHint = false;

    public void Start()
    {
        //aiCompanion = GameObject.FindObjectOfType<AICompanion>();
        aiCompanion.OnCarryingObjectSet += CheckCarryingObject;
    }
    public void EnableHint()
    {
        showHint = true;
        CheckCarryingObject();
    }

    public void CheckCarryingObject()
    {
        if (!showHint)
        {
            return;
        }

        carryingObject = aiCompanion.carryingObject;

        if(carryingObject)
        {
            EnablePanel();
            EnableHint(2);
        }
        else
        {
            EnablePanel();
            EnableHint(1);
        }
    }


    public void EnablePanel()
    {
        hintPanel.SetActive(true);
    }

    public void EnableHint(int hintNumber)
    {
        if (hintNumber == 1)
        {
            hint1.SetActive(true);
            hint2.SetActive(false);
        }
        else if (hintNumber == 2)
        {
            hint2.SetActive(true);
            hint1.SetActive(false);
        }
    }
}
