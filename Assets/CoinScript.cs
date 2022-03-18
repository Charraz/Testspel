using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public new Animator animation;
    private Rigidbody2D rigidkropp;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerMask;

    void Start()
    {
        animation = gameObject.GetComponent<Animator>();
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Debug.Log(DetectPlayer());

        if (DetectPlayer())
        {
            rigidkropp.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            animation.SetBool("IsPickedUp", true);
            Invoke("Death", 0.2f);
        }
    }

    private bool DetectPlayer()
    {
        RaycastHit2D detectPlayerRaycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up , 0.1f, playerMask);
        return detectPlayerRaycast.collider != null;
    }     

    private void Death()
    {
        Destroy(gameObject);
    }
}
