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

    //Referera till HPTextController
    private HPTextController hpTextController;
    public GameObject timecontroller;

    private bool gameEnd = false;
    private void Awake()
    {
        InstanceOfGame = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = PlayerController.InstanceOfPlayer;
        hpTextController = timecontroller.GetComponent<HPTextController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.playerHealth <= 0 && gameEnd == false)
        {
            Invoke("GameEndHP", 3f);
        }

        if (hpTextController.gameTime <= 0 && gameEnd == false)
        {
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
            gameEnd = true;
        }
    }

    private void GameEndHP()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
        gameEnd = true;
    }
}
