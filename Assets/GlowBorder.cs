using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowBorder : MonoBehaviour
{
    public SpriteRenderer im;
    private bool red2 = true;
    private float blackTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(DeathCounter.glowTimer);
        if (red2)
        {
            im.color = new Color(1, 0, 0, DeathCounter.glowTimer * 0.5f);
        } else if (blackTimer > 0) { 
        
                blackTimer -= Time.deltaTime;
            im.color = new Color(0, 0, 0, blackTimer);
        }
    }

    public void black()
    {
        red2 = false;
        im.color = new Color(0, 0, 0, DeathCounter.glowTimer * 0.5f);
        blackTimer = 1;
    }

    public void red() {
        red2 = true;
        im.color = new Color(1, 0, 0, DeathCounter.glowTimer * 0.5f);
        blackTimer = 0;
    }
}
