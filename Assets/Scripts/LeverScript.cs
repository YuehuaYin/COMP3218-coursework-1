using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour
{
    private List<GameObject> listOfChildren;
    public UnityEvent leverActivated = new UnityEvent();
    public AudioSource leverSound;
    private SpriteRenderer sp;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
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
        leverSound.Play();
        Debug.Log("Lever sound");
        leverActivated.Invoke();
        sp.color = new Color(0.5f, 0.5f, 0.5f, 1);
        timer = 0.25f;

    }

    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            sp.color = new Color(1- timer *2, 1-timer*2, 1-timer*2, 1);
        }
    }
}
