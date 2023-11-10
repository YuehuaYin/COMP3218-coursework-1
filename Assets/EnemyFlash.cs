using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{

    public GameObject model;
    private SpriteRenderer sp;
    private float time;
    public GameObject wall;
    public GameObject retryFlash;

    public GameObject hintBox;
    private bool hintStart = false;
    private float hintTimer;
    private SpriteRenderer sp2;
    private Vector3 initialPos;
    private bool figuredOut = false;

    // Start is called before the first frame update
    void Start()
    {
        sp = model.GetComponent<SpriteRenderer>();
        sp2 = hintBox.GetComponent<SpriteRenderer>();
        initialPos = hintBox.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (DeathCounter.timer > 60 && !figuredOut)
        {
            time += Time.deltaTime;
            if ((int) time * 5 % 2 == 0 && wall.activeSelf)
            {
                sp.color = Color.blue;
            } else
            {
                sp.color = Color.white;
            }
        }*/
        if (DeathCounter.timer > 50 && !figuredOut)
        {

            if (transform.position.x > 11 && !hintStart && !figuredOut)
            {
                hintStart = true;

                sp2.color = new Color(sp2.color.r, sp2.color.g, sp2.color.b, 0);
                hintTimer = 2.5f;
                hintBox.transform.position = initialPos;
            }
            if (hintTimer > 0)
            {
                if (hintTimer > 1.2f)
                {
                    
                    hintTimer -= Time.deltaTime;
                } else if (hintTimer > 0.7f)
                {
                    hintTimer -= Time.deltaTime;
                    hintBox.SetActive(true);
                    sp2.color = new Color(sp2.color.r, sp2.color.g, sp2.color.b, 1.2f-hintTimer);
                }
                else
                {
                    hintBox.SetActive(true);
                    hintTimer -= Time.deltaTime * 0.15f;
                    sp2.color = new Color(sp2.color.r, sp2.color.g, sp2.color.b, hintTimer);
                }
            }
            else
            {
                hintStart = false;
                hintTimer = 0;
                hintBox.SetActive(false);

            }
        }

    }

    private void OnDestroy()
    {
        if (wall != null)
        {
            if (wall.activeSelf)
            {
                try
                {
                    retryFlash.GetComponent<AmmoPickupScript>().respawnObj();
                } catch (Exception e)
                {

                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") && collision.gameObject.transform.position.x < transform.position.x)
        {
            figuredOut = true;
            hintStart = false;
            hintTimer = 0;
            hintBox.SetActive(false);
        }
    }
}
