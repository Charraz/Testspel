using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
