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
    // Start is called before the first frame update
    void Start()
    {
        animator = characterContainer.GetComponent<SetAnimatorParameter>();
        //animator = characterContainer.GetComponent<DUN_AnimatedCharacterSelection>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector2.zero && alive && animMode != "Walk")
        {
            //animator.TurnOffCurrentParameter();
            animator.ToggleAnimation("Walk");
            animMode = "Walk";
            Debug.Log("Walk started");
            Debug.Log("Velocity: " + rb.velocity);
        }
        else if (rb.velocity.magnitude == 0 && alive && animMode != "Idle")
        {
            //animator.TurnOffCurrentParameter();
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
    }
}
