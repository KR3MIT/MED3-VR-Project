using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BucketFilledDetector : MonoBehaviour
{
    public UnityEvent BucketFilled;
    public UnityEvent TwoItemsBucketFilled;

    public HashSet<ObjectInfo> BucketFilledObjects = new HashSet<ObjectInfo>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ObjectInfo info))
        {
            if (info.HasType(ObjectInfo.ObjectType.Sphere))
            {
                BucketFilled.Invoke();
                BucketFilledObjects.Add(info);
                if (BucketFilledObjects.Count == 2)
                {
                    TwoItemsBucketFilled.Invoke();
                }
            }
        }
    }
}
