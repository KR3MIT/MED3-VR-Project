using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Table : MonoBehaviour
{
    public UnityEvent OneItemOnTable;
    public UnityEvent TwoItemsOnTable;

    public int maxItemsOnTable = 1;

    public HashSet<ObjectInfo> ItemsOnTable = new HashSet<ObjectInfo>();

    public TMP_Text text;

    private void Start()
    {
        text.text = "0/"+maxItemsOnTable;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.TryGetComponent(out ObjectInfo info))
        {
            //if (info.HasType(ObjectInfo.ObjectType.Sphere))
            //{
                OneItemOnTable.Invoke();
                ItemsOnTable.Add(info);

                if (ItemsOnTable.Count == 1)
                {
                    text.text = "1/"+maxItemsOnTable;
                }
                else if (ItemsOnTable.Count == 2 && maxItemsOnTable >= 2)
                {
                    text.text = "2/"+ maxItemsOnTable;
                    TwoItemsOnTable.Invoke();
                }
            //}
        }
    }
}
