using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "tillykke med at n� s� langt!, kan du lav h�ndtegnet for gr�n?";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hej()
    {
        text.text = "korrekt! kan du lav h�ndtegnet for hej?";
    }

    public void LilleKugle()
    {
        text.text = "yep";
    }

}