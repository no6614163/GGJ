using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherRacket : MonoBehaviour
{
    Vector3 pos; //������ġ

    float delta = 3f; // ��(��)�� �̵������� (x)�ִ밪

    float speed = 500f; // �̵��ӵ�




    void Start()
    {

        pos = transform.position;

    }


    void Update()
    {

        Vector3 v = pos;

        v.y += delta * Mathf.Sin(Time.time * speed);

        // �¿� �̵��� �ִ�ġ �� ���� ó���� �̷��� ���ٿ� ���ְ� �ϳ׿�.

        transform.position = v;

    }
}
