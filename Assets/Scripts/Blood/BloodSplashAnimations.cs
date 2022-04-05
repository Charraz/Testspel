using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplashAnimations : MonoBehaviour
{

    public float DeathTimer;
    private new Animator animation;

    void Start()
    {
        animation = gameObject.GetComponent<Animator>();
        ChooseAnimation();
        Invoke("Death", DeathTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void ChooseAnimation()
    {
        int animationNumber = Random.Range(1, 3);

        if (animationNumber == 1)
        {
            animation.SetTrigger("Animation2");
        }

        else if (animationNumber == 2)
        {
            animation.SetTrigger("Animation7");
        }

        else if (animationNumber == 3)
        {
            animation.SetTrigger("Animation9");
        }
    }
}
