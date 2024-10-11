using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public List<string> håndtegn;
    private string currentTegn;

    private string sentence;

    // Start is called before the first frame update
    void Start()
    {
        RandomTegn();

        text.text = "Tillykke med at nå så langt! Nu skal vi se om du kan huske de tegn du har lært! Kan du vise mig håndtegnet for " + sentence + "?";    

    }
    public void CheckTegn(string tegnNavn)
    {
        //make string to enum
     

        if (tegnNavn != currentTegn)
        {
            return;
        }

        sentence = "Korrekt! Kan du lave håndtegnet for ";

        RandomTegn();
    }

    private void RandomTegn()
    {
       int randomIndex = Random.Range(0,håndtegn.Count); 
        currentTegn = håndtegn[randomIndex];

        sentence += currentTegn;
    }

    public void Grøn()
    {
        text.text = "Korrekt! Kan du lave håndtegnet for Hej?";
    }

    public void Hej()
    {
        text.text = "Korrekt! Kan du lave håndtegnet for Lille Kugle?";
    }

    public void LilleKugle()
    {
        text.text = "Korrekt! Kan du lave håndtegnet for ";
    }
    
    public void Spand()
    {
        text.text = "Korrekt! Kan du lave håndtegnet for Spand ";
    }
}