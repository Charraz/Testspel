using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickedUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Death", 2f);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
