using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Time to stop for after reaching node
    public float pauseTime = 0;
    // Lever to activate after pause
    public LeverScript mechanic;
    public ThiefShoot thiefShoot;

    // Update is called once per frame
    public void activate()
    {
        if (mechanic != null){
            mechanic.activate();
            Debug.Log("lever activated");
        }
        if (thiefShoot != null){
            thiefShoot.shoot();
        }
    }
}
