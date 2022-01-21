using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidBody;
    Vector2 speed;
    float maxSpeed;
    float moveHorizontal;
    float xValue;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        maxSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        speed = playerRigidBody.velocity;
        Debug.Log(xValue);
        xValue = speed.x;
    }

    void FixedUpdate()
    {
        if(moveHorizontal > 0.1f && xValue < maxSpeed)
        {
            playerRigidBody.AddForce(new Vector2(1f, 0f), ForceMode2D.Impulse);
        }
        else if(moveHorizontal < -0.1f && xValue > maxSpeed * -1)
        {
            playerRigidBody.AddForce(new Vector2(-1f, 0f), ForceMode2D.Impulse);
        }
        else if(moveHorizontal == 0)
        {
            playerRigidBody.velocity = playerRigidBody.velocity * 0;
        }
    }
}
