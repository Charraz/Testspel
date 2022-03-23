using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public Animator animator;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = PlayerController.InstanceOfPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();
    }

    private void HealthCheck()
    {
        if (playerController.playerHealth == 5)
        {
            animator.SetInteger("HP", 5);
        }

        else if (playerController.playerHealth == 4)
        {
            animator.SetInteger("HP", 4);
        }

        else if (playerController.playerHealth == 3)
        {
            animator.SetInteger("HP", 3);
        }

        else if (playerController.playerHealth == 2)
        {
            animator.SetInteger("HP", 2);
        }

        else if (playerController.playerHealth == 1)
        {
            animator.SetInteger("HP", 1);
        }

        else if (playerController.playerHealth <= 0)
        {
            animator.SetInteger("HP", 0);
        }
    }
}
