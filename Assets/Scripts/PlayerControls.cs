using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float gunCooldown;
    public float meleeCooldown;
    public GameObject bulletPrefab;
    public GameObject meleeAttack;
    bool gunReady = true;
    bool meleeReady = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
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
}
