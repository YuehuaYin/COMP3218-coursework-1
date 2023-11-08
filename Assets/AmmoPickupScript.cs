using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPickupScript : MonoBehaviour
{
    public bool respawn;
    public SpriteRenderer sp;
    public BoxCollider2D bc;
    private Image retryButton;
    private float retryTimer = 0;
    private bool retry = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (retry)
        {
            retryTimer += Time.deltaTime;
            if ((int)(retryTimer) % 2 == 0)
            {
                retryButton.color = Color.red;
            } else
            {
                retryButton.color = Color.white;
            }
        }
        
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
            /* bc.enabled = true;
             sp.enabled = true;
            */
            retry = true;
            retryButton = GameObject.Find("Canvas").transform.Find("Reset Level").GetComponent<Image>();
        }
    }

}
