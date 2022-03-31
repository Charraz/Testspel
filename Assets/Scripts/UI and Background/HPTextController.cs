using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPTextController : MonoBehaviour
{
    public float gameTime;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTime > 0)
        {
            gameTime = gameTime - 1f * Time.deltaTime;
        }

        else
        {
            gameTime = 0;
        }

        DisplayTime(gameTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
