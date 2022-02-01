using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite;
    public new Animator animation;
    float moveHorizontal;
    float ySpeed;
    float xSpeed;
    bool isGrounded;
    bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        doubleJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        ySpeed = playerRigidbody.velocity.y;
        xSpeed = 10;
        Debug.Log(doubleJump);

        //Player jumping
        if (isGrounded == true)
        {
            if (Input.GetButtonDown("Jump") && doubleJump == false)
            {
                //Animation = Jumping
                animation.SetBool("IsJumping", true);
                //Player jumping
                playerRigidbody.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            }
        }
        else if (isGrounded == false)
        {
            if (Input.GetButtonDown("Jump") && doubleJump == true)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
    }


    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f)
        {
            //Animation = Running
            animation.SetFloat("Speed", xSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(xSpeed, ySpeed);
            playerSprite.flipX = false;
        }

        else if (moveHorizontal < -0.1f)
        {
            //Animation = Running
            animation.SetFloat("Speed", xSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(xSpeed * -1, ySpeed);
            playerSprite.flipX = true;
        }

        else if (moveHorizontal == 0)
        {
            //Animation = Idle
            animation.SetFloat("Speed", 0);
            //Player movement
            playerRigidbody.velocity = new Vector2(0f, ySpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ndrar s� att Grounded �r true s� man kan hoppa igen
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            //N�r man �r "Grounded" s� slutar hoppanimationen att spelas
            animation.SetBool("IsJumping", false);
            doubleJump = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�ndrar s� att Grounded �r false n�r spelaren l�mnar marken
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            doubleJump = true;
        }
    }
}
