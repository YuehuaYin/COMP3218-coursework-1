using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using TMPro;
using Minifantasy.Dungeon;
using Unity.VisualScripting;
using Minifantasy;
using System;

public class PlayerControls : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float gunCooldown;
    public float meleeCooldown;
    public GameObject bulletPrefab;
    public MeleeAttack meleeAttack;
    private float gunTimer;
    private float meleeTimer;
    public GameObject healthbar;
    public float knockForce;
    private float invincibilityTimer;
    private bool invincible = false;
    public float dashSpeed;
    private float dashTimer = 0;
    private SceneSwitcher sceneSwitcher;
    private TMPro.TextMeshProUGUI ammo;
    private TMPro.TextMeshProUGUI deathText;
    public GameObject ammoTextObject;
    public GameObject deathTextObject;
    public GameObject characterContainer;
    private SetAnimatorParameter animator;
    private bool alive = true;
    private string animMode = "Idle";
    private float deathTimer = 0;
    public AudioSource deathSound;
    public AudioSource walkSound;
    private float walkVolume = 0;
    private bool invis = false;
    private float invisTimer = 0;
    private SpriteRenderer sp;
    public GameObject model;
    private bool reset = false;
    public AudioSource invisSound;
    private TMPro.TextMeshProUGUI timerText;
    private TMPro.TextMeshProUGUI scoreText;
    private float timerTimer = 0;
    private int initialScore = 0;
    private ScoreIndication si;
    public AudioSource coinSound;
    private GameObject ammoPickup;
    private float ammoRespawnTimer = 0;
    private bool shoot = false;
    private Flash fl;


    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        sceneSwitcher = GetComponent<SceneSwitcher>();
        gunTimer = gunCooldown;
        meleeTimer = meleeCooldown;
        ammo = ammoTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        deathText = deathTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        deathText.text = DeathCounter.deaths.ToString();
        //animator = characterContainer.GetComponent<DUN_AnimatedCharacterSelection>();
        animator = characterContainer.GetComponent<SetAnimatorParameter>();
        sp = model.GetComponent<SpriteRenderer>();
        
      //  try
      //  {
            timerText = GameObject.Find("Canvas").transform.Find("Time").transform.Find("TimeCount").gameObject.GetComponent<TMPro.TextMeshProUGUI>();

            timerText.text = DeathCounter.timer.ToString();
            timerTimer = DeathCounter.timer;
            scoreText = GameObject.Find("Canvas").transform.Find("Score").transform.Find("ScoreCount").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            scoreText.text = DeathCounter.score.ToString();
            initialScore = DeathCounter.score;
            si = GameObject.Find("Canvas").transform.Find("Score").transform.Find("ScoreIncrease").gameObject.GetComponent<ScoreIndication>();
        fl = GameObject.Find("Canvas").transform.Find("Flash").gameObject.GetComponent<Flash>();
        
        if (DeathCounter.timer == 0 && DeathCounter.prevTimerScore > 0)
            {
                si.scoreChange(DeathCounter.prevTimerScore, true);
                Debug.Log("Time score");
                DeathCounter.prevTimerScore = 0;
            }
    //    }
      /*  catch (Exception e)
        {
            Debug.Log("Couldn't find score object");
            Debug.LogError(e);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector2.zero && alive && animMode != "Walk")
        {
           // animator.TurnOffCurrentParameter();
            animator.ToggleAnimation("Walk");
            animMode = "Walk";
            Debug.Log("Walk started");
            Debug.Log("Velocity: " + rb.velocity);
            walkSound.volume = 0.1f;
            walkVolume = 0;
        }
        else if (rb.velocity.magnitude == 0 && alive && animMode != "Idle")
        {
           // animator.TurnOffCurrentParameter();
            animator.ToggleAnimation("Idle");
            animMode = "Idle";

            Debug.Log("Idle started");
            Debug.Log("Velocity: " + rb.velocity);

            walkVolume = 0.1f;
        }
        if (walkVolume > 0)
        {
            walkVolume -= Time.deltaTime;
            if (walkVolume < 0) walkVolume = 0;
            walkSound.volume = walkVolume;
        }

        if (rb.velocity.x > 0)
        {
            animator.ToggleXDirection(1);
        } else if (rb.velocity.x < 0)
        {
            animator.ToggleXDirection(-1);

        } 
        if (rb.velocity.y > 0)
        {
            animator.ToggleYDirection(1);
        } else if (rb.velocity.y < 0) {
            animator.ToggleYDirection(-1);
        }

        // increase timers
        gunTimer += Time.deltaTime;
        meleeTimer += Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (alive)
        {
            rb.velocity = new Vector2(x * speed, y * speed);
        } else
        {
            rb.velocity = Vector2.zero;
        }

        //new movement to allow for knockback and dash?
        /*
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }*/

        // Left click to fire gun
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        // Right Click to melee
        if (Input.GetKey(KeyCode.Space))
        {
            Melee();
        }

        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
            if ((int)(invincibilityTimer *10) % 2 ==0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            if (invincibilityTimer <= 0)
            {
                invincibilityTimer = 0;
                invincible = false;
                GetComponent<SpriteRenderer>().enabled = true;
            }
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
                invis = false;
                sp.enabled = true;
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
            }
        }



        /*if (Input.GetKey(KeyCode.Space) && dashTimer <= 0 && rb.velocity != Vector2.zero)
        {
            //Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 direction = mouse - transform.position;

            Vector2 direction = rb.velocity;
            
            direction.Normalize();
            //rb.velocity = direction * dashSpeed;
            int mask = 1 << 9;
            RaycastHit2D wallDetect = Physics2D.Raycast(rb.position, direction, dashSpeed, mask);
            float rayMagnitude = 1;
            /*while (wallDetect.collider != null && rayMagnitude > 0.1)
            {
                rayMagnitude *= 0.9f;
                wallDetect = Physics2D.Raycast(rb.position, direction, dashSpeed * rayMagnitude, 9);
            }
            if (wallDetect.collider == null)
            {
                transform.position += new Vector3(direction.x * dashSpeed * rayMagnitude, direction.y * dashSpeed *rayMagnitude, 0);
            }
            else
            {
                Debug.Log("Ray hit at" +  wallDetect.point);
                transform.position = wallDetect.point;
            }

           // rb.AddForce(direction * dashSpeed, ForceMode2D.Impulse);
            
            dashTimer = 2f;
            Debug.Log("Dash");

        }
        else if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
           
        }*/

        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <=0)
            {           
                deathText.text = DeathCounter.deaths.ToString();
                sceneSwitcher.restartScene();
            }
        }

        timerTimer += Time.deltaTime;
        if (timerText != null)
        {
            if (DeathCounter.timer >= (int)timerTimer - 1)
            {
                DeathCounter.timer = (int)timerTimer;
            }
            timerText.text = ((int)timerTimer).ToString();
        }

        if (shoot)
        {
            if (ammoRespawnTimer > 0) {
                ammoRespawnTimer -= Time.deltaTime;
            } else
            {
                shoot = false;
                if (ammoPickup != null)
                {
                    ammoPickup.GetComponent<AmmoPickupScript>().respawnObj();
                }
            }
        }
        DeathCounter.totalTime += Time.deltaTime;
    }

    // Fire ranged attack
    public void Fire()
    {
        int ammoCount = int.Parse(ammo.text);
        if (gunTimer >= gunCooldown && ammoCount > 0){
            Debug.Log("fire");
            gunTimer = 0f;
            
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            ammoCount--;
            ammo.text = ammoCount.ToString();
            shoot = true;
            ammoRespawnTimer = 2;
        }
    }

    // Melee attack
    public void Melee()
    {
        if (meleeTimer >= meleeCooldown) {
            Debug.Log("melee");
            meleeTimer = 0f;
            meleeAttack.attack();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
       if ((collision.gameObject.CompareTag("Enemy") || (collision.gameObject.CompareTag("Boss"))) && !invincible)
        {
            /*
            healthbar.GetComponent<Health>().Damage(1);
            Vector2 knock = transform.position - collision.transform.position;
            knock.Normalize();
            rb.AddForce(knock * knockForce, ForceMode2D.Impulse);
            //rb.velocity = (knock * knockForce);
            invincible = true;
            invincibilityTimer = 1f;
            */

            death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Goal"))
        {
            int timerScore = 200 - 5 * DeathCounter.timer;
            if (timerScore < 0)
            {
                timerScore = 0;
            }
            DeathCounter.score += timerScore;
            DeathCounter.prevTimerScore = timerScore;
            DeathCounter.timer = 0;
            Debug.Log("Timer: " + DeathCounter.timer);
            sceneSwitcher.nextScene();
        }
        else if (collision.CompareTag("Hazard"))
        {
            death();
        }
        else if (collision.CompareTag("AmmoPickup"))
        {
            int ammoCount = int.Parse(ammo.text);

            ammoCount++;
            ammo.text = ammoCount.ToString();
            ammoPickup = collision.gameObject;
            //Destroy(collision.gameObject);
            if (fl != null)
            {
                fl.whiteflash();
            }
        } else if (collision.CompareTag("InvisibilityPotion"))
        {
            invisSound.Play();
            invis = true;
            invisTimer = 5;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.5f);
            if (fl != null)
            {
                fl.whiteflash();
            }
        } else if (collision.CompareTag("Coin"))
        {
            coinSound.Play();
            DeathCounter.score += 200;
            Destroy(collision.gameObject);

            if (scoreText != null)
            {
                scoreText.text = DeathCounter.score.ToString();
            }
            if (si != null)
            {
                si.scoreChange(200, false);
            }
            if (fl != null)
            {
                fl.whiteflash();
            }
        }
    } 

    public void death()
    {
        if (alive)
        {
            deathSound.Play();
            Debug.Log("dead");

            alive = false;
            //  animator.TurnOffCurrentParameter();
            animator.ToggleAnimation("Die");
            deathTimer = 1.4f;
            rb.velocity = Vector2.zero;
            GetComponent<BoxCollider2D>().enabled = false;

            walkSound.Pause();
            if (initialScore - 50 < 0)
            {
                si.scoreChange(initialScore - DeathCounter.score, false);
            }
            else
            {
                si.scoreChange(initialScore - 50 - DeathCounter.score, false);
            }
            DeathCounter.score = initialScore-50;
            
            if (DeathCounter.score < 0)
            {
                DeathCounter.score = 0;
            }
            if (scoreText != null)
            {
                scoreText.text = DeathCounter.score.ToString();
            }
            if (fl != null)
            {
                fl.blackflash();
            }
        }
    }

    public void resetButton()
    {
        //DeathCounter.deaths += 1;
        //deathText.text = DeathCounter.deaths.ToString();
        sceneSwitcher.restartScene();
    }

    public bool getInvis()
    {
        return invis;
    }


}
