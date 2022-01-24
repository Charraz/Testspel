using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidBody;
    Vector2 speed;
    float maxSpeed;
    float moveHorizontal;
    float xSpeed;
    float ySpeed;
    float jump;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        maxSpeed = 10f;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump");
        speed = playerRigidBody.velocity;
        xSpeed = speed.x;
        ySpeed = speed.y;
        Debug.Log(playerRigidBody.velocity);
    }

    void FixedUpdate()
    {
        if(moveHorizontal > 0.1f && xSpeed < maxSpeed)
        {
            playerRigidBody.AddForce(new Vector2(1f, 0f), ForceMode2D.Impulse);
        }
        else if(moveHorizontal < -0.1f && xSpeed > maxSpeed * -1)
        {
            playerRigidBody.AddForce(new Vector2(-1f, 0f), ForceMode2D.Impulse);
        }
        else if (moveHorizontal == 0)
        {
            playerRigidBody.velocity = new Vector2(0, ySpeed);
        }

        if(isGrounded == true)
        {
            if(jump == 1)
            {
                playerRigidBody.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
