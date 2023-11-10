using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class ScoreIndication : MonoBehaviour
{

    public TMPro.TextMeshProUGUI txt;
    private Vector3 initial;
    private float bonus = 1;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        //txt = GetComponent<TMPro.TextMeshProUGUI>();
        initial = transform.position - 10* Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;


            transform.position = initial + timer * Vector3.up * 20 * bonus;
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, timer * 2 * bonus);
        }
    }


    public void scoreChange(int score, bool timeBonus)
    {
        string msg = "";
        if (score == 0)
        {
            return;
        } else if (score > 0) { 
        msg = "+" + score;
            if (timeBonus)
            {
                msg += " time bonus";
            }
            
            txt.color = Color.green;
        } else
        {
            msg = score.ToString();
            if (timeBonus)
            {
                msg += " time bonus";
            }
            txt.color = Color.red;
        }
        txt.text = msg;

        if (true)
        {
            timer = 1;
            bonus = 0.5f;
        } else
        {
            bonus = 1;
            timer = 0.5f;
        }

        Debug.Log("Score change " + timeBonus);
        

    }
}
