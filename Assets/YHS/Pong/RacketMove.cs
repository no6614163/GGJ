using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMove : MonoBehaviour
{
    //public float speed;
    Vector3 mousePos;
    float movey;

    private void Start()
    {
        mousePos = Input.mousePosition;
    }



    private void FixedUpdate()
    {
        //float v = Input.GetAxisRaw("Vertical");
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;

        movey = Input.mousePosition.y; 
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -(mousePos.y - movey));
        mousePos = Input.mousePosition;
    }
}
