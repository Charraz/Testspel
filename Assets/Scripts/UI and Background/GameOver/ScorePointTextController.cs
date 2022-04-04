using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScorePointTextController : MonoBehaviour
{
    private GameController game;

    public TextMeshProUGUI textMeshPro;
    public int myPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        game = GameController.InstanceOfGame;
    }

    // Update is called once per frame
    void Update()
    {
        //textMeshPro.text = ("snyft");
        test();
    }

    private void test()
    {
        myPoints = game.points;
        textMeshPro.text = myPoints.ToString();
    }
}
