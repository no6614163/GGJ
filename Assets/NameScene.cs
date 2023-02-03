using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameScene : Singleton<NameScene>
{
    public UI_Game UI_Game;

    void Awake()
    {
        UI_Game = UI_Manager.Instance.ShowSceneUI<UI_Game>();

        

    }

}
