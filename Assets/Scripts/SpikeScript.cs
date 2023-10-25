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
        } else {
            GetComponent<SpriteRenderer>().sprite = spr2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Obstacle is turned on
    void activate()
    {

    }
}
