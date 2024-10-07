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

    int videoIndex = 1;

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

    private int CalculateVideoIndex(Vector2 clickPosition)
    {
        // Implement your logic to determine the video index based on the click position
        // For example, you could divide the Raw Image into regions and assign a video to each region
        int videoIndex = 0; // Default to the first video

        // Add your region calculation logic here

        return videoIndex;
    }
}
