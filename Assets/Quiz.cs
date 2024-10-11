using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public List<string> h�ndtegn;
    private string currentTegn;

    private string sentence;

    // Start is called before the first frame update
    void Start()
    {
        RandomTegn();

        text.text = "Tillykke med at n� s� langt! Nu skal vi se om du kan huske de tegn du har l�rt! Kan du vise mig h�ndtegnet for " + sentence + "?";    

    }
    public void CheckTegn(string tegnNavn)
    {
        //make string to enum
     

        if (tegnNavn != currentTegn)
        {
            return;
        }

        sentence = "Korrekt! Kan du lave h�ndtegnet for ";

        RandomTegn();
    }

    private void RandomTegn()
    {
       int randomIndex = Random.Range(0,h�ndtegn.Count); 
        currentTegn = h�ndtegn[randomIndex];

        sentence += currentTegn;
    }

    public void Gr�n()
    {
        text.text = "Korrekt! Kan du lave h�ndtegnet for Hej?";
    }

    public void Hej()
    {
        text.text = "Korrekt! Kan du lave h�ndtegnet for Lille Kugle?";
    }

    public void LilleKugle()
    {
        text.text = "Korrekt! Kan du lave h�ndtegnet for ";
    }
    
    public void Spand()
    {
        text.text = "Korrekt! Kan du lave h�ndtegnet for Spand ";
    }
}