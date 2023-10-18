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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
