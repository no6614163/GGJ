using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_FailedPopup : UI_Popup
{
    enum Texts
    {
        Text_Failed,
        Text_Stage,
        Text_Stage1,
        Text_Stage2,
        Text_Stage3,
        Text_Stage4,
        Text_Stage5,

    }

    enum Images
    {
        Image_Out,
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));


        Get<Image>((int)Images.Image_Out).gameObject.BindEvent(OnButtonClickedOut);
    }

    void OnButtonClickedOut(PointerEventData evt)
    {
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Out");
        // TODO : 로비로 이동하는 이벤트 + 점수 정산
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }

    public void SetScore(int stage, int score, bool success)
    {
        // NOTE : 이거 임시
        // TODO :애니메이션 text 넣어줘야됨.
        if(success)
            Get<TMP_Text>((int)Texts.Text_Stage + stage).text = string.Format("Stage {0} : {1}", stage, score);
        else
            Get<TMP_Text>((int)Texts.Text_Stage + stage).text = string.Format("Stage {0} : Failed", stage, score);
    }

}
