using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ObjectType = ObjectInfo.ObjectType;

public class AICompanion : MonoBehaviour
{
    public event Action OnCarryingObjectSet;

    int currentLevel = 0;
    private NavMeshAgent agent;

    [Header("Debug stuff dont touch only look!!!")]
    public ObjectInfo carryingObject;
    public List<ObjectType> currentTypes = new List<ObjectType>();
    public bool canDefine = false;
    public bool actionRunning = false;
    public bool noPath = false;
    public TMP_Text agentText;
    public Transform resetPoint;
    public enum State
    {
        Idle,
        MovePickup,
        MovePlace,
        Move,
        MoveNumber
    }

    public State state = State.Idle;

    private Dictionary<ObjectType, string> userFriendlyNames = new Dictionary<ObjectType, string>
{
    { ObjectType.Green, "Grøn" },
    { ObjectType.Brown, "Brun" },
    { ObjectType.Yellow, "Gul" },
    { ObjectType.Cube, "Firkant" },
    { ObjectType.Sphere, "Kugle" },
    { ObjectType.Capsule, "Cylinder" },
    { ObjectType.Bucket, "Spand" },
    { ObjectType.Table, "Bord" },
    { ObjectType.En, "En" },
    { ObjectType.To, "To" },
    { ObjectType.Tre, "Tre" },
    { ObjectType.Fire, "Fire" },
    { ObjectType.Fem, "Fem" }
};

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //StartCoroutine(test());
    }

    private void Update()
    {
        //if (Mouse.current.rightButton.wasPressedThisFrame)
        //{
        //    Debug.Log("Left mouse button pressed");
        //    StartCoroutine(test());
        //}

        if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            noPath = true;
            agentText.text = "Jeg fandt ingen vej, pr�v noget andet";
            Debug.Log("No path found, please define new action");
            agent.SetDestination(transform.position);
            actionRunning = false;
            canDefine = true;
            StopAllCoroutines();
        }
        else
        {
            noPath = false;
        }
    }

    IEnumerator test()
    {
        //yield return new WaitForSeconds(2f);
        //MoveAndPickupStart();
        //yield return new WaitForSeconds(1f);
        //DefineObjectType("Green");
        //yield return new WaitForSeconds(6f);

        //MoveAndPlaceStart();
        //yield return new WaitForSeconds(1f);
        //DefineObjectType("Bucket");
        //yield return new WaitForSeconds(6f);

        //MoveAndPickupStart();
        //yield return new WaitForSeconds(1f);
        //DefineObjectType("Blue");
        //yield return new WaitForSeconds(5f);

        //MoveAndPlaceStart();
        //yield return new WaitForSeconds(1f);
        //DefineObjectType("Bucket");
        //yield return new WaitForSeconds(6f);

        MoveToStart();
        yield return new WaitForSeconds(1f);
        DefineObjectType("Bucket");

        yield return null;
    }


    public void StartActionDefinition(State stateToStart)
    {
        if (actionRunning)
        {
            return;
        }

        currentTypes.Clear();
        state = stateToStart;
        canDefine = true;

        Debug.Log("Which object?");
    }

    public void DefineObjectType(string objectTypeString)
    {
        if (!canDefine)
        {
            return;
        }

        if (state == State.MovePickup && objectTypeString == "Bucket")//HAHHAHAHAHAHAHAHAHHAAHHAHAHAHAHAHAHAHAHHAHAHAHAHAHAHHAHAHAHAHAHAHAHHAHHAHAHAHAHA
        {
            return;
        }

        ObjectType objectType = (ObjectType)System.Enum.Parse(typeof(ObjectType), objectTypeString);//take the string and convert it to the enum, since unityevents wont take an enum smh

        currentTypes.Add(objectType);
        canDefine = false;
        Debug.Log("Object defined: " + objectType);


        if (CheckMultiple(currentTypes))
        {
            string types = "";
            foreach (ObjectType type in currentTypes)
            {
                //types += type + ", ";
                types += GetUserFriendlyName(type) + ", ";
            }

            agentText.text = "Der er mere end en" + types + " hvilken " + types + "mener du?" ;
            Debug.Log("There are multiple objects of type " + types + ". Please specify.");
            canDefine = true;
            return;
        }
        else
        {
            string friendlyName = GetUserFriendlyName(objectType);
            switch (state)
            {
                case State.MovePickup:
                    agentText.text = friendlyName + " fundet. G�r hen og samler objektet op. Hvad skal jeg med det?";
                    MovePickup();
                    break;
                case State.MovePlace:
                    agentText.text = friendlyName + " fundet. G�r hen og placere det holdte objekt. Hvad skal jeg nu?";
                    MovePlace();
                    break;
                case State.Move:
                    agentText.text = friendlyName + " fundet. G�r hen til objektet.";
                    MoveTo();
                    break;
            }
        }
    }
    private string GetUserFriendlyName(ObjectType objectType)
    {
        if (userFriendlyNames.TryGetValue(objectType, out string friendlyName))
        {
            return friendlyName;
        }
        return objectType.ToString(); // Fallback to the enum name if not found
    }



    #region move and place
    public void MoveAndPlaceStart()
    {
        if (carryingObject == null)
        {
            agentText.text = "Kan ikke pladsere noget da jeg ikke holder noget, saml noget op først";
            Debug.Log("No object to place");
            return;
        }
        StartActionDefinition(State.MovePlace);
        if (actionRunning)
        {
            agentText.text = "Vent venligst til jeg er f�rdig med den nuv�rende aktion";
            return;
        }
        agentText.text = "Hvor skal jeg placere det holdte objekt?";
    }

    private void MovePlace()
    {
        Debug.Log("Only one object found proceeding to move and place");
        ObjectInfo objectInfo = FindObjectsWithTypes(currentTypes)[0];
        StartCoroutine(MoveAndPlace(objectInfo.transform));
    }


    private IEnumerator MoveAndPlace(Transform target)
    {
        if(carryingObject == null)
        {
            Debug.Log("No object to place");
            actionRunning = false;
            state = State.Idle;
            yield break;
        }
        actionRunning = true;
        Move(target);
        while (Vector3.Distance(transform.position, target.position) > 2f )
        {   
            yield return null;
        }
        Place(target);
        actionRunning = false;
        state = State.Idle;
        Debug.Log("Move and place finished");
    }

    #endregion

    #region move and pickup
    public void MoveAndPickupStart()
    {
        if(carryingObject != null)
        {
            agentText.text = "Jeg holder allerede noget, placere det før du samler noget andet op.";
            Debug.Log("Already holding object");
            return;
        }
        StartActionDefinition(State.MovePickup);
        Debug.Log("Move and pickup started");
        if (actionRunning)
        {
            agentText.text = "Vent venligst til jeg er f�rdig med den nuv�rende aktion";
            return;
        }
        agentText.text = "Hvad skal jeg samle op?";
    }

    private void MovePickup()
    {
        Debug.Log("Only one object found proceeding to move and take "+ currentTypes[0]);
        ObjectInfo objectInfo = FindObjectsWithTypes(currentTypes)[0];

        if(objectInfo.isPickUpable)
        {
            StartCoroutine(MoveAndPickup(objectInfo.transform));
        }
        else
        {
            Debug.Log("Object is not pickable");
        }
    }


    private IEnumerator MoveAndPickup(Transform target)
    {
        actionRunning = true;
        Move(target);
        while(Vector3.Distance(transform.position, target.position) > 1.5f)
        {
            yield return null;
        }
        PickUp(FindObjectsWithTypes(currentTypes)[0]);
        actionRunning = false;
        state= State.Idle;
    }

    #endregion

    #region move

    public void MoveToStart()
    {
        StartActionDefinition(State.Move);
        if (actionRunning)
        {
            agentText.text = "Vent venligst til jeg er f�rdig med den nuv�rende aktion";
            return;
        }
        agentText.text = "Hvor skal jeg bev�ge mig hen?";
    }

    private void MoveTo()
    {
        Debug.Log("Only one object found proceeding to move");
        ObjectInfo objectInfo = FindObjectsWithTypes(currentTypes)[0];

        StartCoroutine(MoveToRoutine(objectInfo.transform));
    }

    private IEnumerator MoveToRoutine(Transform target)
    {
        actionRunning = true;
        Move(target);
        while (Vector3.Distance(transform.position, target.position) > 1.5f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        if(resetPoint != null)
        agent.SetDestination(resetPoint.position);
        yield return new WaitForSeconds(.5f);

        actionRunning = false;
        state = State.Idle;
    }

    #endregion

    #region moveNumber

    public void MoveToNumberStart(string number)
    {
        if (actionRunning)
        {
            agentText.text = "Vent venligst til jeg er f�rdig med den nuv�rende aktion";
            return;
        }
        state = State.MoveNumber;
        currentTypes.Clear();

        currentTypes.Add((ObjectType)System.Enum.Parse(typeof(ObjectType), number)); //unityevents and enums :):):):):):):)
        
        MoveToNumber();
        agentText.text = "Bev�ger mig mod tallet " + number;
    }

    private void MoveToNumber()
    {
        ObjectInfo objectInfo = FindObjectsWithTypes(currentTypes)[0];

        StartCoroutine(MoveToNumberRoutine(objectInfo.transform));
    }

    private IEnumerator MoveToNumberRoutine(Transform target)
    {
        actionRunning = true;
        Move(target);
        while (Vector3.Distance(transform.position, target.position) > 1.5f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(.5f);
        if (resetPoint != null)
            agent.SetDestination(resetPoint.position);
        yield return new WaitForSeconds(.5f);

        actionRunning = false;
        state = State.Idle;
    }

    #endregion

    #region actions
    private void Move(Transform target)
    {
        agent.SetDestination(target.position - (target.position - transform.position).normalized);
    }

    private void PickUp(ObjectInfo obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero + new Vector3(0, 1, 1f);
        carryingObject = obj;
        obj.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Place()
    {
        carryingObject.GetComponent<Rigidbody>().isKinematic = false;
        carryingObject.transform.SetParent(null);
        carryingObject = null;
    }

    private void Place(Transform targetTransform)
    {
        carryingObject.GetComponent<Rigidbody>().isKinematic = false;
        carryingObject.transform.SetParent(null);
        carryingObject.transform.position = new Vector3(targetTransform.position.x, carryingObject.transform.position.y, targetTransform.position.z);
        carryingObject = null;
    }

    public void Hello()
    {
        //play wave anim
        Debug.Log("Hello!");
        agentText.text = "Hej!";
    }

    #endregion

    public void EndMove(Transform tra)
    {
        agentText.text = "Farvel :)";
        agent.SetDestination(tra.position);
    }

    #region Helper methods

    private List<ObjectInfo> FindObjectsWithTypes(List<ObjectType> types)
    {
        List<ObjectInfo> objectInfos = new List<ObjectInfo>();
        foreach (ObjectInfo obj in ObjectInfo.objectList[currentLevel])
        {
            bool allTypesMatch = true;
            foreach (ObjectType type in types)
            {
                if (!obj.types.Contains(type))
                {
                    allTypesMatch = false;
                    break;
                }
            }

            if (allTypesMatch)
            {
                objectInfos.Add(obj);
            }
        }
        return objectInfos;
    }

    // private List<ObjectInfo> FindObjectsWithTypes(ObjectType type)
    // {
    //     List<ObjectInfo> objects = new List<ObjectInfo>();
    //     foreach (ObjectInfo obj in ObjectInfo.objectList[currentLevel])
    //     {
    //         if (obj.types.Contains(type))
    //         {
    //             objects.Add(obj);
    //         }
    //     }
    //     return objects;
    // }

    private bool CheckMultiple(List<ObjectType> types)
    {
        if (FindObjectsWithTypes(types).Count > 1)
        {
            canDefine = true;
            return true;
        }
        return false;
    }

    public void SetCarryingObject()
    {
        carryingObject = null;
        OnCarryingObjectSet?.Invoke();
    }
    #endregion
}
