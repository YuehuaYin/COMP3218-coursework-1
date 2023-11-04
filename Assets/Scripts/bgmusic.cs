using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class bgmusic : MonoBehaviour
{
    public AudioSource calmSong;
    public AudioSource intenseSong;
    private string mode = "calm";
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void intense()
    {
        if (mode != "intense")
        {
            //calmSong.pitch = 1.5f;
            //calmSong.volume = 0.5f;
            calmSong.reverbZoneMix = 0;
            mode = "intense";
        }
    }

    public void calm()
    {
        if (mode != "calm")
        {
            calmSong.pitch = 1;
            calmSong.volume = 0.2f;
            calmSong.reverbZoneMix = 1;
            mode = "calm";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == "intense" && calmSong.pitch < 1.5f)
        {
            calmSong.pitch += Time.deltaTime *0.5f;
            calmSong.volume += Time.deltaTime *0.3f;

        }
    }
}
