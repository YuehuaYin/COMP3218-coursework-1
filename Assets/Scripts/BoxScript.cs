using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxScript : MonoBehaviour
{
    private Vector3 adjust = Vector3.zero;
    private int wallCount;
    public GameObject N;
    public GameObject E;
    public GameObject S;
    public GameObject W;
    public float length;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("HigherFloor"))
        {
            adjust = transform.position - collision.transform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (N.GetComponent<boxDirections>().getTouch())
            {
                transform.position += new Vector3(0,-1,0) * length;
            }
            if (E.GetComponent<boxDirections>().getTouch())
            {
                transform.position += new Vector3(-1, 0, 0) * length;
            }
            if (S.GetComponent<boxDirections>().getTouch())
            {
                transform.position += new Vector3(0, 1, 0) * length;
            }
            if (W.GetComponent<boxDirections>().getTouch())
            {
                transform.position += new Vector3(1, 0, 0) * length;
            }
        }
    }
}
    

