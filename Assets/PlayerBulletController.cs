using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Object.Destroy(gameObject);
            Debug.Log("Enemy");
        }

        if (collision.gameObject.tag == "Wall")
        {
            Object.Destroy(gameObject);
            Debug.Log("Wall");
        }
    }
}
