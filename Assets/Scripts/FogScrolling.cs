using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScrolling : MonoBehaviour
{
    float scrollSpeed = -0.7f;
    float newPosition;
    private float length;
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        length = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = Mathf.Repeat(Time.time * scrollSpeed, length);
        transform.position = startPosition + Vector2.right * newPosition;
    }
}
