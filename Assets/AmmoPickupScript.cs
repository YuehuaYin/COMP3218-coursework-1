using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AmmoPickupScript : MonoBehaviour
{
    public bool respawn;
    public SpriteRenderer sp;
    public BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!respawn)
            {
                Destroy(gameObject);
            } else
            {
                bc.enabled = false;
                sp.enabled = false;
            }
        }
    }

    public void respawnObj()
    {
        if (respawn)
        {
            bc.enabled = true;
            sp.enabled = true;
        }
    }

}
