using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public UI_Start UI_Start;

    void Awake()
    {
        // NOTE : юс╫ц
        UI_Start = UI_Manager.Instance.ShowSceneUI<UI_Start>();
        Instantiate(Resources.Load<SoundManager>("Prefabs/SoundManager"));
        SoundManager.Instance.PlayBGM("MainMenu");
    }
}
