using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTextController : MonoBehaviour
{
    private PlayerController playerController;
    public Text pointText;
    public int myPoints;

    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.InstanceOfPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        test();
    }

    private void test()
    {
        myPoints = playerController.points;
        pointText.text = myPoints.ToString();
    }
}
