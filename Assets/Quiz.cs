using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public List<string> h�ndtegn;
    public string currentTegn;

    private string sentence;

    // Start is called before the first frame update
    void Start()
    {
        RandomTegn(true);

        text.text = "Tillykke med at n� s� langt! Nu skal vi se om du kan huske de tegn du har l�rt! Kan du vise mig h�ndtegnet for " + sentence;

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

    private void RandomTegn(bool firstTime = false)
    {
        if (h�ndtegn.Count == 0)
        {
            text.text = "Tillykke! Du har nu gennemf�rt quizzen!";
            return;
        }

        if (!firstTime)
        {
            h�ndtegn.Remove(currentTegn);
        }

        int randomIndex = Random.Range(0, h�ndtegn.Count);
        currentTegn = h�ndtegn[randomIndex];

        sentence += currentTegn + "?";

        if (!firstTime)
            text.text = sentence;
    }
}