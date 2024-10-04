using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ObjectType = ObjectInfo.ObjectType;

public class AICompanion : MonoBehaviour
{
    int currentLevel = 0;
    private NavMeshAgent agent;
    private ObjectInfo carryingObject;
    private List<ObjectType> currentTypes = new List<ObjectInfo.ObjectType>();
    private bool canDefine = false;
    private bool actionRunning = false;

    private enum State
    {
        Idle,
        MovePickup,
        MovePlacing,
    }

    private State state = State.Idle;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    

    



    public void DefineObjectType(string objectTypeString)
    {
        if (!canDefine)
        {
            return;
        }
        ObjectType objectType = (ObjectType)System.Enum.Parse(typeof(ObjectType), objectTypeString);//take the string and convert it to the enum, since unityevents wont take an enum smh

        currentTypes.Add(objectType);
        canDefine = false;
        Debug.Log("Object defined: " + objectType);

        if(CheckMultiple(currentTypes))
        {
            Debug.Log("There are multiple objects of type " + currentTypes + ". Please specify.");
            canDefine = true;
            return;
        }
        else
        {
            switch (state)
            {
                case State.MovePickup:
                    MovePickup();
                    break;
                case State.MovePlacing:
                    //MoveAndPlace();
                    break;
            }
        }
    }

    private bool CheckMultiple(List<ObjectType> types)
    {
        if (FindObjectsWithTypes(types).Count > 1)
        {
            canDefine = true;
            return true;
        }
        return false;
    }

    #region move and place

    #endregion

    #region move and pickup
    public void MoveAndPickupStart()
    {
        if (actionRunning)
        {
            return;
        }

        currentTypes.Clear();
        state = State.MovePickup;
        canDefine = true;
        Debug.Log("Which object?");
    }

    private void MovePickup()
    {
        Debug.Log("Only one object found moving proceeding to move and take");
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
        while(Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            yield return null;
        }
        PickUp(FindObjectsWithTypes(currentTypes)[0]);
        actionRunning = false;
    }

    #endregion

    public void Move(Transform target)
    {
        agent.SetDestination(target.position - (target.position - transform.position).normalized);
    }

    public void PickUp(ObjectInfo obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero + new Vector3(0, 1, 1f);
        carryingObject = obj;
        obj.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Place()
    {
        carryingObject.GetComponent<Rigidbody>().isKinematic = false;
        carryingObject.transform.SetParent(null);
        carryingObject = null;
    }

    public void Hello()
    {
        //play wave anim
        Debug.Log("Hello!");
    }


    #region Helper methods
    //private ObjectInfo FindObjectWithTypes(List<ObjectType> types)
    //{
    //    foreach (ObjectInfo obj in ObjectInfo.objectList[currentLevel])
    //    {
    //        bool allTypesMatch = true;
    //        foreach (ObjectType type in types)
    //        {
    //            if (!obj.types.Contains(type))
    //            {
    //                allTypesMatch = false;
    //                break;
    //            }
    //        }

    //        if (allTypesMatch)
    //        {
    //            return obj;
    //        }
    //    }
    //    return null;
    //}

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

    private List<ObjectInfo> FindObjectsWithTypes(ObjectType type)
    {
        List<ObjectInfo> objects = new List<ObjectInfo>();
        foreach (ObjectInfo obj in ObjectInfo.objectList[currentLevel])
        {
            if (obj.types.Contains(type))
            {
                objects.Add(obj);
            }
        }
        return objects;
    }
    #endregion
}
