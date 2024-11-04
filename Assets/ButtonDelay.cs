using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDelay : MonoBehaviour
{
    public float delay = 3f;
    public bool startInactive = true;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>();

        if (startInactive)
        {
            DelayButtonAction();
        }
    }

    public void DelayButtonAction()
    {
        StartCoroutine(DelayButton());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayButton()
    {
        button.interactable = false;
        yield return new WaitForSeconds(delay);
        button.interactable = true;
    }
}
