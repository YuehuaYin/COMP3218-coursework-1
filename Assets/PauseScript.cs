using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseButton()
    {

            GameObject.Find("Pause").SetActive(true);
            Time.timeScale = 0;
            AudioListener.volume = 0;
            GameObject.Find("Canvas").transform.Find("Reset Level").gameObject.SetActive(false);
        gameObject.SetActive(false);

    }
}
