using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottSigridBehaviour : MonoBehaviour
{
    public GameObject onDeathBloodAnimation;
    public GameObject spitBulletPrefab;
    private Rigidbody2D rigidkropp;
    public Transform shotPosition;
    public GameObject onDeathBloodParticleSystem;
    public GameObject shotSpitPrefab;
    public GameObject onDeathCoin;
    private State state = State.SpottSigridWalk;
    private SpriteRenderer spriterenderer;
    private Material matWhite; //Anv�nds f�r att blinka vitt n�r fienden tr�ffas av skott
    private Material matDefault; //�terst�ller rhinons materail till default
    private new Animator animation;
    public int points;

    private bool canShoot;
    private float moveSpeed;
    private bool movingLeft;
    [SerializeField] float HP;
    public Transform groundDetection;

    //referens till spelare
    private PlayerController playerController;
    private GameController gameController;
    private SFXController sfxController;

    //Teleporter Transform
    [SerializeField] private Transform teleporterTop;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = spriterenderer.material;
        playerController = PlayerController.InstanceOfPlayer;
        gameController = GameController.InstanceOfGame;
        sfxController = SFXController.InstanceOfSFX;

        canShoot = false;
        moveSpeed = -2;
        movingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            killSelf();
        }
        switch (state)
        {
            case State.SpottSigridWalk:
                spottSigridWalk();
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 10f, Color.green);
                RaycastHit2D SeesPlayerWalkState = Physics2D.Raycast(transform.position, transform.TransformDirection (Vector2.left), 12f);
                if (SeesPlayerWalkState.collider != null && SeesPlayerWalkState.collider.tag == "Player")
                {
                    animation.SetBool("PlayerInSight", true);
                    animation.SetBool("AttackDone", false);
                    Invoke("spitAttack", 0.5f);
                    Invoke("sigriStartdWalking", 1f);
                    state = State.SpottSigridAttack;
                }

                break;

            case State.SpottSigridAttack:
                canShoot = true;
                
                break;

            default:
                break;
        }
    }

    private void spottSigridWalk()
    {
        //F�r SpottSigrid att r�ra sig
        if (movingLeft == true)
        {
            rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
        }
        else if (movingLeft == false)
        {
            rigidkropp.velocity = new Vector2(moveSpeed * -1, rigidkropp.velocity.y);
        }
            
        //V�nd n�r SpottSigrid kommer till en plattforms kant
        RaycastHit2D TurnAroundRaycastEdge = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.5f);
        if (TurnAroundRaycastEdge.collider == false)
        {
            rigidkropp.transform.Rotate(0f, 180f, 0f);
            movingLeft = !movingLeft;
        }

        //V�nd n�r SpottSigrid kommer till en v�gg
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 0.5f, Color.green);
        RaycastHit2D TurnAroundRaycastWall = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.5f);
        if (TurnAroundRaycastWall.collider != null && TurnAroundRaycastWall.collider.tag == "Wall" /*|| TurnAroundRaycastWall.collider != null && TurnAroundRaycastWall.collider.tag == "Enemy"*/)
        {
            rigidkropp.transform.Rotate(0f, 180f, 0f);
            movingLeft = !movingLeft;
        }
    }


    private void spitAttack()
    {
        if ( canShoot == true)
        {
            Instantiate(spitBulletPrefab, shotPosition.position, shotPosition.rotation);
            sfxController.PlaySigridShoot();
            if (movingLeft == false)
            {
                Instantiate(shotSpitPrefab, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), shotSpitPrefab.transform.rotation = Quaternion.Euler(0f, 90f, -90f));
            }
            else if (movingLeft == true)
            {
                Instantiate(shotSpitPrefab, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), shotSpitPrefab.transform.rotation = Quaternion.Euler(180f, 90f, -90f));
            }
            canShoot = false;
        }
    }


    private void sigriStartdWalking()
    {
        animation.SetBool("AttackDone", true);
        animation.SetBool("PlayerInSight", false);
        state = State.SpottSigridWalk;
    }


    private void whiteFlash()
    {
        spriterenderer.material = matWhite;
    }


    private void killSelf()
    {
        onDeathBloodAnimation = Instantiate(onDeathBloodAnimation, transform.position = new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        onDeathBloodParticleSystem = Instantiate(onDeathBloodParticleSystem, transform.position, Quaternion.identity);
        onDeathCoin = Instantiate(onDeathCoin, transform.position, Quaternion.identity);
        sfxController.PlaySigridDeath();
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

            Invoke("resetMaterial", 0.1f);
        }

        if (collision.gameObject.tag == "PlayerExplosion")
        {
            HP = HP - 5;
            whiteFlash();

            Invoke("resetMaterial", 0.2f);
        }


        if (collision.gameObject.tag == "Teleporter")
        {
            Vector2 position = new Vector2(transform.position.x, teleporterTop.position.y);
            transform.position = position;

            Vector2 speed = new Vector2(rigidkropp.velocity.x, -3f);
            rigidkropp.velocity = speed;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        playerController.playerHealth--;
    //    }
    //}

    public enum State
    {
        //SpottSigridIdle,
        SpottSigridWalk,
        SpottSigridAttack
    }
}
