using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoBehaviour : MonoBehaviour
{
    private State state = State.RhinoWalk;
    public Rigidbody2D rigidkropp;
    public new Animator animation;
    SpriteRenderer spriterenderer;
    public GameObject onDeathBloodAnimation;
    public GameObject onDeathBloodParticleSystem;
    public GameObject onDeathCoin;
    private Material matWhite; //Används för att blinka vitt när fienden träffas av skott
    private Material matRed; //Används för att göra rhinon röd när han är arger
    private Material matDefault; //Återställer rhinons materail till default
    private PlayerController playerController;
    private GameController gameController;
    public Transform groundDetection;
    public int points;
    private SFXController sfxController;

    float moveSpeed;
    bool movingLeft;
    bool isRed;
    [SerializeField] float HP;

    //Teleporter Transform
    [SerializeField] private Transform teleporterTop;

    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
        playerController = PlayerController.InstanceOfPlayer;
        gameController = GameController.InstanceOfGame;
        sfxController = SFXController.InstanceOfSFX;
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matRed = Resources.Load("RedMorning", typeof(Material)) as Material;
        matDefault = spriterenderer.material;

        moveSpeed = -3;
        movingLeft = true;
        isRed = false;
    }

    void Update()
    {
        //Sätter animationen beroende på vilket state rhinon är i
        rhinoStateChecker();

        if (HP <= 0)
        {
            killSelf();
        }

        switch (state)
        {
            case State.RhinoWalk:
                rhinoWalk();
               
                //Tittar om spelaren är nära nog för att gå in i sitt attack state
                RaycastHit2D SeePlayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 20f);
                if (SeePlayer.collider != null && SeePlayer.collider.tag == "Player")
                {
                    rigidkropp.AddForce(new Vector2(0f, 6f), ForceMode2D.Impulse);
                    Invoke("jumpComplete", 0.65f);
                    state = State.RhinoJumping;
                }

                break;

            case State.RhinoRun:
                rhinoRun();

                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 0.8f, Color.green);
                RaycastHit2D HittingSomething = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.8f);
                if (HittingSomething.collider != null && HittingSomething.collider.tag == "Wall" /*|| HittingSomething.collider != null && HittingSomething.collider.tag == "Enemy"*/)
                {
                    //spriterenderer.color = Color.white;
                    resetMaterial();
                    rhinoWallOrPlayerHit();
                    Invoke("stunComplete", 2);
                    state = State.RhinoWallOrPlayerHit;
                }
                //else if (HittingSomething.collider != null && HittingSomething.collider.tag == "Player")
                //{
                //    //spriterenderer.color = Color.white;
                    
                //}
                break;

            case State.RhinoWallOrPlayerHit:
                if (isRed == true)
                {
                    spriterenderer.material = matDefault;
                    isRed = false;
                }

                break;

            case State.RhinoJumping:
                rhinoJumping();
                break;

            default:
                break;
        }
    }

    private void rhinoWalk()
    {
        //Sätter hastighet då rhinoWalk är aktivt till -3 och nollställer animationstriggern för rhinoWallOrPlayerHit
        moveSpeed = -3;

        //Vänd när Rhino kommer till en vägg eller en annan fiende
        RaycastHit2D TurnAroundRaycast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1f);
        if (TurnAroundRaycast.collider != null && TurnAroundRaycast.collider.tag == "Wall" /*|| TurnAroundRaycast.collider != null && TurnAroundRaycast.collider.tag == "Enemy"*/)
        {
            rigidkropp.transform.Rotate(0f, 180f, 0f);
            movingLeft = !movingLeft;
        }

        //Får Rhino att röra sig
        if (movingLeft == true)
        {
            rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
        }
        else if (movingLeft == false)
        {
            rigidkropp.velocity = new Vector2(moveSpeed * -1, rigidkropp.velocity.y);
        }
    }

    private void rhinoRun()
    {
        moveSpeed = -10;
        isRed = true;

        //Får Rhino att röra sig
        if (movingLeft == true)
        {
            rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
        }
        else if (movingLeft == false)
        {
            rigidkropp.velocity = new Vector2(moveSpeed * -1, rigidkropp.velocity.y);
        }
    }

    private void rhinoWallOrPlayerHit()
    {
        sfxController.PlayRhinoChargeHit();

        if (movingLeft == true)
        {
            rigidkropp.AddForce(new Vector2(12f, 4f), ForceMode2D.Impulse);
        }
        else if (movingLeft == false)
        {
            rigidkropp.AddForce(new Vector2(-12f, 4f), ForceMode2D.Impulse);
        }
    }

    private void rhinoJumping()
    {
        sfxController.PlayRhinoRoar();
        moveSpeed = 0;
        rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
    }

    private void stunComplete()
    {
        state = State.RhinoWalk;
    }

    private void jumpComplete()
    {
        spriterenderer.material = matRed;
        state = State.RhinoRun;
    }

    private void killSelf()
    {
        onDeathBloodAnimation = Instantiate(onDeathBloodAnimation, transform.position = new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        onDeathBloodParticleSystem = Instantiate(onDeathBloodParticleSystem, transform.position, Quaternion.identity);
        onDeathCoin = Instantiate(onDeathCoin, transform.position, Quaternion.identity);
        sfxController.PlayRhinoDeath();
        gameController.points += points;
        Destroy(gameObject);
    }

    void resetMaterial()
    {
        if (state == State.RhinoRun)
        {
            spriterenderer.material = matRed;
        }
        else
        {
            spriterenderer.material = matDefault;
        }
    }

    private void whiteFlash()
    {
        spriterenderer.material = matWhite;
    }

    private void rhinoStateChecker()
    {
        switch (state)
        {
            case State.RhinoWalk:
                UpdateAnimation(false, false, false, false);
                break;
            case State.RhinoJumping:
                UpdateAnimation(true, false, true, false);
                break;
            case State.RhinoRun:
                UpdateAnimation(false, false, false, true);
                break;
            case State.RhinoWallOrPlayerHit:
                UpdateAnimation(false, true, false, false);
                break;
        }
    }

    private void UpdateAnimation(bool seesPlayer, bool hitwall, bool jumping, bool charge)
    {
        animation.SetBool("SeesPlayer", seesPlayer);
        animation.SetBool("HitWall", hitwall);
        animation.SetBool("Jumping", jumping);
        animation.SetBool("Charge", charge);
    }

    public enum State
    {
        RhinoWalk,
        RhinoRun,
        RhinoWallOrPlayerHit,
        RhinoJumping
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            HP = HP - 1;
            whiteFlash();
            Invoke("resetMaterial", 0.1f);
            sfxController.PlayRhinoDamaged();
        }

        if (collision.gameObject.tag == "PlayerExplosion")
        {
            HP = HP - 5;
            whiteFlash();
            Invoke("resetMaterial", 0.2f);
            sfxController.PlayRhinoDamaged();
        }

        if (collision.gameObject.tag == "Teleporter")
        {
            Vector2 position = new Vector2(transform.position.x, teleporterTop.position.y);
            transform.position = position;

            Vector2 speed = new Vector2(rigidkropp.velocity.x, -3f);
            rigidkropp.velocity = speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            resetMaterial();
            Invoke("stunComplete", 2);
            rhinoWallOrPlayerHit();
            //playerController.playerHealth--;
            state = State.RhinoWallOrPlayerHit;
        }
    }
}
