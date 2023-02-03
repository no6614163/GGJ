using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    private float time = 0f;


    private void Update()
    {
        time += Time.deltaTime;
        if(time>=3f)
        {
            //Time.timeScale = 0;
            Debug.Log("클리어");
            time = 0f;
        }

    }
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name=="ball")
        {
            //Time.timeScale = 0;
            Debug.Log("실패");
        }
    }


}
