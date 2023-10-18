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
    public GameObject meleeAttack;
    bool gunReady = true;
    bool meleeReady = true;
    public GameObject healthbar;
    public float knockForce;
    private float invincibilityTimer;
    private bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(x*speed, y*speed);

        // Left click to fire gun
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("fire");
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
        if (gunReady){
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform);
        }
    }

    // Melee attack
    public void Melee()
    {
        if (meleeReady) {
            meleeAttack.GetComponent<SpriteRenderer>().enabled = true;
            meleeAttack.GetComponent<BoxCollider2D>().enabled = true;
            meleeAttack.GetComponent<SpriteRenderer>().enabled = false;
            meleeAttack.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !invincible)
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
}
