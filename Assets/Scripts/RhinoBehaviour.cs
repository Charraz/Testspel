using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoBehaviour : MonoBehaviour
{
    private NPCMode npcMode = NPCMode.RhinoWalk;
    public Rigidbody2D rigidkropp;
    public new Animator animation;

    float moveSpeed;
    bool movingLeft;
    float rhinoHitAnimationComplete;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();

        moveSpeed = -3;
        movingLeft = true;
        rhinoHitAnimationComplete = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Sätter animationen beroende på vilket state rhinon är i
        rhinoStateChecker();

        Debug.Log(npcMode);
        switch (npcMode)
        {
            case NPCMode.RhinoWalk:
                rhinoWalk();
               
                //Tittar om spelaren är nära nog för att gå in i sitt attack state
                RaycastHit2D SeePlayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 5f);
                if (SeePlayer.collider != null && SeePlayer.collider.tag == "Player")
                {
                    npcMode = NPCMode.RhinoRun;
                }
                break;

            case NPCMode.RhinoRun:
                rhinoRun();

                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 0.8f, Color.green);
                RaycastHit2D HittingSomething = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.8f);
                if (HittingSomething.collider != null && HittingSomething.collider.tag == "Wall")
                {
                    npcMode = NPCMode.RhinoWallOrPlayerHit;
                }
                else if (HittingSomething.collider != null && HittingSomething.collider.tag == "Player")
                {
                    npcMode = NPCMode.RhinoWallOrPlayerHit;
                }

                break;

            case NPCMode.RhinoWallOrPlayerHit:
                rhinoWallOrPlayerHit();

                if (rhinoHitAnimationComplete >= 120)
                {
                    rigidkropp.transform.Rotate(0f, 180f, 0f);
                    movingLeft = !movingLeft;
                    npcMode = NPCMode.RhinoWalk;
                }

                rhinoHitAnimationComplete = rhinoHitAnimationComplete + 1;

                break;

            //case NPCMode.RhinoHit:
            //    rhinoHit();
            //    break;

            default:
                break;
        }
    }

    private void rhinoWalk()
    {
        //Sätter hastighet då rhinoWalk är aktivt till -3 och nollställer animationstriggern för rhinoWallOrPlayerHit
        moveSpeed = -3;
        rhinoHitAnimationComplete = 0;

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
            rigidkropp.velocity = new Vector2(moveSpeed * -1, rigidkropp.velocity.y);
        }
        else if (movingLeft == false)
        {
            rigidkropp.velocity = new Vector2(moveSpeed, rigidkropp.velocity.y);
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
        }
        else if (npcMode == NPCMode.RhinoRun)
        {
            animation.SetBool("SeesPlayer", true);
            animation.SetBool("HitAnimationComplete", false);
            animation.SetBool("HitWall", false);
            animation.SetBool("HitByShot", false);
        }
        else if (npcMode == NPCMode.RhinoWallOrPlayerHit)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitAnimationComplete", false);
            animation.SetBool("HitWall", true);
            animation.SetBool("HitByShot", false);
        }
        else if (npcMode == NPCMode.RhinoHit)
        {
            animation.SetBool("SeesPlayer", false);
            animation.SetBool("HitAnimationComplete", false);
            animation.SetBool("HitWall", false);
            animation.SetBool("HitByShot", true);
        }
    }

    public enum NPCMode
    {
        RhinoWalk,
        RhinoRun,
        RhinoWallOrPlayerHit,
        RhinoHit
    }
}
