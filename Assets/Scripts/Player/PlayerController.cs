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
    public float jumpForce;
    bool hasJumped;
    bool canJump;
    bool canDoubleJump;

    //Partikelvariabler
    public GameObject dustEffect;
    public GameObject doubleJumpPoofEffect;
    private bool coroutineAllowed;
    private bool particleCD;

    //PlayerHealth
    public float playerHealth;
    private bool iFrame;
    private Material matWhite;
    private Material matDefault;
    private bool dead = false;

    //Ljudgrejer
    private SFXController sfxController;
    bool isAboutToBeDead = false;

    //Teleporter Transform
    [SerializeField] private Transform teleporterTop;

    //HitStopVariabel
    bool waiting;

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
        sfxController = SFXController.InstanceOfSFX;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = playerSprite.material;
        hasJumped = false;
        canJump = false;
        canDoubleJump = false;
        playerHealth = 5;
        iFrame = false;
        particleCD = false;
        Time.timeScale = 1f;
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

            }
        }
        else if (!IsGrounded())
        {
            coroutineAllowed = false;
            if (Input.GetButtonDown("Jump")  && hasJumped == false)
            {
                canDoubleJump = true;
                Instantiate(doubleJumpPoofEffect, new Vector3(transform.position.x, transform.position.y + -0.5f, transform.position.z), transform.rotation);
            }
        }

        ySpeed = playerRigidbody.velocity.y;

        //Animation = Jumping
        animation.SetFloat("YSpeed", ySpeed);

        if (playerHealth == 0 && isAboutToBeDead == false)
        {
            PlayerDeath();
        }

        PlayerStop();
    }


    private void FixedUpdate()
    {
        if (moveHorizontal > 0.1f && playerHealth > 0)
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

        else if (moveHorizontal < -0.1f && playerHealth > 0)
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

        else if (moveHorizontal == 0 && playerHealth > 0)
        {
            animation.SetFloat("Speed", 0);
            //Player movement

                playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
                StopCoroutine("SpawnDust");
                coroutineAllowed = true;
        }

        if (canJump == true && playerHealth > 0)
        {
            sfxController.PlayPlayerJump();
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }

        if (canDoubleJump == true && hasJumped == false && playerHealth > 0)
        {
            sfxController.PlayPlayerDoubleJump();
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
            playerRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            canDoubleJump = false;
            hasJumped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Traps" && playerHealth > 0)
        {
            if (iFrame == false)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
                Stop(0.05f);
                iFrame = true;
                playerHealth--;
                sfxController.PlayPlayerDamaged();
                TransparentFlash();
                Invoke("TransparentReset", 0.2f);
                Invoke("TransparentFlash", 0.4f);
                Invoke("TransparentReset", 0.6f);
                Invoke("TransparentFlash", 0.8f);
                Invoke("TransparentReset", 1f);
                Invoke("iFrameCD", 1f);
            }
        }

        if (collision.gameObject.tag == "EnemyBullet" && playerHealth > 0)
        {
            if (iFrame == false)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
                Stop(0.05f);
                iFrame = true;
                playerHealth--;
                sfxController.PlayPlayerDamaged();
                TransparentFlash();
                Invoke("TransparentReset", 0.2f);
                Invoke("TransparentFlash", 0.4f);
                Invoke("TransparentReset", 0.6f);
                Invoke("TransparentFlash", 0.8f);
                Invoke("TransparentReset", 1f);
                Invoke("iFrameCD", 1f);
            }
        }

        if (collision.gameObject.tag == "Teleporter")
        {
            Vector2 position = new Vector2(transform.position.x, teleporterTop.position.y);
            transform.position = position;

            Vector2 speed = new Vector2(playerRigidbody.velocity.x, -3f);
            playerRigidbody.velocity = speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && playerHealth > 0)
        {
            if (iFrame == false)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0f);
                playerRigidbody.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
                Stop(0.05f);
                iFrame = true;
                playerHealth--;
                sfxController.PlayPlayerDamaged();
                TransparentFlash();
                Invoke("TransparentReset", 0.2f);
                Invoke("TransparentFlash", 0.4f);
                Invoke("TransparentReset", 0.6f);
                Invoke("TransparentFlash", 0.8f);
                Invoke("TransparentReset", 1f);
                Invoke("iFrameCD", 1f);
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

    private void resetMaterial()
    {
        playerSprite.material = matDefault;
    }

    private void whiteFlash()
    {
        playerSprite.material = matWhite;
    }

    private void TransparentFlash()
    {
        playerSprite.color = new Color(1, 1, 1, 0.2f);
    }

    private void TransparentReset()
    {
        playerSprite.color = new Color(1, 1, 1, 1f);
    }

    public void Stop(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        if(playerHealth > 0)
        { 
            Time.timeScale = 1.0f;
        }

        else if (playerHealth == 0)
        {
            Time.timeScale = 0.3f;
        }
        waiting = false;
    }

    private void PlayerDeath()
    {
        if (playerSprite.flipX == false)
        {
            playerRigidbody.velocity = new Vector2(-5f, 10f);
        }

        else if (playerSprite.flipX == true)
        {
            playerRigidbody.velocity = new Vector2(5f, 10f);
        }

        animation.SetBool("PlayerDead", true);
        sfxController.PlayPlayerDeath();
        sfxController.PlayBackgroundMusic();
        isAboutToBeDead = true;
        Stop(1f);
        Invoke("Death", 0.3f);
    }

    private void PlayerStop()
    {
        if (playerHealth == 0 && isAboutToBeDead == true && IsGrounded() && dead == true)
        {
            playerRigidbody.velocity = new Vector2(0f, 0f);
            animation.SetBool("PlayerDeadEnd", true);
            animation.SetBool("PlayerDead", false);
        }
    }

    private void Death()
    {
        dead = true;
    }
}