using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatchScene : Singleton<CardMatchScene>
{
    public UI_Game UI_Game;
    private void Awake()
    {
        UI_Game = UI_Manager.Instance.ShowSceneUI<UI_Game>();
    }
}
