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
    float HP;
    float rhinoHitAnimationComplete;
    float hitByShotAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();

        moveSpeed = -3;
        movingLeft = true;
        HP = 2;
        rhinoHitAnimationComplete = 0;
        hitByShotAnimation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //S�tter animationen beroende p� vilket state rhinon �r i
        rhinoStateChecker();

        Debug.Log(npcMode);
        switch (npcMode)
        {
            case NPCMode.RhinoWalk:
                rhinoWalk();
               
                //Tittar om spelaren �r n�ra nog f�r att g� in i sitt attack state
                RaycastHit2D SeePlayer = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 5f);
                if (SeePlayer.collider != null && SeePlayer.collider.tag == "Player")
                {
                    npcMode = NPCMode.RhinoRun;
                }

                break;

            case NPCMode.RhinoRun:
                rhinoRun();
                
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

                if (rhinoHitAnimationComplete >= 60)
                {
                    rigidkropp.transform.Rotate(0f, 180f, 0f);
                    movingLeft = !movingLeft;
                    npcMode = NPCMode.RhinoWalk;
                }

                rhinoHitAnimationComplete = rhinoHitAnimationComplete + 1;

                break;

            //case NPCMode.RhinoHit:
            //    rhinoHit();
                
            //    if (hitByShotAnimation >= 60)
            //    {
            //        npcMode = NPCMode.RhinoWalk;
            //    }

            //    break;

            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            npcMode = NPCMode.RhinoHit;
        }
    }

    private void rhinoWalk()
    {
        //S�tter hastighet d� rhinoWalk �r aktivt till -3 och nollst�ller animationstriggern f�r rhinoWallOrPlayerHit
        moveSpeed = -3;
        rhinoHitAnimationComplete = 0;

        //V�nd n�r Rhino kommer till en v�gg
        RaycastHit2D TurnAroundRaycast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1f);
        if (TurnAroundRaycast.collider != null && TurnAroundRaycast.collider.tag == "Wall")
        {
            rigidkropp.transform.Rotate(0f, 180f, 0f);
            movingLeft = !movingLeft;
        }

        //F�r Rhino att r�ra sig
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

        //F�r Rhino att r�ra sig
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

    //private void rhinoHit()
    //{
    //    if (HP > 0)
    //    {
    //        HP = HP - 1;
            
    //    }
    //    else if (HP < 1)
    //    {
    //        Object.Destroy(gameObject);
    //    }

    //    npcMode = NPCMode.RhinoWalk;
    //}

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
