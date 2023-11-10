using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuckFix : MonoBehaviour
{
    private Vector3 adjust = Vector3.zero;
    private int wallCount;
    public GameObject N;
    public GameObject E;
    public GameObject S;
    public GameObject W;
    public float length;
    private Rigidbody2D rb;
    private bool wallTouch = false;
    private Vector3 prevPosition;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((rb.velocity == Vector2.zero  || prevPosition == transform.position && rb.velocity != Vector2.zero)&& wallTouch)
        {
            fix(length * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            wallTouch = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallTouch = false;
            
        }
    }

    private void fix(float length2)
    {
        if (N.GetComponent<boxDirections>().getTouch())
        {
            transform.position += new Vector3(0, -1, 0) * length2;
        }
        if (E.GetComponent<boxDirections>().getTouch())
        {
            transform.position += new Vector3(-1, 0, 0) * length2;
        }
        if (S.GetComponent<boxDirections>().getTouch())
        {
            transform.position += new Vector3(0, 1, 0) * length2;
        }
        if (W.GetComponent<boxDirections>().getTouch())
        {
            transform.position += new Vector3(1, 0, 0) * length2;
        }
    }
}
