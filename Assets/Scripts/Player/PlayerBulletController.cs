using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    public GameObject bulletEffect;
    public GameObject bulletBloodEffect;

    Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        transform.Rotate(0, 0, -90);
        //Döda bullet efter 3 sekunder
        Invoke("Death", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    //Händelser när bullet träffar specifika colliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(bulletBloodEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Object.Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Wall"/* || collision.gameObject.tag == "Ground"*/)
        {
            Destroy(gameObject);
            Instantiate(bulletEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
