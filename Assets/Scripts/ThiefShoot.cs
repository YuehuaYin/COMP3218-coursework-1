using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefShoot : MonoBehaviour
{
    private Vector2 move;
    public float speed;
    public GameObject enemy;
    public GameObject bulletPrefab;
    private GameObject bullet;
    public GameObject target;
    private TargetScript ts;
    private bool disappear = true;
    private float disappearTimer = 0;

    // Start is called before the first frame update
    public void shoot()
    {
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 direction = enemy.transform.position - transform.position;
        direction.z = 0;
        direction.Normalize();
        move = direction * speed;

        disappear = false;
        ts.shoot();
    }

    void Update() 
    {
        if (bullet != null){
            bullet.transform.Translate(move * Time.deltaTime);
        } else if (ts != null && !disappear) {
            disappearTimer += Time.deltaTime;
            if (disappearTimer > 1)
            {
                disappear = true;
                ts.disappear();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AmmoPickup"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void Start()
    {
        target.SetActive(true);
        ts = target.GetComponent<TargetScript>();
        ts.appear();
    }
}
