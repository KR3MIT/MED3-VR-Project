using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowCurrentSign : MonoBehaviour
{
    public TMP_Text tmptext;
    public Text unitytext;

    private string originalText;


    private void Start()
    {
        if (unitytext == null)
        {
            originalText = tmptext.text;
        }
        else if (tmptext == null)
        {
            originalText = unitytext.text;
        }
    }

public void UpdateText(string sign)
    {
        if(tmptext == null)
        {
            unitytext.text = originalText + sign;
            return;
        }

        if(unitytext == null)
        {
            tmptext.text = originalText + sign;
            return;
        }
    }
}
