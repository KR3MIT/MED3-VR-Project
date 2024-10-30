using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCurrentSign : MonoBehaviour
{
    public TMP_Text text;

    private string originalText;


    private void Start()
    {
        originalText = text.text;
    }

    public void UpdateText(string sign)
    {
        text.text = originalText + sign;
    }
}
