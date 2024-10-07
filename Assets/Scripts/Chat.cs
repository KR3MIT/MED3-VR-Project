using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chat : MonoBehaviour
{
    public static Chat Instance { get; private set; }

    public GameObject chatMessage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddMessage(string name, string message, bool isRight)
    {
        GameObject newMessage = Instantiate(chatMessage, transform);

        var nameText = newMessage.transform.GetChild(0).GetComponent<TMP_Text>();
        var messageText = newMessage.transform.GetChild(1).GetComponent<TMP_Text>();
        nameText.text = name;
        messageText.text = message;
    }

    public void ResetChat()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}