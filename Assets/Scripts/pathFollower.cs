using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pathFollower : MonoBehaviour
{
    // Start is called before the first frame update
    Node[] PathNode;
    public GameObject follower;
    public float speed;
    float Timer;
    float PauseTimer;
    private Vector3 CurrentPosition;
    Vector2 startPosition;
    int currentNode;
    private float constSpeed = 0;
    public bool loop;
    private Rigidbody2D rb;
    private bool activated = false;
    private Vector2 currentSpeed;
    public GameObject tutorial;
    private float stuckTimer = 0;

    void Start()
    {
        rb = follower.GetComponent<Rigidbody2D>();
        PathNode = GetComponentsInChildren<Node>();
        CheckNode();
    }

    void CheckNode(){
        Timer = 0;
        PauseTimer = 0;
        activated = false;
        CurrentPosition = PathNode[currentNode].transform.position;
        startPosition = follower.transform.position;
        Vector3 start = new Vector3(startPosition.x, startPosition.y, 0);
        Vector3 distance = CurrentPosition - start;
        distance.Normalize();
       // constSpeed =  speed /distance.magnitude;
        //speed = dist/time
        //time = dist/speed
        
        rb.velocity = distance * speed;
        currentSpeed = distance * speed;
    }

    // Update is called once per frame
    void Update()
    {

        if (follower == null)
        {
            Destroy(gameObject);
            return;
        }
        
        if (Mathf.Abs((follower.transform.position - CurrentPosition).magnitude) < 0.3)
        {
            //Debug.Log("node reached");
            rb.velocity = Vector2.zero;
            if (!activated) {
                PathNode[currentNode].activate();
                activated = true;
            }
            PauseTimer += Time.deltaTime;
            if (PauseTimer >= PathNode[currentNode].pauseTime) {
                if (currentNode < PathNode.Length - 1)
                {
                    currentNode++;
                    CheckNode();
                }
                else if (loop)
                {
                    currentNode = 0;
                    CheckNode();
                }
                else if (tutorial != null)
                {

                    tutorial.SetActive(true);

                } else if (follower.CompareTag("Boss"))
                {
                    Destroy(gameObject);
                }
            }

            } else if (rb.velocity == Vector2.zero && follower.CompareTag("Enemy"))
            {
            if (follower.GetComponent<Enemy>().touchingBox())
            {
                stuckTimer += Time.deltaTime;
                if (stuckTimer > 0.5f)
                {
                    stuckTimer = 0;
                    currentNode--;
                    if (currentNode < 0)
                    {
                        currentNode = PathNode.Length - 1;
                    }
                    CheckNode();
                }
            }
        }
        else if (follower.CompareTag("Player") || follower.CompareTag("Boss"))
        {
            Vector3 direct = CurrentPosition - follower.transform.position;
            direct.Normalize();
            rb.velocity = direct * speed;
        } else if (!follower.GetComponent<Enemy>().getAggro())
        {
            Vector3 direct= CurrentPosition - follower.transform.position;
            direct.Normalize();
            rb.velocity = direct * speed;
        }
        if (rb.velocity != Vector2.zero)
        {
            stuckTimer = 0;
        }
        


        /*Timer += Time.deltaTime * constSpeed;
        if (follower.transform.position != CurrentPosition){
            
            follower.transform.position = Vector3.Lerp(startPosition, CurrentPosition, Timer);
        }
        else {
            PauseTimer += Time.deltaTime;
            if (PauseTimer > PathNode[currentNode].pauseTime){
                PathNode[currentNode].activate();
                if (currentNode < PathNode.Length - 1){
                    currentNode++;
                    CheckNode();
                }
            }
        } */


    }
}
