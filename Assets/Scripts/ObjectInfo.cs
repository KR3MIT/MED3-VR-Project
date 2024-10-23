using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public int level;
    public bool isPickUpable = true;

    public static Dictionary<int, List<ObjectInfo>> objectList = new Dictionary<int, List<ObjectInfo>>();

    public enum ObjectType
    {
        Green,
        Blue,
        Cube,
        Sphere,
        Capsule,
        Bucket,
    }

    public List<ObjectType> types = new List<ObjectType>();

    public void Awake()
    {
        if(objectList.Count == 0)
        {
            for (int i = 0; i < 15; i++)
            {
                objectList.Add(i, new List<ObjectInfo>());
            }
        }

        objectList[level].Add(this);
    }

    public bool HasType(ObjectType type)
    {
        return types.Contains(type);
    }
}