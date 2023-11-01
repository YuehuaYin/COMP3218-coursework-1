using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject[] walls;
    private int collisions = 0;

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
        Debug.Log("Something on the pressure plate");
        if (collision.CompareTag("Box") || collision.CompareTag("Player"))
        {
            collisions++;
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box") || collision.CompareTag("Player"))
        {
            collisions--;
            if (collisions <= 0)
            {
                collisions = 0;
                for (int i = 0; i < walls.Length; i++)
                {
                    walls[i].SetActive(true);
                }
            }
        }
    }

}
