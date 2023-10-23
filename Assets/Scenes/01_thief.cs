using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief_room_1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1*speed, 0*speed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Goal"))
        {
            Destroy(this.gameObject);
        }
    } 
}
