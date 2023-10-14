using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");

    rb.velocity = new Vector2(x*speed, y*speed);

    }
}
