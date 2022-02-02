using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoEnemy : MonoBehaviour
{
    public Rigidbody2D rigidkropp;

    float moveSpeed;
    float fallSpeed;
    bool movingLeft;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();

        movingLeft = true;
        moveSpeed = -3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void FixedUpdate()
    //{
    //    if (movingLeft == true)
    //    {
    //        rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
    //    }
    //    else if (movingLeft == false)
    //    {
    //        rigidkropp.velocity = new Vector2(moveSpeed * -1, rigidkropp.velocity.y);
    //    }

    //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 0.7f, Color.green);
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.7f);
    //    if (hit.collider.tag == "Wall")
    //    {
    //        rigidkropp.transform.Rotate(0f, 180f, 0f);
    //       // movingLeft = !movingLeft;
    //    }
    //    Debug.Log(movingLeft);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    //Death if colliding with a player bullet.
    //    if (collision.gameObject.tag == "PlayerBullet")
    //    {
    //        Object.Destroy(gameObject);
    //    }
    //}
}
