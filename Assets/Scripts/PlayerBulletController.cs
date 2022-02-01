using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    Vector2 lastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

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
            Object.Destroy(gameObject);
            Debug.Log("Enemy");
        }

        //if (collision.gameObject.tag == "Wall")
        //{
        //    rb.velocity = rb.velocity * -1;
        //    //Object.Destroy(gameObject);
        //    Debug.Log("Wall");
        //}

        //if (collision.gameObject.tag == "Ground")
        //{
        //    rb.velocity = rb.velocity * -1;
        //}
    }

    //Studsar bullet när den kolliderar med colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0f);

        Debug.Log("Hit");
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
