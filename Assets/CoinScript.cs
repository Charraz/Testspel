using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Rigidbody2D rigidkropp;
    private BoxCollider2D boxCollider;
    public GameObject coinPickedUp;
    [SerializeField] private LayerMask playerMask;

    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Något kul");
            Instantiate(coinPickedUp, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
    }
}
