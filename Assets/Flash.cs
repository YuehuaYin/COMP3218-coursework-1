using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public SpriteRenderer im;
    private float flashTimer;
    private Color c;
    private float flashMultiplier;
    private float timer;
    public GameObject glow;

    private GlowBorder gb;
    // Start is called before the first frame update
    void Start()
    {
        gb = glow.GetComponent<GlowBorder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashTimer > 0)
        {
            timer -= Time.deltaTime * 1/flashTimer;
            im.color = new Color(c.r, c.g, c.b, timer * flashTimer);
            
        }
    }


    public void whiteflash()
    {
        c = Color.white;
        flashTimer = 0.4f;
        timer = 0.5f;
        
    }
    public void blackflash()
    {
        c = Color.black;
        flashTimer = 0.5f;
        timer = 2;
        DeathCounter.glowTimer = 0;
        gb.black();
    }

}
