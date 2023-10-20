using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float totalhp;
    private float hp;
    public GameObject enemy;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        hp = totalhp;
    }

    // Update is called once per frame
    void Update()
    {
        /*timer += Time.deltaTime;
        if (timer > 1)
        {
            Damage(1);
            timer = 0;
        }
        */
    }

    public void Damage(float damage)
    {
        hp -= damage;
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Debug.Log("Hp: " + hp);
        
        if (hp <= totalhp * 0.25)
        {
            sp.color = Color.red;
        }
        else if (hp <= totalhp * 0.5)
        {
            
            sp.color = Color.yellow;
            Debug.Log("Yellow");

        }
        else if (hp <= totalhp *0.75)
        {
            sp.color = new Color(1f, 0.4f, 0f, 1f);
            Debug.Log("Orange");
        }
        else if (hp < totalhp)
        {
            
            sp.color = Color.green;
        }
        
        

        if (hp <= 0)
        {
            Destroy(enemy);
        }

        transform.localScale = new Vector3( hp / totalhp, transform.localScale.y, 0);
        transform.position = new Vector3(enemy.transform.position.x -0.5f *(1-hp/totalhp), transform.position.y, 0);
    }
}
