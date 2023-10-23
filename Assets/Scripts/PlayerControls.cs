using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private SceneSwitcher sceneSwitcher;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sceneSwitcher = GetComponent<SceneSwitcher>();
        gunTimer = gunCooldown;
        meleeTimer = meleeCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        // increase timers
        gunTimer += Time.deltaTime;
        meleeTimer += Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(x*speed, y*speed);

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

    }

    // Fire ranged attack
    public void Fire()
    {
        if (gunTimer >= gunCooldown){
            Debug.Log("fire");
            gunTimer = 0f;
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform);
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
        
       if (collision.gameObject.CompareTag("Enemy") && !invincible)
        {
            healthbar.GetComponent<Health>().Damage(1);
            Vector2 knock = transform.position - collision.transform.position;
            knock.Normalize();
            rb.AddForce(knock * knockForce, ForceMode2D.Impulse);
            //rb.velocity = (knock * knockForce);
            invincible = true;
            invincibilityTimer = 1f;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Goal"))
        {
            sceneSwitcher.nextScene();
        }
    } 

}
