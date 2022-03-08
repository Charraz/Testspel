using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    //private Vector3 mousePosition;
    //private float moveSpeed = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

    }

    private void FixedUpdate()
    {
        Vector3 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseCursorPos.z = 0f;
        transform.position = mouseCursorPos;
    }
}
