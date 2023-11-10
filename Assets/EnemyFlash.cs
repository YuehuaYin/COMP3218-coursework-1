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
    // Start is called before the first frame update
    void Start()
    {
        sp = model.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DeathCounter.timer > 80)
        {
            time += Time.deltaTime;
            if ((int) time * 5 % 2 == 0 && wall.activeSelf)
            {
                sp.color = Color.blue;
            } else
            {
                sp.color = Color.white;
            }
        }
    }

    private void OnDestroy()
    {
        if (wall != null)
        {
            if (wall.activeSelf)
            {
                retryFlash.GetComponent<AmmoPickupScript>().respawnObj();
            }
        }
    }
}
