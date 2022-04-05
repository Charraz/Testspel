using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController InstanceOfSFX;

    #region Variables

    //Music
    public AudioSource backgroundMusic;

    //PlayerSounds
    public AudioSource playerDamaged;
    public AudioSource playerJump;
    public AudioSource playerDoubleJump;
    public AudioSource playerDeath;

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
    public AudioSource ghostDeathSound;
    public AudioSource ghostSpawn;

    //WeaponSounds
    public AudioSource bulletShoot;
    public AudioSource bulletWallorGroundHit;
    public AudioSource grenadeHit;
    public AudioSource grenadeExplosion;
    public AudioSource grenadeThrow;

    //CoinSounds
    public AudioSource coinPickup;
    public AudioSource chestPickup;
    public AudioSource chestSpawn;
    public AudioSource chestActive;

    #endregion

    private void Awake()
    {
        InstanceOfSFX = this;
    }
    private void Start()
    {

    }

    #region BackgroundMusic

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
    #endregion

    #region PlayerSounds
    public void PlayPlayerDamaged()
    {
        playerDamaged.Play();
    }

    public void PlayPlayerJump()
    {
        playerJump.Play();
    }

    public void PlayPlayerDoubleJump()
    {
        playerDoubleJump.Play();
    }
    public void PlayPlayerDeath()
    {
        playerDeath.Play();
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

    public void PlayRhinoDeath()
    {
        rhinoDeath.Play();
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

    public void PlayGhostDeathSound()
    {
        ghostDeathSound.Play();
    }

    public void PlayGhostSpawn()
    {
        ghostSpawn.Play();
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

    public void PlayGrenadeThrow()
    {
        grenadeThrow.Play();
    }
    #endregion

    #region CoinSounds
    public void PlayCoinPickup()
    {
        coinPickup.Play();
    }
    public void PlayChestPickup()
    {
        chestPickup.Play();
    }

    public void PlayChestSpawn()
    {
        chestSpawn.Play();
    }

    public void PlayChestActive()
    {
        chestActive.Play();
    }

    public void StopChestActive()
    {
        chestActive.Stop();
    }
    #endregion
}
