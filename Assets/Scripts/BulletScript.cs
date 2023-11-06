using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector2 move;
    public float speed;
    //  public AudioSource bulletSound;
    private float timer;
    
    void Start()
    {
       // bulletSound.Play();
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouse - transform.position;
        direction.z = 0;
        direction.Normalize();
        move = direction * speed;
        Debug.Log(move);
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    void Update() 
    {
        transform.Translate(move * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > 0.02f)
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision entered bullet " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall") )
        {
            Destroy(gameObject);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Trigger entered bullet " + collision.tag);
        if (collision.CompareTag("Enemy") || collision.CompareTag("Wall") ) 
        {
            Destroy(gameObject);
        }
    }

}
