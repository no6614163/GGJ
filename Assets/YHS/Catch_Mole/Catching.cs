using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catching : MonoBehaviour
{
    //public int GameOver_Check = 0;
    GameObject obj;
    GameObject obj2;
    public float TimeLimit = 20f;
    public float time = 0f;
    private bool m_End = false;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("mole");
        obj2 = GameObject.Find("mole");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.name == "mole")
                {
                    Debug.Log("두더지 잡음");
                    hit.collider.GetComponent<Mole>().hit = true;
                    //obj2.GetComponent<Mole>().hit = true;

                }
            }
        }
        if (m_End)
            return;

        time += Time.deltaTime;
        if (time >= TimeLimit)
        {
            //Time.timeScale = 0;
            Debug.Log("클리어");
            m_End = true;
            time = 0f;
            UI_Manager.Instance.ShowPopupUI<UI_ClearPopup>();
            Time.timeScale = 0;
        }

        //void GameOver()
        //{
        //    if(GameOver_Check>=3)
        //    {
        //        UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
        //    }
        //}
    }
}
