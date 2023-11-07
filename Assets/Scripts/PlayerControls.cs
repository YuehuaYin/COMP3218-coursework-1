using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using TMPro;
using Minifantasy.Dungeon;
using Unity.VisualScripting;
using Minifantasy;

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
        if (Input.GetMouseButtonDown(1))
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
                DeathCounter.deaths+=1;
                deathText.text = DeathCounter.deaths.ToString();
                sceneSwitcher.restartScene();
            }
        }
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
            Destroy(collision.gameObject);
        } else if (collision.CompareTag("InvisibilityPotion"))
        {
            invisSound.Play();
            invis = true;
            invisTimer = 5;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.5f);
        }
    } 

    public void death()
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
