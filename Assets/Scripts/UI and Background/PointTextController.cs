using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTextController : MonoBehaviour
{
    private GameController game;
    public Text pointText;
    public int myPoints = 0;

    private void Awake()
    {
        game = GameController.InstanceOfGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
