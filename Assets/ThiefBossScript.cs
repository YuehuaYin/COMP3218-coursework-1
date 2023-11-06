using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefBossScript : MonoBehaviour
{
    private int hp = 3;
    public GameObject healthBar;
    private Health health;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        health = healthBar.GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity == Vector2.zero)
        {
            GetComponent<ThiefAnimation2>().left();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Hazard"))
        {
            Damage();
        }
    }

    private void Damage()
    {
        hp--;
        health.Damage(1);


    }
}
