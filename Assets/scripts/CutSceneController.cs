using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    public State state;
    private VideoPlayer videoPlayer = new VideoPlayer();

    // Use this for initialization
    void Start()
    {
        if(state.currentLevel < state.levelSettings.Count)
        {
            videoPlayer = gameObject.GetComponent<VideoPlayer>();
            //string url = "Assets/Resources/lvl" + state.currentLevel + "/video/1.mp4";
            //videoPlayer.url = url;
            //videoPlayer.loopPointReached += LoadScene;
        }
        LoadScene(videoPlayer);
    }

    private void LoadScene(VideoPlayer vp)
    {

        if (state.currentLevel < state.levelSettings.Count - 1)
        { 
        Debug.Log(state.currentLevel);
            SceneManager.LoadSceneAsync(state.levelSettings[state.currentLevel+1].sceneName);
        }
        else
        {
            SceneManager.LoadSceneAsync("title");
        }
    }
}
