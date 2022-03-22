using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    private Rigidbody2D rigidkropp;
    private BoxCollider2D boxCollider;
    public GameObject chestPickedUp;
    public GameObject coinParticleSystem;
    [SerializeField] private LayerMask playerMask;
    private GameController gameController;
    public int points;

    void Start()
    {
        gameController = GameController.InstanceOfGame;
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (IsTouchingPlayer())
        {
            Instantiate(chestPickedUp, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Instantiate(coinParticleSystem, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation = Quaternion.Euler(-90f, 0f, 0f));
            gameController.points += points;
            Destroy(gameObject);
        }
    }

    private bool IsTouchingPlayer()
    {
        float extraLength = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, extraLength, playerMask);

        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.up * (boxCollider.bounds.extents.y + extraLength), Color.green);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.up * (boxCollider.bounds.extents.y + extraLength), Color.green);
        //Debug.DrawRay(boxCollider.bounds.center - new Vector3(0, boxCollider.bounds.extents.y), Vector2.right * (boxCollider.bounds.extents.x), Color.green);

        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(chestPickedUp, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Instantiate(coinParticleSystem, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation = Quaternion.Euler(-90f, 0f, 0f));
            gameController.points += points;
            Destroy(gameObject);
        }
    }
}
