using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Rigidbody2D rigidkropp;
    private BoxCollider2D boxCollider;
    private SpriteRenderer coinSpriteRenderer;
    public GameObject coinPickedUp;
    [SerializeField] private LayerMask playerMask;
    private GameController gameController;
    private SFXController sfxController;
    public int points;
    private float spawnVelocityX;
    private float spawnVelocityY;

    //Teleporter Transform
    [SerializeField] private Transform teleporterTop;

    private void Awake()
    {
        spawnVelocityX = Random.Range(-3f, 3f);
        spawnVelocityY = Random.Range(3f, 5f);
    }

    void Start()
    {
        gameController = GameController.InstanceOfGame;
        sfxController = SFXController.InstanceOfSFX;
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        coinSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        rigidkropp.AddForce(new Vector2(spawnVelocityX, spawnVelocityY), ForceMode2D.Impulse);
        Invoke("BlinkWhenSoonDespawning", 25);
        Invoke("KillSelf", 30f);
    }

    void Update()
    {
        if (IsTouchingPlayer())
        {
            Instantiate(coinPickedUp, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            gameController.points += points;
            sfxController.PlayCoinPickup();
            Destroy(gameObject);
        }
    }

    private bool IsTouchingPlayer()
    {
        float extraLength = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, extraLength, playerMask);
        //Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.up * (boxCollider.bounds.extents.y + extraLength), Color.green);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(coinPickedUp, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            gameController.points += points;
            sfxController.PlayCoinPickup();
            Destroy(gameObject);
        }
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

    private void BlinkWhenSoonDespawning()
    {
        Invoke("Transparence", 0f);
        Invoke("TransparenceReset", 0.2f);
        Invoke("Transparence", 0.4f);
        Invoke("TransparenceReset", 0.6f);
        Invoke("Transparence", 0.8f);
        Invoke("TransparenceReset", 1f);
        Invoke("Transparence", 1.2f);
        Invoke("TransparenceReset", 1.4f);
        Invoke("Transparence", 1.6f);
        Invoke("TransparenceReset", 1.8f);
        Invoke("Transparence", 2f);
        Invoke("TransparenceReset", 2.2f);
        Invoke("Transparence", 2.4f);
        Invoke("TransparenceReset", 2.6f);
        Invoke("Transparence", 2.8f);
        Invoke("TransparenceReset", 3f);
        Invoke("Transparence", 3.2f);
        Invoke("TransparenceReset", 3.4f);
        Invoke("Transparence", 3.6f);
        Invoke("TransparenceReset", 3.8f);
        Invoke("Transparence", 4f);
        Invoke("TransparenceReset", 4.2f);
        Invoke("Transparence", 4.4f);
        Invoke("TransparenceReset", 4.6f);
        Invoke("Transparence", 4.8f);
        Invoke("TransparenceReset", 5f);
    }

    private void Transparence()
    {
        coinSpriteRenderer.color = new Color(1, 1, 1, 0);
    }

    private void TransparenceReset()
    {
        coinSpriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void KillSelf()
    {
        Destroy(gameObject);
    }
}
