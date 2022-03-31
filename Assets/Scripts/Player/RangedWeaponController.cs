using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlash;
    public GameObject myPlayer;
    public GameObject rocketPrefab;
    public Animator gunAnimator;
    public ParticleSystem bulletShells;
    private bool weapon1CD;
    private bool weapon2CD;

    private SFXController sfxController;
    private PlayerController playerController;

    //Animatorbools
    //private bool pointRight;
    //private bool pointLeft;
    //private bool pointUp;
    //private bool pointDown;


    private void Start()
    {
        weapon1CD = false;
        weapon2CD = false;

        sfxController = SFXController.InstanceOfSFX;
        playerController = PlayerController.InstanceOfPlayer;

        //pointRight = true;
        //pointLeft = false;
        //pointUp = false;
        //pointDown = false;
    }
    // Update is called once per frame

    private void Update()
    {

        if (playerController.playerHealth == 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetButton("Fire1") && weapon1CD == false && playerController.playerHealth > 0)
        {
            Shoot();
            weapon1CD = true;
            Invoke("Weapon1CDActive", 0.2f);
            
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            bulletShells.Stop();
        }

        if (Input.GetButtonDown("Fire2") && weapon2CD == false && playerController.playerHealth > 0)
        {
            ShootRocket();
            weapon2CD = true;
            Invoke("Weapon2CDActive", 2f);
        }
    }
    void FixedUpdate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90)
        {
            if (myPlayer.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
                
            }

            else if (myPlayer.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 0, rotationZ);
            }
        }

        //if (rotationZ > -90 && rotationZ < 90 && transform.eulerAngles.y == 0)
        //{
        //    pointRight = true;
        //    pointLeft = false;
        //    pointUp = false;
        //    pointDown = false;
        //    //Debug.Log("RIGHT");
        //}

        //if (rotationZ > -90 && rotationZ < 90 && transform.eulerAngles.y == 180)
        //{
        //    pointRight = false;
        //    pointLeft = true;
        //    pointUp = false;
        //    pointDown = false;
        //    //Debug.Log("LEFT");
        //}

        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);


    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        gunAnimator.SetTrigger("Shoot");
        sfxController.PlayBulletShoot();
        bulletShells.Play();
    }

    void ShootRocket()
    {
        Instantiate(rocketPrefab, firePoint.position, transform.rotation);
        sfxController.PlayGrenadeThrow();
    }

    void Weapon1CDActive()
    {
        weapon1CD = false;
    }

    void Weapon2CDActive()
    {
        weapon2CD = false;
    }
}
