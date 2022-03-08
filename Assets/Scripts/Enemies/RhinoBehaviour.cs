using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoBehaviour : MonoBehaviour
{
    private State state = State.RhinoWalk;
    public Rigidbody2D rigidkropp;
    public new Animator animation;
    SpriteRenderer spriterenderer;
    public GameObject onDeathBloodSplash;
    private Material matWhite; //Används för att blinka vitt när fienden träffas av skott
    private Material matRed; //Används för att göra rhinon röd när han är arger
    private Material matDefault; //Återställer rhinons materail till default
    private PlayerController playerController;
    public Transform groundDetection;

    float moveSpeed;
    bool movingLeft;
    bool isRed;
    [SerializeField] float HP;

    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
        playerController = PlayerController.InstanceOfPlayer;
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
                RaycastHit2D SeePlayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 8f);
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
                if (HittingSomething.collider != null && HittingSomething.collider.tag == "Wall" || HittingSomething.collider != null && HittingSomething.collider.tag == "Enemy")
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
        if (TurnAroundRaycast.collider != null && TurnAroundRaycast.collider.tag == "Wall" || TurnAroundRaycast.collider != null && TurnAroundRaycast.collider.tag == "Enemy")
        {
            rigidkropp.transform.Rotate(0f, 180f, 0f);
            movingLeft = !movingLeft;
        }

        //Vänd när Rhino kommer till en plattforms kant
        RaycastHit2D TurnAroundRaycastEdge = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.5f);
        if (TurnAroundRaycastEdge.collider == false)
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
         onDeathBloodSplash = Instantiate(onDeathBloodSplash, transform.position = new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
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
        if (state == State.RhinoWalk)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitWall", false);
            animation.SetBool("Jumping", false);
            animation.SetBool("Charge", false);
        }
        else if (state == State.RhinoJumping)
        {
            animation.SetBool("SeesPlayer", true);
            animation.SetBool("HitWall", false);
            animation.SetBool("Jumping", true);
            animation.SetBool("Charge", false);
        }
        else if (state == State.RhinoRun)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitWall", false);
            animation.SetBool("Jumping", false);
            animation.SetBool("Charge", true);
        }
        else if (state == State.RhinoWallOrPlayerHit)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitWall", true);
            animation.SetBool("Jumping", false);
            animation.SetBool("Charge", false);
        }
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
        }

        if (collision.gameObject.tag == "PlayerExplosion")
        {
            HP = HP - 5;
            whiteFlash();
            Invoke("resetMaterial", 0.2f);
        }

        if (collision.gameObject.tag == "Player")
        {
            resetMaterial();
            Invoke("stunComplete", 2);
            rhinoWallOrPlayerHit();
            playerController.playerHealth--;
            state = State.RhinoWallOrPlayerHit;
        }
    }
}
