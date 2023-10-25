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
    static Vector3 CurrentPosition;
    Vector2 startPosition;
    int currentNode;
    void Start()
    {
        PathNode = GetComponentsInChildren<Node>();
        CheckNode();
    }

    void CheckNode(){
        Timer = 0;
        CurrentPosition = PathNode[currentNode].transform.position;
        startPosition = follower.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime * speed;
        if (follower.transform.position != CurrentPosition){
            follower.transform.position = Vector3.Lerp(startPosition, CurrentPosition, Timer);
        }
        else {
            if (currentNode < PathNode.Length - 1){
                currentNode++;
                CheckNode();
            }
        }
    }
}
