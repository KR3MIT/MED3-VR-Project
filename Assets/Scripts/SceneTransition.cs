using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneTransition : MonoBehaviour
{
    [Header("references")]
    public Image fadeImage;

    [Header("events")]
    public UnityEvent onFadeInComplete;
    public UnityEvent onFadeOutComplete;

    //singleton
    public static SceneTransition instance;

    private UnityAction fadeOutCallback;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void FadeOut(float duration, UnityAction callback = null)
    {
        fadeOutCallback = callback;
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine(duration, true));
    }

    public void FadeIn(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine(duration, false));
    }

    private IEnumerator FadeCoroutine(float duration, bool fadeOut)
    {
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        float startAlpha = fadeOut ? 0f : 1f;
        float endAlpha = fadeOut ? 1f : 0f;

        float stayBlackDuration = duration / 5;
        yield return new WaitForSeconds(stayBlackDuration); //makes it feel better

        duration -= stayBlackDuration;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;

        if (fadeOut)
        {
            onFadeOutComplete?.Invoke();
            fadeOutCallback?.Invoke();
        }
        else
        {
            onFadeInComplete?.Invoke();
        }
    }
}
