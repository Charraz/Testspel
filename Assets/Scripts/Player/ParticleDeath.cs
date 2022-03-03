using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeath : MonoBehaviour
{

    public float DeathTimer;

    // Start is called before the first frame update
    void Start()
    {
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
}
