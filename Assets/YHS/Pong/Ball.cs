using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int level;
    public float speed;
    // Start is called before the first frame update
    private void Awake()
    {
        level = GameSystem.Instance.GameLevel + 1;
        if(level==1)
        {
            speed = 8f;
        }
        if(level==2)
        {
            speed = 11f;
        }
        if(level==3)
        {
            speed = 13f;
        }
        if(level==4)
        {
            speed = 16f;
        }
        if(level==5)
        {
            speed = 19f;
        }
    }
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    float hitFactor(Vector2 ballPos,Vector2 racketPos,float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name=="RacketL")
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            SoundManager.Instance.PlaySFXPitched("bounce", "pong", 0.02f);
        }

        if(col.gameObject.name=="RacketR")
        {
            SoundManager.Instance.PlaySFXPitched("bounce", "pong", 0.02f);
        }

        
    }
}
