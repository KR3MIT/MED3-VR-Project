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
        text.text = "Tillykke med at n� s� langt! Nu skal vi se om du kan huske de tegn du har l�rt! Kan du vise mig h�ndtegnet for gr�n?";    

    }

    public void Gr�n()
    {
        text.text = "korrekt! kan du lav h�ndtegnet for Hej?";
    }

    public void Hej()
    {
        text.text = "korrekt! kan du lav h�ndtegnet for Lille Kugle?";
    }

    public void LilleKugle()
    {
        text.text = "yep";
    }

}