using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Sprite spr1, spr2;
    public bool isActivated;
    public LeverScript lever1;
    public LeverScript lever2;
    // Start is called before the first frame update
    void Start()
    {
        // add activate function to lever's list of functions
        if (lever1 != null) {
            lever1.leverActivated.AddListener(activate);
        }
        if (lever2 != null) {
            lever2.leverActivated.AddListener(activate);
        }

        if (isActivated){
            GetComponent<SpriteRenderer>().sprite = spr1;
            GetComponent<BoxCollider2D>().enabled = true;
        } else {
            GetComponent<SpriteRenderer>().sprite = spr2;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Obstacle is turned on
    public void activate()
    {
        if (!isActivated){
            GetComponent<SpriteRenderer>().sprite = spr1;
            GetComponent<BoxCollider2D>().enabled = true;
            isActivated = true;
        } else {
            GetComponent<SpriteRenderer>().sprite = spr2;
            GetComponent<BoxCollider2D>().enabled = false;
            isActivated = false;
        }
    }
}
