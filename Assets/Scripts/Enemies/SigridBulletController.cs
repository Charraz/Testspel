using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigridBulletController : MonoBehaviour
{
    private float speed = -8f;
    private bool bulletGravityOn = false;
    private Rigidbody2D rigidkroppBullet;

    //referens till spelare
    private PlayerController playerController;
    

    private void Awake()
    {
        playerController = PlayerController.InstanceOfPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidkroppBullet = GetComponent<Rigidbody2D>();
        rigidkroppBullet.velocity = transform.right * speed;
        Invoke("bulletGravity", 1.5f);
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if (bulletGravityOn == true)
        {
            rigidkroppBullet.AddForce(new Vector2(0f, -3f));
        }
    }

    private void bulletGravity()
    {
        bulletGravityOn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //playerController.playerHealth--;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
