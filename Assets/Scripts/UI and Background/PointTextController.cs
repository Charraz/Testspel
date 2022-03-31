using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointTextController : MonoBehaviour
{
    private GameController game;
    public TextMeshProUGUI pointText;
    public int myPoints = 0;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        game = GameController.InstanceOfGame;
    }

    // Update is called once per frame
    void Update()
    {
        test();
    }

    private void test()
    {
        myPoints = game.points;
        pointText.text = myPoints.ToString();
    }
}
