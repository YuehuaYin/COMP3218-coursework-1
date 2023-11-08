using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public GameObject videoplayer;
    private UnityEngine.Video.VideoPlayer video;
    public GameObject start;
    public GameObject skipbutton;
    public GameObject score;
    public GameObject deaths;
    public GameObject time;
    public GameObject rankText;
    public GameObject background;

    // Start is called before the first frame update
    void Start()
    {
        skipbutton.SetActive(true);
        video = videoplayer.GetComponent<UnityEngine.Video.VideoPlayer>();
        video.Play();
        video.loopPointReached += EndReached;
    }

    public void skipButton()
    {
        EndReached(video);
    }

    void EndReached(UnityEngine.Video.VideoPlayer video)
    {
        videoplayer.SetActive(false);
        skipbutton.SetActive(false);
        background.SetActive(true);
        start.SetActive(true);

        score.GetComponent<TextMeshProUGUI>().text = "Score: " + DeathCounter.score;

        deaths.GetComponent<TextMeshProUGUI>().text = "Deaths: " + DeathCounter.deaths;

        time.GetComponent<TextMeshProUGUI>().text = "Total time: " +  (int) DeathCounter.totalTime;

        float rankScore = DeathCounter.score - DeathCounter.deaths * 50 -  (int) DeathCounter.totalTime * 10;
        string rank = "S";
        Color rankColor = Color.blue;
        if (rankScore < -1000)
        {
            rankColor = Color.red;
            rank = "F";
        } else if (rankScore < -500)
        {
            rankColor = Color.red;
            rank = "E";
        }
        else if (rankScore < 0)
        {
            rankColor = Color.yellow;
            rank = "D";
        }
        else if (rankScore < 500)
        {
            rankColor = Color.yellow;
            rank = "C";
        }
        else if (rankScore < 1000)
        {
            rankColor = Color.green;
            rank = "B";
        }
        else if (rankScore < 3000)
        {
            rankColor = Color.green;
            rank = "A";
        } else
        {
            rank = "S";
        }

        rankText.GetComponent<TextMeshProUGUI>().text = "Rank: " + rank;
        rankText.GetComponent<TextMeshProUGUI>().color = rankColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
