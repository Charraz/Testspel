using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigridBulletController : MonoBehaviour
{
    private float speed = -8f;
    private bool bulletGravityOn = false;
    private Rigidbody2D rigidkroppBullet;

    // Start is called before the first frame update
    void Start()
    {
        rigidkroppBullet = GetComponent<Rigidbody2D>();
        rigidkroppBullet.velocity = transform.right * speed;
        Invoke("bulletGravity", 0.5f);
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        if (bulletGravityOn == true)
        {
            rigidkroppBullet.AddForce(new Vector2(0f, -3f));
        }
    }

    private void bulletGravity()
    {
        bulletGravityOn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
