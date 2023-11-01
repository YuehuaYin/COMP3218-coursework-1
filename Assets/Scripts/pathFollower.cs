using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFollower : MonoBehaviour
{
    // Start is called before the first frame update
    Node[] PathNode;
    public GameObject follower;
    public float speed;
    float Timer;
    float PauseTimer;
    static Vector3 CurrentPosition;
    Vector2 startPosition;
    int currentNode;
    private float constSpeed = 0;
    public bool loop;
    private Rigidbody2D rb;
    private bool activated = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs((follower.transform.position - CurrentPosition).magnitude) < 0.3)
        {
            Debug.Log("node reached");
            rb.velocity = Vector2.zero;
            if (!activated) {
                PathNode[currentNode].activate();
                activated = true;
            }
            PauseTimer += Time.deltaTime;
            if (PauseTimer >= PathNode[currentNode].pauseTime){
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
            }
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
