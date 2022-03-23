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
        int animationNumber = Random.Range(1, 9);
        if (animationNumber == 1)
        {
            animation.SetTrigger("Animation1");
        }
        else if (animationNumber == 2)
        {
            animation.SetTrigger("Animation2");
        }
        else if (animationNumber == 3)
        {
            animation.SetTrigger("Animation3");
        }
        else if (animationNumber == 4)
        {
            animation.SetTrigger("Animation4");
        }
        else if (animationNumber == 5)
        {
            animation.SetTrigger("Animation5");
        }
        else if (animationNumber == 6)
        {
            animation.SetTrigger("Animation6");
        }
        else if (animationNumber == 7)
        {
            animation.SetTrigger("Animation7");
        }
        else if (animationNumber == 8)
        {
            animation.SetTrigger("Animation8");
        }
        else if (animationNumber == 9)
        {
            animation.SetTrigger("Animation9");
        }
    }
}
