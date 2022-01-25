using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTEST : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    float moveHorizontal;
    float ySpeed;
    float xSpeed;
    float jump;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump");
        ySpeed = playerRigidbody.velocity.y;
        Debug.Log(playerRigidbody.velocity);

    }


    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f)
        {
            playerRigidbody.velocity = new Vector2(10f, ySpeed);
        }

        else if (moveHorizontal < -0.1f)
        {
            playerRigidbody.velocity = new Vector2(-10f, ySpeed);
        }

        else if (moveHorizontal == 0)
        {
            playerRigidbody.velocity = new Vector2(0f, ySpeed);
        }

        if (isGrounded == true)
        {
            if (jump == 1)
            {
                playerRigidbody.velocity = new Vector2(xSpeed, 10f);
                //playerRigidbody.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
