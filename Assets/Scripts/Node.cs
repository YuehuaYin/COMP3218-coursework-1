using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Time to stop for after reaching node
    public float pauseTime = 0;
    // Lever to activate after pause
    public LeverScript mechanic;
    public ThiefShoot thiefShoot;
    public GameObject interact;
    private SpriteRenderer sp;
    private bool attacking;
    private float timer = 0;

    public void Start()
    {
        if (interact != null)
        {
            sp = interact.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    public void activate()
    {
        if (mechanic != null){
            mechanic.activate();
            Debug.Log("lever activated");
            attacking = true;
            interact.transform.localScale = Vector3.zero;
            sp.enabled = true;

        }
        if (thiefShoot != null){
            thiefShoot.shoot();
        }
    }

    void Update()
    {

        if (attacking)
        {
            if (timer >= 0.25f)
            {
                attacking = false;
                timer = 0f;
                sp.enabled = false;
            }

            timer += Time.deltaTime;
            interact.transform.localScale = Vector3.one * timer * 8;
            sp.color = new Color(sp.color.r, sp.color.b, sp.color.g, 1 - 4 * timer);

        }
    }



}
