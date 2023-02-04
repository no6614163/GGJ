using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{

    private Rigidbody2D m_Rigidbody;
    private AnimalAnimation m_Animator;

    private float m_AnimationInterval = 5;
    private float m_Time = 0;
    private float m_Speed = 1;
    private float m_Distance = 8;
    private Vector2 m_Dir = Vector2.zero;
    private bool m_Left = true;

    void Awake()
    {
        m_Animator = GetComponent<AnimalAnimation>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void ChangeAnimation()
    {
        int rand = Random.Range(0, 4);
        m_Animator.SetAnim((Acting)rand);
        if(m_Animator.CurrentAct == Acting.Walk)
        {
            m_Dir = Random.insideUnitCircle;
            m_Dir.Normalize();
            Debug.Log(m_Dir.x);
            Debug.Log(m_Left);
        }
    }
    void Flip()
    {
        if ((m_Dir.x > 0 && m_Left) || (m_Dir.x < 0 && !m_Left))
        {
            m_Left = !m_Left;
            float x = transform.GetChild(0).localScale.x;
            transform.GetChild(0).localScale = new Vector3(-x, 1, 1);
        }
    }


    void Update()
    {
        m_Time += Time.deltaTime;
        
        if(m_Animator.CurrentAct == Acting.Walk)
        {
            if (Vector2.Distance(Vector2.zero, transform.position) > m_Distance)
            {
                m_Dir = -m_Dir;
            }
            Flip();
            m_Rigidbody.velocity = m_Dir * m_Speed;
        }
        else
        {
            m_Rigidbody.velocity = Vector2.zero;
        }

        if(m_Time >= m_AnimationInterval)
        {
            //
            ChangeAnimation();
            m_Time = 0;
        }

    }
}
