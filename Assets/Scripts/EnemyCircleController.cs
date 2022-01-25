using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleController : MonoBehaviour
{
    Rigidbody2D rb2D;

    float moveSpeed;
    bool movingLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        
        movingLeft = true;
        moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(movingLeft == true)
        {
            moveSpeed = -5;
        }
        else if(movingLeft == false)
        {
            moveSpeed = 5;
        }
    }

    void FixedUpdate()
    {
        if (movingLeft == true)
        {
            rb2D.AddForce(new Vector2(moveSpeed, 0f));
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground" && movingLeft == true)
    //    {
    //        rb2D.AddForce(new Vector2(0f, 0f));
    //        movingLeft = false;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground" && movingLeft == false)
    //    {
    //        rb2D.AddForce(new Vector2(0f, 0f));
    //        movingLeft = true;
    //    }
    //}
}
