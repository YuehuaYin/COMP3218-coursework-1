using Minifantasy;
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
    public float hp = 1;
    public GameObject healthbar;
    public GameObject sight;
    public GameObject characterContainer;
    private SetAnimatorParameter animator;
    private bool alive = true;
    private string animMode = "";
    private float deathTimer = 0;
    private float aggroTimer = 1;
    private bool wallTouch = false;
    public AudioSource deathSound;
    private bool invis = false;
    private bool boxTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = characterContainer.GetComponent<SetAnimatorParameter>();
        
        //animator = characterContainer.GetComponent<DUN_AnimatedCharacterSelection>();
        
        player = GameObject.Find("Player");
        Quaternion direction = sight.transform.rotation;
        Debug.Log(direction);
        
        if (direction.z <= -0.5 || direction.z >= 0.8)
        {
            animator.ToggleXDirection(-1);
        } else if (direction.z > 0.65 && direction.z < 0.8 && direction.w > 0)
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
            if (player.GetComponent<PlayerControls>().getInvis() && alive)
            {
                aggro = false;
                rb.velocity = Vector2.zero;
                sight.GetComponent<SpriteRenderer>().color = Color.blue;
                try
                {
                    
                        GameObject.Find("BGmusic").GetComponent<bgmusic>().unaggro();
                    
                }
                catch (Exception e)
                {
                    Debug.Log("BG music not available unless you start from home scene");
                }
            }
            else
            {
                if (aggroTimer < 10 && !wallTouch)
                {
                    aggroTimer += Time.deltaTime;
                }
                Vector2 direct = player.transform.position - transform.position;
                if (direct.magnitude < 0.1)
                {
                    rb.velocity = Vector2.zero;
                }
                else if (alive)
                {
                    direct.Normalize();
                    rb.velocity = direct * speed * aggroTimer * 0.5f;
                }
            }
           // sight.transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        
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
        /*} catch(Exception e)
        {
            Debug.Log("The enemy animation broke");
            Debug.LogException(e);
            animator.ToggleAnimation("Walk");
            animMode = "Walk";
        }
        Debug.Log(animator.GetAnimation());
        */

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
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (sight != null)
        {
            if (player.GetComponent<PlayerControls>().getInvis() && !invis)
            {
                sight.GetComponent<PolygonCollider2D>().enabled = false;
                invis = true;
            }
            else if (invis)
            {
                sight.GetComponent<PolygonCollider2D>().enabled = true;
                invis = false;
            }
        }
        if (!alive)
        {
            rb.velocity = Vector2.zero;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Enemy hit wall");
            wallTouch = true;
            aggroTimer = 2;
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            boxTouch = true;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Enemy left wall");
            wallTouch = false;
            aggroTimer = 2;
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            boxTouch = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Bullet"))
        {
            Death();
        }
        else if (collision.CompareTag("Hazard"))
        {
            Death();
        }
        
        
    } 

    public void Aggro()
    {
        if (!aggro)
        {
            aggro = true;
            try
            {
                GameObject.Find("BGmusic").GetComponent<bgmusic>().intense();
            }
            catch (Exception e)
            {
                Debug.Log("BG music not available unless you start from home scene");
            }
        }
    }

    public bool getAggro()
    {

        return aggro|| !alive;
    }
    public bool touchingBox()
    {
        return boxTouch && alive && !aggro;
    }

    private void Death()
    {
        if (alive) {

            DeathCounter.score += 100;
            try
            {
                GameObject.Find("Canvas").transform.Find("Score").transform.Find("ScoreCount").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = DeathCounter.score.ToString();
                GameObject.Find("Canvas").transform.Find("Score").transform.Find("ScoreIncrease").gameObject.GetComponent<ScoreIndication>().scoreChange(100, false);
            } catch (Exception e)
            {
                Debug.Log("Couldn't find score object");
            }

        alive = false;
        // animator.TurnOffCurrentParameter();
        animator.ToggleAnimation("Die");
        deathTimer = 1.4f;
        rb.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(sight);
        deathSound.Play();
        
        try
        {
                if (aggro)
                {
                    GameObject.Find("BGmusic").GetComponent<bgmusic>().unaggro();
                    aggro = false;
                }
        }
        catch (Exception e)
        {
            Debug.Log("BG music not available unless you start from home scene");
        }
        }
    }


}
