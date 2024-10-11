using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chat : MonoBehaviour
{
    public static Chat Instance { get; private set; }

    public GameObject chatMessage;
    public Transform panelTransform;
    public Vector3 messageScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale factor for the messages


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
        if (panelTransform == null)
        {
            Debug.LogError("Panel transform is not set.");
            return;
        }

        if (isRight == true) {
            AddMessage("","", false);
            AddMessage("", "", false);
        }

        GameObject newMessage = Instantiate(chatMessage, panelTransform); // Instantiate on the panel

        var nameText = newMessage.transform.GetChild(0).GetComponent<TMP_Text>();
        var messageText = newMessage.transform.GetChild(1).GetComponent<TMP_Text>();
        nameText.text = name;
        messageText.text = message;

        // Scale up the message
        newMessage.GetComponent<RectTransform>().localScale = messageScale;

    }

    public void ResetChat()
    {
        if (panelTransform == null)
        {
            Debug.LogError("Panel transform is not set.");
            return;
        }

        foreach (Transform child in panelTransform)
        {
            //Destroy(child.gameObject);
        }
    }
}