using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject particleExplosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Invoke("Death", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            particleExplosion = Instantiate(particleExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
