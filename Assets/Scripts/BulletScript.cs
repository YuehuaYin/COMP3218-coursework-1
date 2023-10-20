using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector2 move;
    public float speed;
    void Start()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouse - transform.position;
        direction.z = 0;
        move = Vector2.ClampMagnitude(direction, speed);
        Debug.Log(move);
    }

    void Update() 
    {
        transform.Translate(move);
        //check for collisions
    }
}
