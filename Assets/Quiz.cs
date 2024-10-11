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
        text.text = "Tillykke med at nå så langt! Nu skal vi se om du kan huske de tegn du har lært! Kan du vise mig håndtegnet for grøn?";    

    }

    public void Grøn()
    {
        text.text = "korrekt! kan du lav håndtegnet for Hej?";
    }

    public void Hej()
    {
        text.text = "korrekt! kan du lav håndtegnet for Lille Kugle?";
    }

    public void LilleKugle()
    {
        text.text = "yep";
    }

}