using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Singleton
    public static GameController InstanceOfGame;

    //PlayerScore
    public int points = 0;
    private PlayerController playerController;

    private void Awake()
    {
        InstanceOfGame = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.InstanceOfPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.playerHealth <= 0)
        {
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
