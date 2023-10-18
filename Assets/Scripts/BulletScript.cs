using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mouse;
    public float speed;
    void Start()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update() 
    {
        Debug.Log(mouse.normalized.x);
// gameObject.velocity = new Vector2(x*speed, y*speed);
    }
}
