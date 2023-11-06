using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Vector2 dir2 = GameObject.Find("Target").transform.position - transform.position;
        dir2.Normalize();
        rb.velocity = dir2 *speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDirection(Vector2 dir)
    {
        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")|| collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
