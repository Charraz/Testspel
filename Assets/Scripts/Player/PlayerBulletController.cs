using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    public GameObject bulletEffect;
    public GameObject bulletBloodEffect;
    private CinemachineScreenShake cinemachineScreenShake;

    Vector2 lastVelocity;

    private void Awake()
    {
        cinemachineScreenShake = CinemachineScreenShake.InstanceOfCinemachine;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        transform.Rotate(0, 0, -90);
        Invoke("Death", 3f);
        cinemachineScreenShake.ShakeCamera(1f, 0.1f);
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
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Wall")
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
