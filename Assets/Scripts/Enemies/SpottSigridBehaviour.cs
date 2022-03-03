using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottSigridBehaviour : MonoBehaviour
{
    private State state = State.SpottSigridWalk;
    public Rigidbody2D rigidkropp;
    public new Animator animation;
    SpriteRenderer spriterenderer;
    public GameObject onDeathTreeSplash;
    private Material matWhite; //Används för att blinka vitt när fienden träffas av skott
    private Material matDefault; //Återställer rhinons materail till default

    private float moveSpeed;
    private bool movingLeft;
    [SerializeField] float HP;
    public Transform groundDetection;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = spriterenderer.material;

        moveSpeed = -2;
        movingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            case State.SpottSigridIdle:
                break;

            case State.SpottSigridWalk:
                spottSigridWalk();

                break;

            case State.SpottSigridAttack:
                break;

            default:
                break;
        }
    }

    private void spottSigridWalk()
    {
        //Får SpottSigrid att röra sig
        if (movingLeft == true)
        {
            rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
        }
        else if (movingLeft == false)
        {
            rigidkropp.velocity = new Vector2(moveSpeed * -1, rigidkropp.velocity.y);
        }
            
        //Vänd när SpottSigrid kommer till en plattforms kant
        RaycastHit2D TurnAroundRaycast = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.5f);
        if (TurnAroundRaycast.collider == false)
        {
            rigidkropp.transform.Rotate(0f, 180f, 0f);
            movingLeft = !movingLeft;
        }
    }



    private void whiteFlash()
    {
        spriterenderer.material = matWhite;
    }


    private void killSelf()
    {
        onDeathTreeSplash = Instantiate(onDeathTreeSplash, transform.position = new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }


    void resetMaterial()
    {
        spriterenderer.material = matDefault;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            HP = HP - 1;
            whiteFlash();

            if (HP < 1)
            {
                killSelf();
            }
            else
            {
                Invoke("resetMaterial", .1f);
            }


        }
    }

    public enum State
    {
        SpottSigridIdle,
        SpottSigridWalk,
        SpottSigridAttack
    }
}
