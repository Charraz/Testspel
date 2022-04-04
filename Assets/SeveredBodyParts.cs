using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeveredBodyParts : MonoBehaviour
{
    public GameObject onDeathBloodAnimation;
    public GameObject onDeathBloodParticleSystem;
    private Rigidbody2D rigidkropp;
    private float spawnVelocityX;
    private float spawnVelocityY;

    private void Awake()
    {
        spawnVelocityX = Random.Range(-5f, 5f);
        spawnVelocityY = Random.Range(3f, 7f);
    }

    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        rigidkropp.AddForce(new Vector2(spawnVelocityX, spawnVelocityY), ForceMode2D.Impulse);
        Invoke("Death", 10f);
    }


    void Update()
    {
        
    }

    private void Death()
    {
        onDeathBloodAnimation = Instantiate(onDeathBloodAnimation, transform.position = new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        onDeathBloodParticleSystem = Instantiate(onDeathBloodParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
