using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoBehaviour : MonoBehaviour
{
    private NPCMode npcMode = NPCMode.RhinoWalk;
    public Rigidbody2D rigidkropp;
    public new Animator animation;
    SpriteRenderer spriterenderer;
    public GameObject onDeathBloodSplash;

    float moveSpeed;
    bool movingLeft;
    float HP;
    bool rhinoReadyToBeShotAgain;
    //float hitByShotAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();

        moveSpeed = -3;
        movingLeft = true;
        HP = 3;
        rhinoReadyToBeShotAgain = true;
        //hitByShotAnimation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(npcMode);
        //Sätter animationen beroende på vilket state rhinon är i
        rhinoStateChecker();

        switch (npcMode)
        {
            case NPCMode.RhinoWalk:
                rhinoWalk();
               
                //Tittar om spelaren är nära nog för att gå in i sitt attack state
                RaycastHit2D SeePlayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 8f);
                if (SeePlayer.collider != null && SeePlayer.collider.tag == "Player")
                {
                    rigidkropp.AddForce(new Vector2(0f, 6f), ForceMode2D.Impulse);
                    Invoke("jumpComplete", 0.65f);
                    npcMode = NPCMode.RhinoJumping;
                }

                break;

            case NPCMode.RhinoRun:
                rhinoRun();

                RaycastHit2D HittingSomething = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.8f);
                if (HittingSomething.collider != null && HittingSomething.collider.tag == "Wall")
                {
                    spriterenderer.color = Color.white;
                    rhinoWallOrPlayerHit();
                    Invoke("stunComplete", 2);
                    npcMode = NPCMode.RhinoWallOrPlayerHit;
                }
                else if (HittingSomething.collider != null && HittingSomething.collider.tag == "Player")
                {
                    spriterenderer.color = Color.white;
                    rhinoWallOrPlayerHit();
                    Invoke("stunComplete", 2);
                    npcMode = NPCMode.RhinoWallOrPlayerHit;
                }

                break;

            case NPCMode.RhinoWallOrPlayerHit:

                break;

            case NPCMode.RhinoJumping:
                rhinoJumping();

                break;

            case NPCMode.RhinoHit:
                rhinoHit();

                break;

            default:
                break;
        }

        checkIfDead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && HP >= 1) 
        {
            if (rhinoReadyToBeShotAgain == true)
            {
                Invoke("rhinoShotHitComplete", 0.4f);
                npcMode = NPCMode.RhinoHit;
                rhinoReadyToBeShotAgain = false;
                HP = HP - 1;
            }
            else if (rhinoReadyToBeShotAgain == false)
            {

            }
        }
    }

    private void rhinoWalk()
    {
        //Sätter hastighet då rhinoWalk är aktivt till -3 och nollställer animationstriggern för rhinoWallOrPlayerHit
        moveSpeed = -3;

        //Vänd när Rhino kommer till en vägg
        RaycastHit2D TurnAroundRaycast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1f);
        if (TurnAroundRaycast.collider != null && TurnAroundRaycast.collider.tag == "Wall")
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
        moveSpeed = -7;
        spriterenderer.color = Color.red;

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
            rigidkropp.AddForce(new Vector2(10f, 4f), ForceMode2D.Impulse);
        }
        else if (movingLeft == false)
        {
            rigidkropp.AddForce(new Vector2(-10f, 4f), ForceMode2D.Impulse);
        }
    }

    private void rhinoJumping()
    {
        moveSpeed = 0;
        rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
    }

    private void stunComplete()
    {
        npcMode = NPCMode.RhinoWalk;
    }

    private void jumpComplete()
    {
        npcMode = NPCMode.RhinoRun;
    }

    private void rhinoHit()
    {
        moveSpeed = 0;
        spriterenderer.color = Color.white;
    }

    private void rhinoShotHitComplete()
    {
        rhinoReadyToBeShotAgain = true;
        npcMode = NPCMode.RhinoWalk;
    }

    private void checkIfDead()
    {
        if (HP <= 0)
        {
            onDeathBloodSplash = Instantiate(onDeathBloodSplash, transform.position = new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void rhinoStateChecker()
    {
        if (npcMode == NPCMode.RhinoWalk)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitAnimationComplete", true);
            animation.SetBool("HitWall", false);
            animation.SetBool("HitByShot", false);
            animation.SetBool("Jumping", false);
        }
        else if (npcMode == NPCMode.RhinoRun)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitAnimationComplete", false);
            animation.SetBool("HitWall", false);
            animation.SetBool("HitByShot", false);
            animation.SetBool("Jumping", false);
        }
        else if (npcMode == NPCMode.RhinoWallOrPlayerHit)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitAnimationComplete", false);
            animation.SetBool("HitWall", true);
            animation.SetBool("HitByShot", false);
            animation.SetBool("Jumping", false);
        }
        else if (npcMode == NPCMode.RhinoHit)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitAnimationComplete", false);
            animation.SetBool("HitWall", false);
            animation.SetBool("HitByShot", true);
            animation.SetBool("Jumping", false);
        }
        else if (npcMode == NPCMode.RhinoJumping)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitAnimationComplete", false);
            animation.SetBool("HitWall", false);
            animation.SetBool("HitByShot", false);
            animation.SetBool("Jumping", true);
        }
    }

    public enum NPCMode
    {
        RhinoWalk,
        RhinoRun,
        RhinoWallOrPlayerHit,
        RhinoHit,
        RhinoJumping
    }
}
