using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch_Mole : Singleton<Catch_Mole>
{
    public UI_Game UI_Game;
    public int total_count = 0;
    float value;
    float lefttime;
    int pluscount;
    GameObject obj1;
    GameObject obj2;
    bool GameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        obj1 = GameObject.Find("Catch");
        obj2 = GameObject.Find("Catch");
        Time.timeScale = 1;
    }
    private void Awake()
    {
        UI_Game = UI_Manager.Instance.ShowSceneUI<UI_Game>();
    }

    // Update is called once per frame
    void Update()
    {
        lefttime = obj1.GetComponent<Catching>().time;

        value = 1 - (lefttime / 20);
        UI_Game.SetTimeSlider(value);
    }
    public void MoleNotHitted()
    {
        total_count++;
        if (obj2.GetComponent<Catching>().m_End==false&&total_count == 3)
        {
            obj2.GetComponent<Catching>().m_End = true;
            UI_Manager.Instance.ShowPopupUI<UI_FailedPopup>();
            SoundManager.Instance.PlaySFXPitched("end", "MoleSound", 0.02f);
            GameOver = true;
        }
    }

}
