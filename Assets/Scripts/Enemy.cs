using Minifantasy.Dungeon;
using System;
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
    public float hp = 10;
    public GameObject healthbar;
    public GameObject sight;
    public GameObject characterContainer;
    private DUN_AnimatedCharacterSelection animator;
    private bool alive = true;
    private string animMode = "Idle";
    private float deathTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = characterContainer.GetComponent<DUN_AnimatedCharacterSelection>();
        player = GameObject.Find("Player");
        Quaternion direction = sight.transform.rotation;
        Debug.Log(direction.z);
        if (direction.z <= -0.5 || direction.z >= 0.8)
        {
            animator.ToggleXDirection(-1);
        } else if (direction.z > 0.65 && direction.z < 0.8)
        {
            Debug.Log("Facing up");
            animator.ToggleYDirection(1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (aggro)
        {
            
            Vector2 direct = player.transform.position - transform.position;
            if (direct.magnitude < 0.1)
            {
                rb.velocity = Vector2.zero;
            }
            else if (alive)
            {
                direct.Normalize();
                rb.velocity = direct * speed;
            }
           // sight.transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

        if (rb.velocity != Vector2.zero && alive && animMode != "Walk")
        {
            animator.TurnOffCurrentParameter();
            animator.ToggleAnimation("Walk");
            animMode = "Walk";
            Debug.Log("Walk started");
            Debug.Log("Velocity: " + rb.velocity);
        }
        else if (rb.velocity.magnitude == 0 && alive && animMode != "Idle")
        {
            animator.TurnOffCurrentParameter();
            animator.ToggleAnimation("Idle");
            animMode = "Idle";

            Debug.Log("Idle started");
            Debug.Log("Velocity: " + rb.velocity);
        }

        if (rb.velocity.x > 0)
        {
            animator.ToggleXDirection(1);
        }
        else if (rb.velocity.x < 0)
        {
            animator.ToggleXDirection(-1);

        }
        if (rb.velocity.y > 0)
        {
            animator.ToggleYDirection(1);
        }
        else if (rb.velocity.y < 0)
        {
            animator.ToggleYDirection(-1);
        }
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                Destroy(gameObject);
            }
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

            Death();
            
        }
        
        
    } 

    public void Aggro()
    {
        aggro = true;
    }

    private void Death()
    {
        alive = false;
        animator.TurnOffCurrentParameter();
        animator.ToggleAnimation("Die");
        deathTimer = 1.5f;
        rb.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(sight);
    }

    
}
