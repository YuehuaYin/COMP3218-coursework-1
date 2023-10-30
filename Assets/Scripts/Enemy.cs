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
    public float hp = 1;
    public GameObject healthbar;
    public GameObject sight;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            
            Vector2 direct = player.transform.position - transform.position;
            
            direct.Normalize();
            rb.velocity = direct * speed;
           // sight.transform.rotation = Quaternion.LookRotation(rb.velocity);
        }


    }

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Bullet"))
        {
            healthbar.GetComponent<Health>().Damage(1);
        }
        else if (collision.gameObject.CompareTag("Melee"))
        {
            healthbar.GetComponent<Health>().Damage(2);
        }
        
        Debug.Log("Enemy hit by bullet");
    }  */
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Bullet"))
        {
            healthbar.GetComponent<Health>().Damage(1);
        }
        else if (collision.CompareTag("Hazard"))
        {
            healthbar.GetComponent<Health>().Damage(1);
        }
        
    } 

    public void Aggro()
    {
        aggro = true;
    }


    
}
