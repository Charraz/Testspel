using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public GameObject spelarn;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private int HP;
    private Rigidbody2D rigidkropp;
    private new Animator animation;
    private SpriteRenderer spriterenderer;
    private Material matWhite; //Används för att blinka vitt när fienden träffas av skott
    private Material matDefault; //Återställer rhinons materail till default

    //referar till player
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        playerController = spelarn.GetComponent<PlayerController>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = spriterenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            animation.SetBool("IsDead", true);
            Invoke("killSelf", .5f);
        }
        transform.position = Vector2.MoveTowards(transform.position, spelarn.transform.position, speed * Time.deltaTime);

        if (spelarn.transform.position.x > rigidkropp.transform.position.x)
        {
            spriterenderer.flipX = true;
        }
        else
        {
            spriterenderer.flipX = false;
        }
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
            HP = HP - 2;
            whiteFlash();
            Invoke("resetMaterial", 0.2f);
        }

        if (collision.gameObject.tag == "Player")
        {
            playerController.playerHealth--;
            if (spriterenderer.flipX == true)
            {
                transform.Translate(new Vector2(-7f, 0f));
            }

            else if (spriterenderer.flipX == false)
            {
                transform.Translate(new Vector2(7f, 0f));
            }

        }
    }

    private void killSelf()
    {
        Destroy(gameObject);
    }

    private void whiteFlash()
    {
        spriterenderer.material = matWhite;
    }

    void resetMaterial()
    {
        spriterenderer.material = matDefault;
    }


}
