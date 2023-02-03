using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ClearPopup : UI_Popup
{
    enum Texts
    {
        Text_Success,
        Text_Stage,
    }

    enum Images
    {
        Image_NextStage,
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));


        Get<Image>((int)Images.Image_NextStage).gameObject.BindEvent(OnButtonClickedNextStage);
    }

    void OnButtonClickedNextStage(PointerEventData evt)
    {
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Next Stage");
        // TODO : 다음 스테이지 입력 해주는게 맞음. 스테이지 자체는 랜덤으로
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }

    public void SetScore(int stage, int score)
    {
        // NOTE : 이거 임시
        // TODO :애니메이션 text 넣어줘야됨.
        Get<TMP_Text>((int)Texts.Text_Stage + stage).text = string.Format("Stage {0} : {1}", stage, score);
    }

}
