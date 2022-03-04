using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottSigridBehaviour : MonoBehaviour
{
    public GameObject onDeathTreeSplashPrefab;
    public GameObject spitBulletPrefab;
    public Rigidbody2D rigidkropp;
    public Transform shotPosition;
    private State state = State.SpottSigridWalk;
    private SpriteRenderer spriterenderer;
    private Material matWhite; //Används för att blinka vitt när fienden träffas av skott
    private Material matDefault; //Återställer rhinons materail till default
    public new Animator animation;

    private bool canShoot;
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

        canShoot = false;
        moveSpeed = -2;
        movingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            //case State.SpottSigridIdle:
            //    RaycastHit2D SeesPlayerIdleState = Physics2D.Raycast(groundDetection.position, Vector2.left, 10f);
            //    if (SeesPlayerIdleState.collider.tag == "Player")
            //    {
            //        Invoke("spitAttack", 1f);
            //        state = State.SpottSigridAttack;
            //    }
            //    break;

            case State.SpottSigridWalk:
                spottSigridWalk();
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 10f, Color.green);
                RaycastHit2D SeesPlayerWalkState = Physics2D.Raycast(groundDetection.position, Vector2.left, 10f);
                if (SeesPlayerWalkState.collider.tag == "Player")
                {
                    state = State.SpottSigridAttack;
                }

                break;

            case State.SpottSigridAttack:
                canShoot = true;
                Invoke("spitAttack", 0.1f);
                Invoke("sigriStartdWalking", 2f);
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

    //private void spottSigridAttack()
    //{
        
    //}

    private void spitAttack()
    {
        Instantiate(spitBulletPrefab, shotPosition.position, shotPosition.rotation);
        canShoot = false;
    }

    private void sigriStartdWalking()
    {
        state = State.SpottSigridWalk;
    }


    private void whiteFlash()
    {
        spriterenderer.material = matWhite;
    }


    private void killSelf()
    {
        onDeathTreeSplashPrefab = Instantiate(onDeathTreeSplashPrefab, transform.position = new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
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
        //SpottSigridIdle,
        SpottSigridWalk,
        SpottSigridAttack
    }
}
