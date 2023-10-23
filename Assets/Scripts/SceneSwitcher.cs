using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void getNextScene(){
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void nextScene(){
        getNextScene();        
    }

    public void startButton()
    {
        SceneManager.LoadScene("01");
    }
}