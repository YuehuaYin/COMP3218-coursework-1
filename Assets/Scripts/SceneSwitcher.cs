using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public void nextScene(){
        switch (SceneManager.GetActiveScene().name) 
        {
        case "01":
            SceneManager.LoadScene("02");
            break;
        case "02":
            SceneManager.LoadScene("03");
            break;
        }
    }

    public void startButton()
    {
        SceneManager.LoadScene("01");
    }

    public void restartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}