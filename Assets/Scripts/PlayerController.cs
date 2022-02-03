using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite;
    
    public new Animator animation;
    
    //Movementvariabler
    float moveHorizontal;
    public float moveSpeed;

    //Jumpvariabler
    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;
    int doubleJump;

    //Wallclimbvariabler
    bool isTouchingFront;
    public Transform frontCheck;
    public float wallSlidingSpeed;
    bool wallSliding;
    bool wallClimb;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        doubleJump = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        Debug.Log(isGrounded);

        //Player jumping
        if (isGrounded == true)
        {
            //Alternativ lösning i ifsatsen efter getbuttondown: Mathf.Abs(playerRigidbody.velocity.y) < 0.001)

            if (Input.GetButtonDown("Jump") && doubleJump < 1)
            {
                //Animation = Jumping
                animation.SetBool("IsJumping", true);
                //Player jumping
                playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
        else if (isGrounded == false)
        {
            if (Input.GetButtonDown("Jump") && doubleJump >= 1 && wallClimb == false)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump --;
            }

            else if (Input.GetButtonDown("Jump") && doubleJump >= 1 && wallClimb == true)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump--;
            }
        }
    }


    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f)
        {
            //Animation = Running
            animation.SetFloat("Speed", moveSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(moveSpeed, playerRigidbody.velocity.y);
            playerSprite.flipX = false;
        }

        else if (moveHorizontal < -0.1f)
        {
            //Animation = Running
            animation.SetFloat("Speed", moveSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(moveSpeed * -1, playerRigidbody.velocity.y);
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
            doubleJump = 0;
        }

        if (collision.gameObject.tag == "Wall")
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
            wallClimb = true;
            if (doubleJump < 1)
                { 
                    doubleJump++;
                }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ändrar så att Grounded är false när spelaren lämnar marken
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            doubleJump ++;
        }

        if (collision.gameObject.tag == "Wall")
        {
            wallClimb = false;
        }
    }
}
