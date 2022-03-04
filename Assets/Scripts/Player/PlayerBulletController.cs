using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;

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
            
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
