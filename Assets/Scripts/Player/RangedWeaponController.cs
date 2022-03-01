using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject myPlayer;
    public GameObject rocketPrefab;
    private bool weapon1CD;
    private bool weapon2CD;


    private void Start()
    {
        weapon1CD = false;
        weapon2CD = false;
    }
    // Update is called once per frame

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && weapon1CD == false)
        {
            Shoot();
            weapon1CD = true;
            Invoke("Weapon1CDActive", 0.3f);

        }

        if (Input.GetButtonDown("Fire2") && weapon2CD == false)
        {
            ShootRocket();
            weapon2CD = true;
            Invoke("Weapon2CDActive", 1f);
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
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }
        }
        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);


    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void ShootRocket()
    {
        Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
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
