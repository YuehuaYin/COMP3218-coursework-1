using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour
{
    private List<GameObject> listOfChildren;
    public UnityEvent leverActivated = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Melee")) //collision is from an attack
        {
            Debug.Log("lever hit");
            activate();
        }

        if (collision.CompareTag("Bullet")) //collision is from a bullet - not working????/
        {
            Debug.Log("lever hit");
            activate();
        }
    } 

    public void activate(){
        leverActivated.Invoke();
    }
}
