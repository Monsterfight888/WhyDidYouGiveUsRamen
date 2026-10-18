using UnityEngine;
using UnityEngine.Video;

public class PlayCutscene : MonoBehaviour
{
    public string fileName = "8-Final_Cutscene.mp4";

    void Start()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();
        vp.source = VideoSource.Url;
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        vp.Play();
    }
}
