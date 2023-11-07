using Minifantasy;
using Minifantasy.Dungeon;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ThiefAnimation2 : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject characterContainer;
    private SetAnimatorParameter animator;
    private bool alive = true;
    private string animMode = "Idle";
    public float invisTimer;
    public GameObject model;
    private SpriteRenderer sp;
    private float damageTimer;
    // Start is called before the first frame update
    void Start()
    {
        animator = characterContainer.GetComponent<SetAnimatorParameter>();
        //animator = characterContainer.GetComponent<DUN_AnimatedCharacterSelection>();
        rb = GetComponent<Rigidbody2D>();
        sp = model.GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            return;
        }
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        } else if (rb.velocity != Vector2.zero && alive && animMode != "Walk")
        {
            //animator.TurnOffCurrentParameter();
            animator.ToggleAnimation("Walk");
            animMode = "Walk";
            //Debug.Log("Walk started");
            Debug.Log("Velocity: " + rb.velocity);
        }
        else if (rb.velocity.magnitude == 0 && alive && animMode != "Idle")
        {
            //animator.TurnOffCurrentParameter();
            animator.ToggleAnimation("Idle");
            animMode = "Idle";

            //Debug.Log("Idle started");
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
        float ratio = rb.velocity.y;
        if (rb.velocity.x != 0)
        {
            ratio = rb.velocity.y / Mathf.Abs(rb.velocity.x);
        }
        if (ratio > 0.2)
        {
            animator.ToggleYDirection(1);
        }
        else if (rb.velocity.y < 0)
        {
            animator.ToggleYDirection(-1);
        }
        if (invisTimer > 0)
        {
            invisTimer -= Time.deltaTime;
            if ((int)(invisTimer * 10) % 2 == 0 && invisTimer < 1)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
            }
            else
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.5f);
            }
            if (invisTimer <= 0)
            {
                invisTimer = 0;
                
                sp.enabled = true;
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
            }
        }
    }

    public void left()
    {
        animator.ToggleXDirection(-1);
    }
    public void Damage()
    {
        damageTimer = 0.5f;
        animator.ToggleAnimation("Dmg");
        animMode = "Dmg";
    }
    public void death()
    {
        animator.ToggleAnimation("Die");
        animMode = "Die";
        alive = false;
    }
}
