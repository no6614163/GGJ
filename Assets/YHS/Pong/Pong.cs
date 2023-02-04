using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pong : Singleton<Pong>
{
    float value;
    float lefttime;
    public UI_Game UI_Game;
    GameObject obj1;

    private void Start()
    {
        obj1 = GameObject.Find("leftwall");
    }
    void Awake()
    {
        UI_Game = UI_Manager.Instance.ShowSceneUI<UI_Game>();
    }
    private void Update()
    {
        lefttime= obj1.GetComponent<GameState>().time;
        value = 1-(lefttime / 20);
        UI_Game.SetTimeSlider(value);
    }
}
