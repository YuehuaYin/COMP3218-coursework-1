using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public GameObject videoplayer;
    private UnityEngine.Video.VideoPlayer video;
    public GameObject button;
    public GameObject pauseMenu;
    private GameObject reset;
    

    public void nextScene(){
        switch (SceneManager.GetActiveScene().name)
        {
        case "01":
            //SceneManager.MoveGameObjectToScene(music, SceneManager.GetSceneByName("02"));
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
            SceneManager.LoadScene("04");
            break;
        case "04":
            SceneManager.LoadScene("04.1");
            break;
        case "04.1":
            SceneManager.LoadScene("05");
            break;
        case "05":
            SceneManager.LoadScene("05.1");
            break;
        case "05.1":
            SceneManager.LoadScene("06");
            break;
        case "06":
            SceneManager.LoadScene("06.1");
            break;
        case "06.1":
            SceneManager.LoadScene("07");
            break;
        case "07":
            SceneManager.LoadScene("08");
            break;
        case "08":
            SceneManager.LoadScene("09");
            break;
        case "09":
            SceneManager.LoadScene("Final");
            break;
        case "Final":
            SceneManager.LoadScene("End Screen");
            break;
        }
        try
        {
            GameObject.Find("BGmusic").GetComponent<bgmusic>().calm();
        } catch (Exception e)
        {
            Debug.Log("No bg music");
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
        DeathCounter.deaths += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        try
        {
            GameObject.Find("BGmusic").GetComponent<bgmusic>().calm();
        } catch (Exception e)
        {
            Debug.Log("No bg music");
        }
        play();
    }
    public void play()
    {
        Time.timeScale = 1.0f;
        AudioListener.volume = 1;
        try
        {
            GameObject.Find("Pause").SetActive(false);
        } catch (Exception e)
        {

        }
        try
        {
            GameObject.Find("Canvas").transform.Find("Reset Level").gameObject.SetActive(true);
        } catch (Exception e)
        {

        }


    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                AudioListener.volume = 0;
                Debug.Log(reset.transform.position);

                reset.SetActive(false);
            } else
            {
                Debug.Log("Pause menu is null");
            }
            
        }
    }
    public void Start()
    {
        try
        {
            reset = GameObject.Find("Reset Level");
        } catch (Exception e)
        {

        }

        //al = GameObject.Find("Main Camera").GetComponent<AudioListener>();
    }

    

    public void gameQuit()
    {
        Application.Quit();
    }
}