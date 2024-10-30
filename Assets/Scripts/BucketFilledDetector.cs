using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BucketFilledDetector : MonoBehaviour
{
    public UnityEvent BucketFilled;
    public UnityEvent TwoItemsBucketFilled;

    public HashSet<ObjectInfo> BucketFilledObjects = new HashSet<ObjectInfo>();

    private TMP_Text text;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        characterController = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.TryGetComponent(out ObjectInfo info))
        {
            if (info.HasType(ObjectInfo.ObjectType.Sphere))
            {
                BucketFilled.Invoke();
                BucketFilledObjects.Add(info);

                if(BucketFilledObjects.Count == 1)
                {
                    text.text = "1/2";
                }
                else if (BucketFilledObjects.Count == 2)
                {
                    text.text = "2/2";
                    TwoItemsBucketFilled.Invoke();
                }
            }
        }
    }
}
