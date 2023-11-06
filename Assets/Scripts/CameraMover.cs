using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float minX;
    public float maxX;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x + 8 < minX)
        {
            transform.position = new Vector3(minX, this.gameObject.transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x + 8 > maxX) 
        {
            transform.position = new Vector3(maxX, this.gameObject.transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 8, this.gameObject.transform.position.y, transform.position.z);
        }
    }
}
