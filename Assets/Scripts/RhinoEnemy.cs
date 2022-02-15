using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoEnemy : MonoBehaviour
{
    public Rigidbody2D rigidkropp;
    public new Animator animation;

    float moveSpeed;
    //float runSpeed;
    //float fallSpeed;
    bool playerInSight;
    bool movingLeft;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();

        movingLeft = true;
        playerInSight = false;
        moveSpeed = -3;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 1f, Color.green);
        RaycastHit2D TurnAroundRaycast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1f);
        if (TurnAroundRaycast.collider!=null && TurnAroundRaycast.collider.tag == "Wall")
        {
            rigidkropp.transform.Rotate(0f, 180f, 0f);
            movingLeft = !movingLeft;
        }

        RaycastHit2D SeePlayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 3f);
        if (SeePlayer.collider != null && SeePlayer.collider.tag == "Player")
        {
            moveSpeed = -6;
            animation.SetBool("SeesPlayer", true);
        }
        else
        {
            moveSpeed = -3;
            animation.SetBool("SeesPlayer", false);
        }
    }

    private void FixedUpdate()
    {
        if (movingLeft == true)
        {
            rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
        }
        else if (movingLeft == false)
        {
            rigidkropp.velocity = new Vector2(moveSpeed * -1, rigidkropp.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Death if colliding with a player bullet.
        if (collision.gameObject.tag == "PlayerBullet")
        {
            moveSpeed = -2;
            Debug.Log("Träff");
            animation.SetBool("RhinoHit", true);
            Destroy(gameObject);
            //Invoke("death", 1f);
        }
    }

    private void death()
    {
        Destroy(gameObject);
    }
}
