using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraFadeEffect : MonoBehaviour
{
    private SpriteRenderer auraSpriteRenderer;
    private float transparency;

    void Start()
    {
        auraSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        transparency = 1f;
    }

    void Update()
    {
        if (transparency <= 0)
        {
            AuraFadeIn();
        }
        else if (transparency >= 1)
        {
            AuraFadeOut();
        }
    }

    private void AuraFadeOut()
    {
        auraSpriteRenderer.color = new Color(1, 1, 1, transparency - 0.1f);
    }

    private void AuraFadeIn()
    {
        auraSpriteRenderer.color = new Color(1, 1, 1, transparency + 0.1f);
    }
}
