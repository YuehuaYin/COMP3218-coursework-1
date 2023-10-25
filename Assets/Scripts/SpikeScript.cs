using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Sprite spr1, spr2;
    public bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        if (isActivated){
            GetComponent<SpriteRenderer>().sprite = spr1;
            GetComponent<BoxCollider2D>().enabled = true;
        } else {
            GetComponent<SpriteRenderer>().sprite = spr2;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Obstacle is turned on
    void activate()
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
