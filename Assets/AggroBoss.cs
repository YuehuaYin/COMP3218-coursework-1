using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroBoss : MonoBehaviour
{
    public GameObject boss;
    public SpriteRenderer sp;
    private bool aggro = false;
    public GameObject player;
    private float timer = 0;
    private bool increase = true;
    public float max;
    private Rigidbody2D rb;
    public GameObject target;
    private float targetTimer;
    private bool seePlayer = false;
    public GameObject bulletPrefab;
    private bool targetOn = false;
    private bool bulletReady = true;
    private TargetScript ts;
    public float shootCooldown;
    public AudioSource shootSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = boss.GetComponent<Rigidbody2D>();
        ts = target.GetComponent<TargetScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (increase)
        {
            timer += Time.deltaTime;
            if (timer > 2*max) { 
            increase = false;
            }
        } else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                increase = true;
            }
        }
        Vector2 dir = Vector2.zero;
        if (rb.velocity != Vector2.zero)
        {
            if (targetOn)
            {
                targetOn = false;
                targetTimer = 0;
                bulletReady = true;
                ts.disappear();
            }
            dir = rb.velocity;
            aggro = false;
            sp.color = Color.blue;
            
        } else if (aggro)
        {
            dir = player.transform.position - transform.position;
        } else
        {
            dir = new Vector2(-1,max-timer);
            sp.color = new Color(1f, 0.72f, 0f, 0.5f);
        }
        if (dir.magnitude > 0.3 && !aggro || (dir.x != 0 && dir.y != 0))
        {
            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

        }

        if (seePlayer)
        {
            Vector2 direction = player.transform.position - transform.position;
            int mask = 1 << 9;
            RaycastHit2D wallDetect = Physics2D.Raycast(transform.position, direction, direction.magnitude, mask);
            if (wallDetect.collider == null && !aggro && !player.GetComponent<PlayerControls>().getInvis())
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
                if (!targetOn)
                {
                    targetTimer = 0;
                    target.transform.position = player.transform.position;
                    target.SetActive(true);
                    ts.appear();
                }
                //sp.color = new Color(1f, 0.72f, 0f, 0.5f);
                sp.color = Color.red;
                targetOn = true;
                transform.localScale = Vector3.one * 1.6f;
                
                
                
            }
            else if (aggro && wallDetect.collider != null)
            {
                Debug.Log(wallDetect.collider);
                unaggro();

            }
        }

        if (player.GetComponent<PlayerControls>().getInvis() && aggro)
        {
            unaggro();
        }

        if (targetOn)
        {
            targetTimer += Time.deltaTime;
            if (targetTimer > shootCooldown +1)
            {
                targetTimer = 0;
                bulletReady = true;
                if (!aggro)
                {
                    targetOn = false;
                    ts.disappear();
                }
                else
                {
                    ts.appear();
                    target.transform.position = player.transform.position;
                }
            }
            else if (targetTimer > shootCooldown && bulletReady)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                ts.shoot();
                shootSound.Play();

                
                bulletReady = false;
            } 
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            seePlayer = true;
           
            

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            seePlayer = false;
            if (aggro)
            {

                unaggro();
            }
        }
    }

    public void unaggro()
    {
        try
        {
            if (aggro)
            {
                GameObject.Find("BGmusic").GetComponent<bgmusic>().unaggro();
                
            }
        }
        catch (Exception e)
        {
            Debug.Log("BG music not available unless you start from home scene");
        }
        aggro = false;
        sp.color = Color.blue;
        transform.localScale = Vector3.one * 1.5f;


        timer = max - (player.transform.position.y - transform.position.y) / -(player.transform.position.x - transform.position.x);

        Debug.Log(timer);

    }
}
