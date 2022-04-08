using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorePoints1 : MonoBehaviour
{
    private GameController game;
    public TextMeshProUGUI textMeshPro;
    public int myPoints1 = 0;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        game = GameController.InstanceOfGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.timesPlayed == 1)
        {
            Played1();
        }

    }

    private void Played1()
    {
        myPoints1 = game.points;
        textMeshPro.text = myPoints1.ToString();
    }
}
