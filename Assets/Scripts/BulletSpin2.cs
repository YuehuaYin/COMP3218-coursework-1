using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpin2 : MonoBehaviour
{
    public float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("Enemy bullet");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
