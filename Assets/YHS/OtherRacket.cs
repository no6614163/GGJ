using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherRacket : MonoBehaviour
{
    public GameObject ball;
    public float speed = 1f;

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, ball.transform.position.y, transform.position.z), speed * Time.deltaTime);
 
    }
}
