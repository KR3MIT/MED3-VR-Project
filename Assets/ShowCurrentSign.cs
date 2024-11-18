using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowCurrentSign : MonoBehaviour
{
    public TMP_Text tmptext;
    public Text unitytext;

    public bool isTMP;

    private string originalText;


    private void Start()
    {
        if (isTMP)
        {
            originalText = tmptext.text;
        }
        else
        {
            originalText = unitytext.text;
        }
    }

public void UpdateText(string sign)
    {
        if(!isTMP)
        {
            unitytext.text = originalText + sign;
            return;
        }

        if(isTMP)
        {
            tmptext.text = originalText + sign;
            return;
        }
    }
}
