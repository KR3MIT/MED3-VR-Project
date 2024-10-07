using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialVideoBehaviour : MonoBehaviour
{
    public List<VideoClip> videoClips;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    int videoIndex = 0;

    private void Start()
    {
        // Assign the Render Texture to the Raw Image
        rawImage.texture = videoPlayer.targetTexture;
    }

    public void OnVideoButtonClick()
    {
            rawImage.gameObject.SetActive(true);
            // Play the selected video
            videoPlayer.clip = videoClips[videoIndex];
            videoPlayer.Play();

            videoIndex++;
    }

    public void OnExitButtonClick()
    {
        // Stop the video
        videoPlayer.Stop();
        rawImage.gameObject.SetActive(false);
    }

}
