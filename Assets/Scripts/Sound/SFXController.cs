using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController InstanceOfSFX;

    #region Variables
    //PlayerSounds
    public AudioSource playerDamaged;
    public AudioSource playerJump;

    //RhinoSounds
    public AudioSource rhinoRoar;
    public AudioSource rhinoDamaged;
    public AudioSource rhinoChargeHit;
    public AudioSource rhinoDeath;

    //SigridSounds
    public AudioSource sigridShoot;
    public AudioSource sigridDamaged;
    public AudioSource sigridDeath;

    //GhostSounds
    public AudioSource ghostDamaged;
    public AudioSource ghostDeath;

    //WeaponSounds
    public AudioSource bulletShoot;
    public AudioSource bulletWallorGroundHit;
    public AudioSource grenadeHit;
    public AudioSource grenadeExplosion;

    //CoinSounds
    public AudioSource coinPickup;

    #endregion

    private void Awake()
    {
        InstanceOfSFX = this;
    }
    private void Start()
    {

    }

    #region PlayerSounds
    public void PlayPlayerDamaged()
    {
        playerDamaged.Play();
    }

    public void PlayPlayerJump()
    {
        playerJump.Play();
    }
    #endregion

    #region RhinoSounds
    public void PlayRhinoRoar()
    {
        rhinoRoar.Play();
    }

    public void PlayRhinoDamaged()
    {
        rhinoDamaged.Play();
    }

    public void PlayRhinoChargeHit()
    {
        rhinoChargeHit.Play();
    }
    #endregion

    #region SigridSounds
    public void PlaySigridShoot()
    {
        sigridShoot.Play();
    }

    public void PlaySigridDamaged()
    {
        sigridDamaged.Play();
    }

    public void PlaySigridDeath()
    {
        sigridDeath.Play();
    }
    #endregion

    #region GhostSounds

    public void PlayGhostDamaged()
    {
        ghostDamaged.Play();
    }

    public void PlayGhostDeath()
    {
        ghostDeath.Play();
    }
    #endregion

    #region WeaponSounds

    public void PlayBulletShoot()
    {
        bulletShoot.Play();
    }

    public void PlayBulletWallorGroundHit()
    {
        bulletWallorGroundHit.Play();
    }

    public void PlayGrenadeHit()
    {
        grenadeHit.Play();
    }

    public void PlayGrenadeExplosion()
    {
        grenadeExplosion.Play();
    }

    #endregion

    #region CoinSounds
    public void PlayCoinPickup()
    {
        coinPickup.Play();
    }
    #endregion
}
