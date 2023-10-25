using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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

        if (Input.GetKey(KeyCode.Space) && dashTimer <= 0 && rb.velocity != Vector2.zero)
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
            }*/
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
           
        }

        if (Input.GetKey(KeyCode.R)){
            sceneSwitcher.restartScene();
        }

    }

    // Fire ranged attack
    public void Fire()
    {
        if (gunTimer >= gunCooldown){
            Debug.Log("fire");
            gunTimer = 0f;
            
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            
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
            /*
            healthbar.GetComponent<Health>().Damage(1);
            Vector2 knock = transform.position - collision.transform.position;
            knock.Normalize();
            rb.AddForce(knock * knockForce, ForceMode2D.Impulse);
            //rb.velocity = (knock * knockForce);
            invincible = true;
            invincibilityTimer = 1f;
            */

            sceneSwitcher.restartScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Goal"))
        {
            sceneSwitcher.nextScene();
        }
        if (collision.CompareTag("Hazard"))
        {
            sceneSwitcher.restartScene();
        }
    } 

}
