using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherRacket : MonoBehaviour
{
    Vector3 pos; //현재위치

    float delta = 3f; // 좌(우)로 이동가능한 (x)최대값

    float speed = 500f; // 이동속도




    void Start()
    {

        pos = transform.position;

    }


    void Update()
    {

        Vector3 v = pos;

        v.y += delta * Mathf.Sin(Time.time * speed);

        // 좌우 이동의 최대치 및 반전 처리를 이렇게 한줄에 멋있게 하네요.

        transform.position = v;

    }
}
