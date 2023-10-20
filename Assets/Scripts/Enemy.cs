using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private bool aggro = false;
    public float speed;
    public Rigidbody2D rb;
    public float hp;
    public GameObject healthbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            
            Vector2 direct = player.transform.position - transform.position;
            
            direct.Normalize();
            rb.velocity = direct * speed;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Bullet"))
        {
            healthbar.GetComponent<Health>().Damage(1);
        }
        
    }

    public void Aggro()
    {
        aggro = true;
    }


    
}
