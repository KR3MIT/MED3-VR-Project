using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystemPuzzle1 : MonoBehaviour
{
    public AICompanion aiCompanion;
    public Door door;

    public GameObject hint1;
    public GameObject hint2;
    public GameObject hint3;

    public GameObject hintPanel;

    bool carryingObject = false;
    bool doorOpen = false;

    public bool showHint = false;

    public void Start()
    {
        //aiCompanion = GameObject.FindObjectOfType<AICompanion>();
        aiCompanion.OnCarryingObjectSet += CheckConditions;
        door.OnDoorOpen += CheckConditions;
    }

    public void EnableHint()
    {
        showHint = true;
        CheckConditions();
    }

    public void CheckConditions()
    {
        if (!showHint)
        {
            return;
        }

        doorOpen = door.doorOpen;
        carryingObject = aiCompanion.carryingObject;
        if (!aiCompanion.carryingObject.HasType(ObjectInfo.ObjectType.Brown))//only the brown object should be carried after the door is open
        {
            carryingObject = false;
        }


        if (!doorOpen)
        {
            EnablePanel();
            EnableHint(1);
        }
        else if(doorOpen && !carryingObject)
        {
            EnablePanel();
            EnableHint(2);
        }
        else if(doorOpen && carryingObject)
        {
            EnablePanel();
            EnableHint(3);
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
            hint3.SetActive(false);
        }
        else if (hintNumber == 2)
        {
            hint2.SetActive(true);
            hint1.SetActive(false);
            hint3.SetActive(false);
        }
        else if (hintNumber == 3)
        {
            hint3.SetActive(true);
            hint1.SetActive(false);
            hint2.SetActive(false);
        }
    }
}
