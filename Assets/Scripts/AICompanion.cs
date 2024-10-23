using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using ObjectType = ObjectInfo.ObjectType;

public class AICompanion : MonoBehaviour
{
    int currentLevel = 0;
    private NavMeshAgent agent;

    [Header("Debug stuff dont touch only look!!!")]
    public ObjectInfo carryingObject;
    public List<ObjectType> currentTypes = new List<ObjectType>();
    public bool canDefine = false;
    public bool actionRunning = false;

    public enum State
    {
        Idle,
        MovePickup,
        MovePlace,
    }

    private State state = State.Idle;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        MoveAndPickupStart();
        yield return new WaitForSeconds(1f);
        DefineObjectType("Sphere");
        yield return new WaitForSeconds(6f);

        MoveAndPlaceStart();
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
                case State.MovePlace:
                    MovePlace();
                    break;
            }
        }
    }

    

    #region move and place
    public void MoveAndPlaceStart()
    {
        StartActionDefinition(State.MovePlace);
    }

    private void MovePlace()
    {
        Debug.Log("Only one object found proceeding to move and place");
        ObjectInfo objectInfo = FindObjectsWithTypes(currentTypes)[0];
        StartCoroutine(MoveAndPlace(objectInfo.transform));
    }


    private IEnumerator MoveAndPlace(Transform target)
    {
        actionRunning = true;
        Move(target);
        while (Vector3.Distance(transform.position, target.position) > 1.5f)
        {   
            yield return null;
        }
        Place(target);
        actionRunning = false;
        state = State.Idle;
    }

    #endregion

    #region move and pickup
    public void MoveAndPickupStart()
    {
        StartActionDefinition(State.MovePickup);
    }

    private void MovePickup()
    {
        Debug.Log("Only one object found proceeding to move and take");
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
        carryingObject = null;
        carryingObject.transform.position = new Vector3(targetTransform.position.x, carryingObject.transform.position.y, targetTransform.position.z);
    }

    public void Hello()
    {
        //play wave anim
        Debug.Log("Hello!");
    }

    #endregion

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

    private bool CheckMultiple(List<ObjectType> types)
    {
        if (FindObjectsWithTypes(types).Count > 1)
        {
            canDefine = true;
            return true;
        }
        return false;
    }
    #endregion
}
