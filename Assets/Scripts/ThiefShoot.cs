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

    // Start is called before the first frame update
    public void shoot()
    {
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 direction = enemy.transform.position - transform.position;
        direction.z = 0;
        direction.Normalize();
        move = direction * speed;
    }

    void Update() 
    {
        if (bullet != null){
            bullet.transform.Translate(move * Time.deltaTime);
        }
    }
}
