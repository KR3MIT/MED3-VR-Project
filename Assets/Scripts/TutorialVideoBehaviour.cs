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

    int videoIndex;

    private void Start()
    {
        // Assign the Render Texture to the Raw Image
        rawImage.texture = videoPlayer.targetTexture;
    }

    public void OnVideoButtonClick(int index)
    {
            rawImage.gameObject.SetActive(true);
            // Play the selected video
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();
    }

    public void OnExitButtonClick()
    {
        // Stop the video
        videoPlayer.Stop();
        rawImage.gameObject.SetActive(false);
    }

}
