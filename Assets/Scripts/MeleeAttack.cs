using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    private GameObject parent;
    private float attackDuration;
    private bool attacking = false;
    private float timer = 0f;
    public GameObject interact;
    private SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        attackDuration = 0.5f;
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        sp = interact.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        /*
        if (attacking == false)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mouse - transform.position;
            direction.z = 0;
            transform.position = (Vector2)(parent.transform.position + (direction.normalized * 1f));
        }
        */

        if (attacking)
        {
            if (timer >= attackDuration)
            {
                attacking = false;
                timer = 0f;
                //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                
            }

            timer += Time.deltaTime;
            if (timer <= 0.25f)
            {
                interact.transform.localScale = Vector3.one * timer * 8;
                sp.color = new Color(sp.color.r, sp.color.b, sp.color.g, 1 - 4 * timer);
            } else
            {
                this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                sp.enabled = false;
            }

        }
    }


    public void attack()
    {
        attacking = true;
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        interact.transform.localScale = Vector3.zero;
        sp.enabled = true;
    }
}
