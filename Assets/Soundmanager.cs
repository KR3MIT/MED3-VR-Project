using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundmanager : MonoBehaviour
{
 public static Soundmanager instance;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
     
        }
    }

    public void PlaySound(int soundIndex)
    {
        audioSource.clip = audioClips[soundIndex];
        audioSource.Play();
    }
}
