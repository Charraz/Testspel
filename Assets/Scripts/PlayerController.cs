using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite;
    public new Animator animation;
    float moveHorizontal;
    float xSpeed;
    public float jumpForce;
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
        xSpeed = 10;
        //Debug.Log(doubleJump);

        //Player jumping
        if (isGrounded == true)
        {
            //Alternativ lösning i ifsatsen efter getbuttondown: Mathf.Abs(playerRigidbody.velocity.y) < 0.001)

            if (Input.GetButtonDown("Jump") && doubleJump == false)
            {
                //Animation = Jumping
                animation.SetBool("IsJumping", true);
                //Player jumping
                playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
        else if (isGrounded == false)
        {
            if (Input.GetButtonDown("Jump") && doubleJump == true)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
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
            playerRigidbody.velocity = new Vector2(xSpeed, playerRigidbody.velocity.y);
            playerSprite.flipX = false;
        }

        else if (moveHorizontal < -0.1f)
        {
            //Animation = Running
            animation.SetFloat("Speed", xSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(xSpeed * -1, playerRigidbody.velocity.y);
            playerSprite.flipX = true;
        }

        else if (moveHorizontal == 0)
        {
            //Animation = Idle
            animation.SetFloat("Speed", 0);
            //Player movement
            playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ändrar så att Grounded är true så man kan hoppa igen
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            //När man är "Grounded" så slutar hoppanimationen att spelas
            animation.SetBool("IsJumping", false);
            doubleJump = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ändrar så att Grounded är false när spelaren lämnar marken
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            doubleJump = true;
        }
    }
}
