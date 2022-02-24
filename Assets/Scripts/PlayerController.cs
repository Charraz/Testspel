using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Rigidbody2D playerRigidbody;
    SpriteRenderer playerSprite;
    public GameObject dirtEffect;
    
    public new Animator animation;
    
    //Movementvariabler
    float moveHorizontal;
    public float moveSpeed;

    //Jumpvariabler
    bool isGrounded;
    public float jumpForce;
    int doubleJump;

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
        //Debug.Log(playerRigidbody.velocity);

        //Player jumping
        if (isGrounded == true)
        {
            //Alternativ l�sning i ifsatsen efter getbuttondown: Mathf.Abs(playerRigidbody.velocity.y) < 0.001)

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
            //playerSprite.flipX = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else if (moveHorizontal < -0.1f)
        {
            //Animation = Running
            animation.SetFloat("Speed", moveSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(moveSpeed * -1, playerRigidbody.velocity.y);
            //playerSprite.flipX = true;
            transform.eulerAngles = new Vector3(0, 180, 0);

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
        //�ndrar s� att Grounded �r true s� man kan hoppa igen
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            //N�r man �r "Grounded" s� slutar hoppanimationen att spelas
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
        //�ndrar s� att Grounded �r false n�r spelaren l�mnar marken
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            doubleJump ++;
        }
    }
}
