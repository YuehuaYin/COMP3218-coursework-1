using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public GameObject videoplayer;
    private UnityEngine.Video.VideoPlayer video;
    public GameObject button;
    public GameObject skipbutton;
    private GameObject reset;
    private int initialScore;
    public GameObject selevelLvl;
    public GameObject selectButton;
    
    public void nextScene(){

        DeathCounter.ResetTimer();
        DeathCounter.prevScore = DeathCounter.score;

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
        GameObject.Find("SelectLevel").SetActive(false);
        skipbutton.SetActive(true);
        video = videoplayer.GetComponent<UnityEngine.Video.VideoPlayer>();
        video.Play();
        video.loopPointReached += EndReached;
        GameObject.Find("Canvas").transform.Find("Quit").transform.gameObject.SetActive(false);
    }

    void EndReached(UnityEngine.Video.VideoPlayer video)
    {
        SceneManager.LoadScene("01");
    }

    public void skipButton()
    {
        EndReached(video);
    }


    public void restartScene(){
        //DeathCounter.ResetTimer();
        DeathCounter.deaths += 1;
        /*
        if (DeathCounter.score > initialScore)
        {
            Debug.Log("Initial score " + initialScore);
            DeathCounter.score = initialScore;
        }*/
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        try
        {
            GameObject.Find("BGmusic").GetComponent<bgmusic>().calm();
        } catch (Exception e)
        {
            Debug.Log("No bg music");
        }
    }

    public void Start()
    {

        if (DeathCounter.prevScore < DeathCounter.score)
        {
            DeathCounter.score = DeathCounter.prevScore;
        }
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

    public void restartGame()
    {
        DeathCounter.deaths = 0;
        DeathCounter.score = 0;
        DeathCounter.timer = 0;
        DeathCounter.prevTimerScore = 0;
        DeathCounter.prevScore = 0;
        DeathCounter.totalTime = 0;
        DeathCounter.glowTimer = 0;
        SceneManager.LoadScene("01");
    }

    public void home(){
        DeathCounter.deaths = 0;
        DeathCounter.score = 0;
        DeathCounter.timer = 0;
        DeathCounter.prevTimerScore = 0;
        DeathCounter.prevScore = 0;
        DeathCounter.totalTime = 0;
        DeathCounter.glowTimer = 0;
        AudioListener.volume = 1;
        SceneManager.LoadScene("Home");
        Destroy(GameObject.Find("BGmusic"));
    }

    public void selectLevelButton(){
        button.SetActive(false);
        selectButton.SetActive(false);
        selevelLvl.SetActive(true);
    }

    public void backbutton(){
        button.SetActive(true);
        selectButton.SetActive(true);
        selevelLvl.SetActive(false);
    }

    public void selectLevel(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

}