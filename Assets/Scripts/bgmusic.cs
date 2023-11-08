using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class bgmusic : MonoBehaviour
{
    public AudioSource calmSong;
    public AudioSource intenseSong;
    private string mode = "calm";
    private int aggroCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void unaggro()
    {
        aggroCount--;
        Debug.Log("aggro count " + aggroCount);
        if (aggroCount <= 0 )
        {
            calm();
        }
    }


    public void intense()
    {
        aggroCount++;
        if (mode != "intense")
        {

            //calmSong.pitch = 1.5f;
            //calmSong.volume = 0.5f;
            calmSong.reverbZoneMix = 0;
            mode = "intense";
            DeathCounter.glowTimer = 0;
            try
            {
                GameObject.Find("Canvas").transform.Find("GlowBorder").GetComponent<GlowBorder>().red();
            } catch (Exception e)
            {

            }
        }
    }

    public void calm()
    {
        aggroCount = 0;
        if (mode != "calm")
        {
            //calmSong.pitch = 1;
            //calmSong.volume = 0.2f;
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
            DeathCounter.glowTimer += Time.deltaTime;

        } else if (mode == "calm" && calmSong.pitch > 1)
        {
            calmSong.pitch -= Time.deltaTime * 0.5f;
            calmSong.volume -= Time.deltaTime * 0.3f;
            DeathCounter.glowTimer -= Time.deltaTime;

        }
    }
}
