using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICompanion : MonoBehaviour
{
    int currentLevel = 0;
    private NavMeshAgent agent;
    private ObjectInfo carryingObject;
    private Queue<IEnumerator> actionQueue = new Queue<IEnumerator>();
    private bool isProcessingQueue = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        EnqueueAction(MoveAndWait(FindObjectWithTypes(new List<ObjectInfo.ObjectType> { ObjectInfo.ObjectType.Cube, ObjectInfo.ObjectType.Yellow }).transform));
        EnqueueAction(PickUpAndWait(FindObjectWithTypes(new List<ObjectInfo.ObjectType> { ObjectInfo.ObjectType.Cube, ObjectInfo.ObjectType.Yellow })));
        EnqueueAction(MoveAndWait(FindObjectWithTypes(new List<ObjectInfo.ObjectType> { ObjectInfo.ObjectType.PressurePad }).transform));
        EnqueueAction(PlaceAndWait());

        StartCoroutine(ProcessQueue());
    }

    private void EnqueueAction(IEnumerator action)
    {
        actionQueue.Enqueue(action);
    }

    private IEnumerator ProcessQueue()
    {
        if (isProcessingQueue)
            yield break;

        isProcessingQueue = true;

        while (actionQueue.Count > 0)
        {
            yield return StartCoroutine(actionQueue.Dequeue());
        }

        isProcessingQueue = false;
    }

    private IEnumerator MoveAndWait(Transform target)
    {
        Move(target);
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
    }

    private IEnumerator PickUpAndWait(ObjectInfo obj)
    {
        PickUp(obj);
        yield return null;
    }

    private IEnumerator PlaceAndWait()
    {
        Place();
        yield return null;
    }

    private ObjectInfo FindObjectWithTypes(List<ObjectInfo.ObjectType> types)
    {
        foreach (ObjectInfo obj in ObjectInfo.objectList[currentLevel])
        {
            bool allTypesMatch = true;
            foreach (ObjectInfo.ObjectType type in types)
            {
                if (!obj.types.Contains(type))
                {
                    allTypesMatch = false;
                    break;
                }
            }

            if (allTypesMatch)
            {
                return obj;
            }
        }
        return null;
    }

    private void Move(Transform target)
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
}
