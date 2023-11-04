using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class glow : MonoBehaviour
{
    private bool increase = false;
    public SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (increase)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a +Time.deltaTime *0.2f);
            if (sp.color.a >= 0.5)
            {
                increase = false;
            }
        } else
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - Time.deltaTime *0.2f);
            if (sp.color.a <= 0)
            {
                increase = true;
            }
        }
    }
}
