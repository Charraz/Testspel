using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    float moveHorizontal;
    float ySpeed;
    float xSpeed;
    float jump;
    bool isGrounded;

    [SerializeField]Rigidbody2D bullet;
    Vector2 bulletSpawn;
    float bulletspeed = 5f;

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
        //Debug.Log(playerRigidbody.velocity);

        //Går inte att ha i FixedUpdate får knapptryckning blir fucked
        if (Input.GetMouseButtonDown(0))
        {
            skott();
        }

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
                //playerRigidbody.velocity = new Vector2(xSpeed, 10f);
                playerRigidbody.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
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


    void skott()
    {
        Vector2 mousePos = Input.mousePosition;
        bulletSpawn = new Vector2(playerRigidbody.position.x, playerRigidbody.position.y + 1f);
        Rigidbody2D clone;
        clone = Instantiate(bullet, bulletSpawn, transform.rotation);
        clone.velocity = Vector2.MoveTowards(bulletSpawn, mousePos, 10f);
    }
}
