using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Vi har gjort en singleton
    public static PlayerController InstanceOfPlayer;

    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSprite;
    private BoxCollider2D playerCollider;
    [SerializeField] private LayerMask groundMask;
    public new Animator animation;

    //Movementvariabler
    float moveHorizontal;
    public float moveSpeed;
    private float ySpeed;

    //Jumpvariabler
    bool isGrounded;
    public float jumpForce;
    bool hasJumped;
    bool canJump;
    bool canDoubleJump;

    //Partikelvariabler
    public GameObject dustEffect;
    private bool coroutineAllowed;
    private bool particleCD;

    //PlayerHealth
    public float playerHealth;
    private bool iFrame;

    //Här deklarerar vi singletonen så att den har alla värden som spelaren har.
    //Denna kan sedan kommas åt av alla andra script i projektet.
    private void Awake()
    {
        InstanceOfPlayer = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
        hasJumped = false;
        canJump = false;
        canDoubleJump = false;
        playerHealth = 5;
        iFrame = false;
        particleCD = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        //Debug.Log(playerRigidbody.velocity);

        //Player jumping
        if (IsGrounded())
        {
            //Alternativ lösning i ifsatsen efter getbuttondown: Mathf.Abs(playerRigidbody.velocity.y) < 0.001)
            hasJumped = false;
            animation.SetBool("IsJumping", false);
            coroutineAllowed = true;

            if (Input.GetButtonDown("Jump"))
            {
                //Animation = Jumping
                animation.SetBool("IsJumping", true);

                canJump = true;
                //Player jumping

                Debug.Log("WOOO");

            }
        }
        else if (!IsGrounded())
        {
            coroutineAllowed = false;
            if (Input.GetButtonDown("Jump")  && hasJumped == false)
            {
                canDoubleJump = true;
            }
        }

        ySpeed = playerRigidbody.velocity.y;

        //Animation = Jumping
        animation.SetFloat("YSpeed", ySpeed);

        if (moveHorizontal > 0.1f)
        {
            
        }

        else if (moveHorizontal < -0.1f)
        {
            
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
            //transform.eulerAngles = new Vector3(0, 0, 0);
            if (coroutineAllowed == true)
            {
                StartCoroutine("SpawnDust");
                coroutineAllowed = false;
            }
        }

        else if (moveHorizontal < -0.1f)
        {
            //Animation = Running
            animation.SetFloat("Speed", moveSpeed);
            //Player movement
            playerRigidbody.velocity = new Vector2(moveSpeed * -1, playerRigidbody.velocity.y);
            playerSprite.flipX = true;
            //transform.eulerAngles = new Vector3(0, 180, 0);

            if (coroutineAllowed == true)
            {
                StartCoroutine("SpawnDust");
                coroutineAllowed = false;
            }

        }

        else if (moveHorizontal == 0)
        {
            animation.SetFloat("Speed", 0);
            //Player movement
            playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);

                StopCoroutine("SpawnDust");
                coroutineAllowed = true;
        }

        if (canJump == true)
        {
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }

        if (canDoubleJump == true && hasJumped == false)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            canDoubleJump = false;
            hasJumped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ändrar så att Grounded är true så man kan hoppa igen
        //if (collision.gameObject.tag == "Ground")
        //{
        //    isGrounded = true;
        //    //När man är "Grounded" så slutar hoppanimationen att spelas
        //    animation.SetBool("IsJumping", false);
        //    doubleJump = 0;
        //    coroutineAllowed = true;
        //}

        if (collision.gameObject.tag == "Enemy")
        {
            if (iFrame == false)
            {
                playerRigidbody.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
                iFrame = true;
                Debug.Log("RÖV");
                Invoke("iFrameCD", 0.7f);
            }
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (iFrame == false)
            {
                playerRigidbody.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
                iFrame = true;
                Debug.Log("RÖV");
                Invoke("iFrameCD", 0.7f);
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    //Ändrar så att Grounded är false när spelaren lämnar marken
    //    if (collision.gameObject.tag == "Ground" && IsGrounded())
    //    {
    //        isGrounded = false;
    //        doubleJump++;
    //        
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (iFrame == false)
            {
                playerRigidbody.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
                iFrame = true;
                Debug.Log("RÖV");
                Invoke("iFrameCD", 0.7f);
            }
        }
    }
    IEnumerator SpawnDust()
    {
        while(IsGrounded() && particleCD == false)
        {
            Instantiate(dustEffect, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), dustEffect.transform.rotation);
            particleCD = true;
            Invoke("ParticleCD", 0.25f);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private bool IsGrounded()
    {
        float extraLength = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, extraLength, groundMask);

        //Debug.DrawRay(playerCollider.bounds.center + new Vector3(playerCollider.bounds.extents.x, 0), Vector2.down * (playerCollider.bounds.extents.y + extraLength), Color.green);
        //Debug.DrawRay(playerCollider.bounds.center - new Vector3(playerCollider.bounds.extents.x, 0), Vector2.down * (playerCollider.bounds.extents.y + extraLength), Color.green);
        //Debug.DrawRay(playerCollider.bounds.center - new Vector3(0, playerCollider.bounds.extents.y), Vector2.right * (playerCollider.bounds.extents.x), Color.green);

        return raycastHit.collider != null;
    }
    private void iFrameCD()
    {
        iFrame = false;
    }

    private void ParticleCD()
    {
        particleCD = false;
    }
}