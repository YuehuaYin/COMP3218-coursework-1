using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public GameObject videoplayer;
    private UnityEngine.Video.VideoPlayer video;
    public GameObject button;

    public void nextScene(){
        switch (SceneManager.GetActiveScene().name) 
        {
        case "01":
            SceneManager.LoadScene("02");
            break;
        case "02":
            SceneManager.LoadScene("03");
            break;
        case "03":
            SceneManager.LoadScene("03.1");
            break;
        case "03.1":
            SceneManager.LoadScene("03.2");
            break;
        case "03.2":
            SceneManager.LoadScene("03.3");
            break;
        case "03.3":
            SceneManager.LoadScene("04");
            break;
        case "04":
            SceneManager.LoadScene("04.1");
            break;
        case "04.1":
            SceneManager.LoadScene("05");
            break;
        case "05":
            SceneManager.LoadScene("06");
            break;
        case "06":
            SceneManager.LoadScene("07");
            break;
        }
    }

    public void startButton()
    {
        button.SetActive(false);
        video = videoplayer.GetComponent<UnityEngine.Video.VideoPlayer>();
        video.Play();
        video.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer video)
    {
        SceneManager.LoadScene("01");
    }


    public void restartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}