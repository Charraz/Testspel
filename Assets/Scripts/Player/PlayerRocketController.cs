using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public GameObject particleExplosion;
    public GameObject explosionRadius;
    private SFXController sfxController;
    private CinemachineScreenShake cinemachineScreenShake;

    private void Awake()
    {
        cinemachineScreenShake = CinemachineScreenShake.InstanceOfCinemachine;
    }

    // Start is called before the first frame update
    void Start()
    {
        sfxController = SFXController.InstanceOfSFX;
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
        particleExplosion = Instantiate(particleExplosion, new Vector2(transform.position.x, transform.position.y + 1.6f), Quaternion.identity);
        explosionRadius = Instantiate(explosionRadius, transform.position, Quaternion.identity);
        sfxController.PlayGrenadeExplosion();
        cinemachineScreenShake.ShakeCamera(4f, 0.3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
            //particleExplosion = Instantiate(particleExplosion, new Vector2(transform.position.x, transform.position.y + 1.6f), Quaternion.identity);
            //explosionRadius = Instantiate(explosionRadius, transform.position, Quaternion.identity);
            //sfxController.PlayGrenadeExplosion();
            //Destroy(gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Traps")
        {
            Death();
            //particleExplosion = Instantiate(particleExplosion, transform.position = new Vector2(transform.position.x, transform.position.y + 1.6f), Quaternion.identity);
            //explosionRadius = Instantiate(explosionRadius, transform.position, Quaternion.identity);
            //sfxController.PlayGrenadeExplosion();
            //Destroy(gameObject);
        }


    }
}
