using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTimeController : MonoBehaviour
{
    private GameController game;

    public float timeScore;

    public TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        game = GameController.InstanceOfGame;
        
    }

    // Update is called once per frame
    void Update()
    {
        test();
    }

    private void test()
    {
        timeScore = game.gameTime;
        timeScore = timeScore - 300;
        timeScore = -timeScore;
        float minutes = Mathf.FloorToInt(timeScore / 60);
        float seconds = Mathf.FloorToInt(timeScore % 60);
        textMeshPro.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
}
