using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    void Start()
    {
        // Access the Chat instance and call its methods
        if (Chat.Instance != null)
        {
           
            Chat.Instance.AddMessage("Player", "Hej", false);
            //Chat.Instance.AddMessage("", "", true);
            //Chat.Instance.AddMessage("", "", true);
            Chat.Instance.AddMessage("Robot", "Hvad vil du?", true);
            Chat.Instance.AddMessage("Robot", "Hvad vil du?", true);
            Chat.Instance.AddMessage("Robot", "Hvad vil du?", true);
            Chat.Instance.AddMessage("Robot", "Hvad vil du?", true);
            Chat.Instance.AddMessage("Robot", "Hvad vil du?", true);

        }
        else
        {
            Debug.LogError("Chat instance is not available.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
