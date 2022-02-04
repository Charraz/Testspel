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

    int test;
    //Jumpvariabler
    bool isGrounded;
    public Transform groundCheck;
    public float checkRadiusGround;
    public float checkRadiusFront;
    public LayerMask whatIsGround;
    public float jumpForce;
    int doubleJump;

    //Wallclimbvariabler
    bool isTouchingFront;
    public Transform frontCheck;
    public float wallSlidingSpeed;
    bool wallSliding;
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

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
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadiusGround, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadiusFront, whatIsGround);
        Debug.Log(playerRigidbody.velocity);

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
            if (Input.GetButtonDown("Jump") && doubleJump >= 1)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump --;
            }

            else if (Input.GetButtonDown("Jump") && doubleJump >= 1)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump--;
            }
        }

        if (isTouchingFront == true && isGrounded == false)
        {
            wallSliding = true;
        }

        else
        {
            wallSliding = false;
        }

        if (wallSliding == true)
        {
            //playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, Mathf.Clamp(playerRigidbody.velocity.y, -wallSlidingSpeed, float.MaxValue));
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, -wallSlidingSpeed);
        }

        if (Input.GetButtonDown("Jump") && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpToFalse", wallJumpTime);
        }

        if (wallJumping == true)
        {
            playerRigidbody.velocity = new Vector2(xWallForce * -moveHorizontal, yWallForce);
        }
    }


    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f && wallJumping == false)
        {
            //Animation = Running
            animation.SetFloat("Speed", moveSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(moveSpeed, playerRigidbody.velocity.y);
            //playerSprite.flipX = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else if (moveHorizontal < -0.1f && wallJumping == false)
        {
            //Animation = Running
            animation.SetFloat("Speed", moveSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(moveSpeed * -1, playerRigidbody.velocity.y);
            //playerSprite.flipX = true;
            transform.eulerAngles = new Vector3(0, 180, 0);

        }

        else if (moveHorizontal == 0 && wallJumping == false)
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

        //if (collision.gameObject.tag == "Wall")
        //{
        //    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
        //    if (doubleJump < 1)
        //        { 
        //            doubleJump++;
        //        }
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ändrar så att Grounded är false när spelaren lämnar marken
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            doubleJump ++;
        }
    }

    void SetWallJumpToFalse()
    {
        wallJumping = false;
    }
}
