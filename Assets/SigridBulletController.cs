using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigridBulletController : MonoBehaviour
{

    //public float speed;
    private Rigidbody2D rigidkropp;

    //Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = GetComponent<Rigidbody2D>();
        //rigidkropp.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        rigidkropp.velocity = new Vector2(-10f, 0f);
        //lastVelocity = rigidkropp.velocity;
    }

    //Händelser när bullet träffar specifika colliders
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject == true)
    //    {
    //        Object.Destroy(gameObject);
    //    }
    //}
}
