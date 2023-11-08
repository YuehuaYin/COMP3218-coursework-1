using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ThiefBossScript : MonoBehaviour
{
    private int hp = 3;
    public GameObject healthBar;
   // private Health health;
    private Rigidbody2D rb;
    public GameObject path2;
    public GameObject path3;
    private float timer = 2;
    public GameObject model;
    private SpriteRenderer sp;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    private BoxCollider2D bc;
    private bool still = false;
    private bool alive = true;
    private float deathTimer = 0;
    public GameObject player;
    public GameObject sight;
    public GameObject target;
    public AudioSource hitSound;
    public AudioSource deadSound;
    public GameObject ammoPickup;


    // Start is called before the first frame update
    void Start()
    {
        //health = healthBar.GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        sp = model.GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>(); 
    }

    // Update is called once per frame
    void Update()
    {


        if (rb.velocity == Vector2.zero)
        {
            
            //Debug.Log("Velocity zero");
            GetComponent<ThiefAnimation2>().left();
        } else
        {
            timer += Time.deltaTime;
            if ((int)(timer * 10) % 2 == 0 && timer < 1.5f && timer > 0.5f)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
            } else
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
            }
            if (timer >= 1.5f && !bc.enabled)
            {
                bc.enabled = true;
            }

            
            //Debug.Log(rb.velocity.ToString());
        }
        if (still)
        {
            rb.velocity = Vector2.zero;
        }
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                player.GetComponent<SceneSwitcher>().nextScene();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Hazard"))
        {
            if (timer != 0) {
                timer = 0;
                Damage();
            }
        }
        if (collision.CompareTag("AmmoPickup"))
        {
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            still = true;
        }
    }
    private void Damage()
    {
        hp--;
        try
        {
            GameObject.Find("Canvas").transform.Find("Flash").gameObject.GetComponent<Flash>().whiteflash();
        } catch { }
        
        GetComponent<ThiefAnimation2>().Damage();
        
        bc.enabled = false;

        switch (hp)
        {
            case 2:
                path2.SetActive(true);
                Destroy(heart3);
                hitSound.Play();
                if (ammoPickup != null)
                {
                    Destroy(ammoPickup);
                }
                break;
            case 1: 
                path3.SetActive(true);
                Destroy(heart2);
                hitSound.Play();
                break;
            case 0:
                Destroy(heart1);
                alive = false;
                still = true;
                deathTimer = 1.4f;
                GetComponent<ThiefAnimation2>().death();
                sight.GetComponent<AggroBoss>().unaggro();
                Destroy(sight);
                Destroy(target);
                deadSound.Play();
                break;
              
        }


    }
}
