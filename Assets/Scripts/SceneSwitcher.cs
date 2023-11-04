using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    //public GameObject music;
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
        GameObject.Find("BGmusic").GetComponent<bgmusic>().calm();
    }

    public void startButton()
    {
        SceneManager.LoadScene("01");
        
    }

    public void restartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameObject.Find("BGmusic").GetComponent<bgmusic>().calm();
    }
}