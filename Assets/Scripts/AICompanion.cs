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


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    

    public void MoveAndTakeStart()
    {
        currentTypes.Clear();
        canDefine = true;
        Debug.Log("Which object?");
    }

    public void DefineObjectType(ObjectType objectType)
    {
        if (!canDefine)
        {
            return;
        }
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
            Debug.Log("Only one object found moving proceeding to move and take");
            ObjectInfo objectInfo = FindObjectsWithTypes(currentTypes)[0];
            Move(objectInfo.transform);
        }
    }

    private bool CheckMultiple(List<ObjectType> types)
    {
        if (FindObjectsWithTypes(types).Count > 1)
        {
            Debug.Log("There are multiple objects of type " + types + ". Please specify.");
            canDefine = true;
            return true;
        }
        return false;
    }

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
