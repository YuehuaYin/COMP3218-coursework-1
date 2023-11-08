using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Aggro : MonoBehaviour
{
    public GameObject enemy;
    public SpriteRenderer sp;
    private bool aggro = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 dir = enemy.GetComponent<Rigidbody2D>().velocity;
        if ((dir.magnitude > 0.3 && !aggro) || (Mathf.Abs(dir.x) >0.05 && Mathf.Abs(dir.y) > 0.05))
        {
            dir.Normalize();
            /*
            float dir2 = 90 - Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), new Vector3(dir.x, dir.y, 0.0f));
            if (dir.x < 0.0f)
            {
                dir2 = 180 -dir2;
                
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, dir2));
            */
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            //transform.RotateAround(enemy.transform.position, dir2);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // if (!collision.gameObject.GetComponent<PlayerControls>().getInvis())
           // {
                Vector2 direction = collision.transform.position - transform.position;
                int mask = 1 << 9;
                RaycastHit2D wallDetect = Physics2D.Raycast(transform.position, direction, direction.magnitude, mask);
                if (wallDetect.collider == null && !collision.gameObject.GetComponent<PlayerControls>().getInvis())
                {
                    aggro = true;
                    enemy.GetComponent<Enemy>().Aggro();


                    sp.color = Color.red;
                }
                else
                {
                    Debug.Log("There is a wall in the way. No aggro");
                }
           // }

        }
        
    }
}
