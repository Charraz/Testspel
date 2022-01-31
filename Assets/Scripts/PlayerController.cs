using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite;
    float moveHorizontal;
    float ySpeed;
    float xSpeed;
    float jump;
    bool isGrounded;
    bool jumpCD;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        jumpCD = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Jump");
        ySpeed = playerRigidbody.velocity.y;
    }


    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f)
        {
            playerRigidbody.velocity = new Vector2(10f, ySpeed);
            playerSprite.flipY = false;
        }

        else if (moveHorizontal < -0.1f)
        {
            playerRigidbody.velocity = new Vector2(-10f, ySpeed);
            playerSprite.flipY = true;
        }

        else if (moveHorizontal == 0)
        {
            playerRigidbody.velocity = new Vector2(0f, ySpeed);
        }

        if (isGrounded == true)
        {
            if (jump == 1 && jumpCD == false)
            {             
                //playerRigidbody.velocity = new Vector2(xSpeed, 10f);
                playerRigidbody.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
                jumpCD = true;

                Invoke("CD", 0.3f);

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

    private void CD()
    {
        jumpCD = false;
    }
}
