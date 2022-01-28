using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleController : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] Rigidbody2D bullet;
    public GameObject blueExplosion;

    float moveSpeed;
    float fallSpeed;
    public bool movingLeft;
    float shotTimer;
    Vector2 bulletSpawn;
    float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        
        movingLeft = true;
        moveSpeed = -3;
        shotTimer = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //Graty
        fallSpeed = rb2D.velocity.y;
    }

    void FixedUpdate()
    {
        if (movingLeft == true)
        {
            rb2D.velocity = new Vector2(moveSpeed, fallSpeed);
        }
        else if (movingLeft == false)
        {
            rb2D.velocity = new Vector2(moveSpeed * -1, fallSpeed);
        }

        shotTimer = shotTimer - 1;
        //if (shotTimer <= 0)
        //{
        //    Vector2 mousePos = Input.mousePosition;
        //    bulletSpawn = new Vector2(0f, 0f);
        //    Rigidbody2D clone;
        //    clone = Instantiate(bullet, bulletSpawn, transform.rotation);
        //    clone.velocity = Vector2.MoveTowards(transform.position, mousePos, bulletSpeed);
        //    shotTimer = 60;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy changes direction when hitting a wall
        if (collision.gameObject.tag == "Wall")
        {
            movingLeft = !movingLeft;
            transform.Rotate(0f, 180f, 0f);
        }

        if(collision.gameObject.tag == "PlayerBullet")
        {
            Object.Destroy(gameObject);
            blueExplosion = Instantiate(blueExplosion, transform.position, Quaternion.identity);
        }
    }
}
