using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour
{

    public UI_Lobby UI_Lobby;


    void Awake()
    {
        // NOTE : �ӽ�
        UI_Manager.Instance.ShowSceneUI<UI_Lobby>();
    }



}
