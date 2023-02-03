using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_SuccessPopup : UI_Popup
{
    enum Texts
    {
        Text_Success,
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
        PlayAnimation();
    }
    void PlayAnimation()
    {
        for (int i = 0; i < Constants.MaxStageCount; i++)
        {
            // TODO : 애니메이션 실행
            Get<TMP_Text>((int)Texts.Text_Stage + i+1).text = string.Format("Stage {0} : {1}", i+1, GameSystem.Instance.ScoreArray[i]);
        }
    }

    void OnButtonClickedOut(PointerEventData evt)
    {
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Out");
        // TODO : 로비로 이동하는 이벤트 + 점수 정산
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }

}
