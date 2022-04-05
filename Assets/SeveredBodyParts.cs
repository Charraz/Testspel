using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeveredBodyParts : MonoBehaviour
{
    public GameObject onDeathBloodAnimation;
    private Rigidbody2D rigidkropp;
    private float spawnVelocityX;
    private float spawnVelocityY;
    private int spawnTorque;
    public int tourqueLow;
    public int torqueHigh;
    [SerializeField] private Transform teleporterTop;

    private void Awake()
    {
        spawnVelocityX = Random.Range(-10f, 10f);
        spawnVelocityY = Random.Range(3f, 5f);
        spawnTorque = Random.Range(tourqueLow, torqueHigh);
    }

    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        rigidkropp.AddForce(new Vector2(spawnVelocityX, spawnVelocityY), ForceMode2D.Impulse);
        rigidkropp.AddTorque(spawnTorque);
        Invoke("Death", 20f);
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Teleporter")
        {
            Vector2 position = new Vector2(transform.position.x, teleporterTop.position.y);
            transform.position = position;

            Vector2 speed = new Vector2(rigidkropp.velocity.x, -3f);
            rigidkropp.velocity = speed;
        }
    }

    private void Death()
    {
        onDeathBloodAnimation = Instantiate(onDeathBloodAnimation, transform.position = new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }
}
