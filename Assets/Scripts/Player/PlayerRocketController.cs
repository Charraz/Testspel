using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public GameObject particleExplosion;
    public GameObject explosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        Invoke("Death", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Death()
    {
        particleExplosion = Instantiate(particleExplosion, transform.position = new Vector2(transform.position.x, transform.position.y + 1.6f), Quaternion.identity);
        explosionRadius = Instantiate(explosionRadius, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            particleExplosion = Instantiate(particleExplosion, transform.position = new Vector2(transform.position.x, transform.position.y + 1.6f), Quaternion.identity);
            explosionRadius = Instantiate(explosionRadius, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


    }
}