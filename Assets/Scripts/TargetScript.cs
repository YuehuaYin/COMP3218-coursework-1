using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private SpriteRenderer sp;
    private float appearTimer = 0;
    private float disappearTimer = 0;

    private float shootTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (appearTimer > 0)
        {
            appearTimer -= Time.deltaTime;
            if (appearTimer < 0 ) { appearTimer = 0; }
            transform.localScale = Vector3.one * (1+appearTimer) *0.2f;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1 - appearTimer * 2);
        }
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer < 0) { shootTimer = 0; }
            transform.localScale = Vector3.one * (1 + shootTimer * 2) * 0.2f;
            sp.color = new Color(4* shootTimer, 4 * shootTimer, 1, sp.color.a);
        }

        if (disappearTimer > 0)
        {
            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0) { disappearTimer = 0; }
            transform.localScale = Vector3.one * (1.5f - disappearTimer) * 0.2f;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, disappearTimer * 2);
        }
    }


    public void appear()
    {
        appearTimer = 0.5f;
        disappearTimer = 0;
    }

    public void shoot()
    {
        shootTimer = 0.25f;
    }

    public void disappear()
    {
        disappearTimer = 0.5f;
        appearTimer = 0;
    }
}
