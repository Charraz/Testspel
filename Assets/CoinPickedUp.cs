using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickedUp : MonoBehaviour
{
    private Rigidbody2D rigidkropp;
    public float jumpSpeed;

    private void Awake()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
    }
    

    void Start()
    {
        rigidkropp.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        Invoke("Death", 0.5f);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
