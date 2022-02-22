using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public GameObject particleExplosion;
    Collider2D activeExplosion;
    public float explosionRadius;
    int hit;

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
        particleExplosion = Instantiate(particleExplosion, transform.position, Quaternion.identity);
        activeExplosion = Physics2D.OverlapCircle(transform.position, explosionRadius);
        hit++;
        Debug.Log(hit);

        if (activeExplosion.tag == "Enemy")
        {
            Debug.Log("OOOOO");
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            hit++;
            Debug.Log(hit);
            particleExplosion = Instantiate(particleExplosion, transform.position, Quaternion.identity);
            activeExplosion = Physics2D.OverlapCircle(transform.position, explosionRadius);

            if (activeExplosion.tag == "Enemy")
            {
                Debug.Log("OOOOO");
            }
        }

        Destroy(gameObject);
    }
}
