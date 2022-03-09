using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalleKlinga : MonoBehaviour
{
    private Rigidbody2D rigidKropp;
    private PlayerController playerController;
    public float timeToDirectionChange;
    private float moveSpeed;
    private bool movingLeft;

    private void Awake()
    {
        playerController = PlayerController.InstanceOfPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        movingLeft = true;
        moveSpeed = 3f;
        rigidKropp = GetComponent<Rigidbody2D>();
        Invoke("changeMoveDirection", timeToDirectionChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft == true)
        {
            rigidKropp.velocity = new Vector2(moveSpeed * -1, 0f);
        }
        if (movingLeft == false)
        {
            rigidKropp.velocity = new Vector2(moveSpeed, 0f);
        }
    }

    private void changeMoveDirection()
    {
        movingLeft = !movingLeft;
        Invoke("changeMoveDirection", timeToDirectionChange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.playerHealth--;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

}
