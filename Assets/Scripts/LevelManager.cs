using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //public static LevelManager instance;

    public float fadeInTime = 1f;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this);
        //}
        //else
        //{
        //    Destroy(this);
        //}

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        //StartCoroutine(test());
    }

    IEnumerator test ()
    {
        yield return new WaitForSeconds(3f);
        EndLevel(3f, "lvl 1");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //SceneTransition.instance.FadeIn(fadeInTime);
    }

    /// <summary>
    /// end level with fade out
    /// </summary>
    /// <param name="fadeDuration"></param>
    /// <param name="levelName"></param>
    private void EndLevel(float fadeDuration, string levelName)
    {
        SceneTransition.instance.FadeOut(fadeDuration, () => LoadLevel(levelName));
    }

    public void EndLVL(string levelName)
    {
        EndLevel(3f, "lvl 1");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void LoadLevel()
    {
        //load next scene from build index
        ObjectInfo.objectList.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
