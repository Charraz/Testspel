using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private SpriteRenderer spriterenderer;
    private Material matWhite;
    private Material matDefault;

    private bool readyToBlink;

    void Start()
    {
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = spriterenderer.material;
        readyToBlink = true;
        enemySpawner = EnemySpawner.InstanceOfEnemySpawner;
    }


    void Update()
    {
        CheckIfIShouldBlink();
    }

    private void blinkingPortalEffect()
    {
        Invoke("whiteFlash", 0f);
        Invoke("resetMaterial", 0.2f);
        Invoke("whiteFlash", 0.4f);
        Invoke("resetMaterial", 0.6f);
        Invoke("whiteFlash", 0.8f);
        Invoke("resetMaterial", 1f);
        Invoke("ReadyToBlink", 1.1f);
    }



    private void ReadyToBlink()
    {
        readyToBlink = true;
    }



    void resetMaterial()
    {
        spriterenderer.material = matDefault;
    }



    private void whiteFlash()
    {
        spriterenderer.material = matWhite;
    }



    private void CheckIfIShouldBlink()
    {
        if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 1)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 3f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 2)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 2.7f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 3)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 2.4f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 4)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 2.1f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 5)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 1.8f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 6)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 1.5f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 7)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 1.2f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 8)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 0.9f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 9)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 0.6f);
            }
        }
        else if (enemySpawner.nextSpawnPoint == enemySpawner.Portal2 && enemySpawner.Level == 10)
        {
            if (readyToBlink == true)
            {
                readyToBlink = false;
                Invoke("blinkingPortalEffect", 0.3f);
            }
        }
    }
}
