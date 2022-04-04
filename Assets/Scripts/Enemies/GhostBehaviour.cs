using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private int HP;
    private Rigidbody2D rigidkropp;
    private SpriteRenderer spriterenderer;
    private Material matWhite; //Används för att blinka vitt när fienden träffas av skott
    private Material matDefault; //Återställer rhinons materail till default
    public GameObject ghostDeathEffect;
    public GameObject onDeathGooParticleSystem;
    public GameObject onDeathCoin;
    public int points;

    //referar till player
    private PlayerController playerController;
    private GameController gameController;
    private SFXController sfxController;

    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = gameObject.GetComponent<Rigidbody2D>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        playerController = PlayerController.InstanceOfPlayer;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = spriterenderer.material;
        gameController = GameController.InstanceOfGame;
        sfxController = SFXController.InstanceOfSFX;
        sfxController.PlayGhostSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            sfxController.PlayGhostDeath();
            sfxController.PlayGhostDeathSound();
            Instantiate(ghostDeathEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            onDeathGooParticleSystem = Instantiate(onDeathGooParticleSystem, transform.position, Quaternion.identity);
            onDeathCoin = Instantiate(onDeathCoin, transform.position, Quaternion.identity);
            sfxController.PlayGhostDeath();
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, playerController.transform.position, speed * Time.deltaTime);

        if (playerController.transform.position.x > rigidkropp.transform.position.x)
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
            sfxController.PlayGhostDamaged();
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

    private void whiteFlash()
    {
        spriterenderer.material = matWhite;
    }

    void resetMaterial()
    {
        spriterenderer.material = matDefault;
    }


}
