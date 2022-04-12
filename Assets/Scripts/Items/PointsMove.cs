using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMove : MonoBehaviour
{
    private Rigidbody2D rigidkropp;
    private bool forceHasBeenAdded;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidkropp = GetComponent<Rigidbody2D>();
        forceHasBeenAdded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (forceHasBeenAdded == false)
        {
            rigidkropp.AddForce(new Vector2(0f, 3f), ForceMode2D.Impulse);
            forceHasBeenAdded = true;
        }
    }
}
