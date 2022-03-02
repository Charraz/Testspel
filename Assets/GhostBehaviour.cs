using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject spelarN;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private int HP;
    public Rigidbody2D rigidkropp;
    public new Animator animation;
    private SpriteRenderer spriterenderer;
    private Material matWhite; //Anv�nds f�r att blinka vitt n�r fienden tr�ffas av skott
    private Material matDefault; //�terst�ller rhinons materail till default

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = spriterenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, spelarN.transform.position, speed * Time.deltaTime);

        if (spelarN.transform.position.x > rigidkropp.transform.position.x)
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

            if (HP < 1)
            {
                animation.SetBool("IsDead", true);
                Invoke("killSelf", .5f);
            }
            else
            {
                Invoke("resetMaterial", .1f);
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