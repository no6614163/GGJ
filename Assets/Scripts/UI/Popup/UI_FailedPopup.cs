using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using DG.Tweening;

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
        PlayAnimation();
    }
    void PlayAnimation()
    {
        // TODO : 애니메이션 실행
        Sequence seq = DOTween.Sequence();
        int currentStage = GameSystem.Instance.CurrentStage;
        for (int i = 0; i < 5; i++)
        {
            if (i + 1 < currentStage)
            {
                //Get<TMP_Text>((int)Texts.Text_Stage + (i + 1)).text = string.Format("Stage {0} : Clear", (i + 1));
                seq.Append(Get<TMP_Text>((int)Texts.Text_Stage + (i + 1)).DOText(string.Format("Stage {0} : Clear", (i + 1)), 1));
            }
            else if(i + 1 == currentStage)
            {
                seq.Append(Get<TMP_Text>((int)Texts.Text_Stage + (i + 1)).DOText(string.Format("Stage {0} : Failed", (i + 1)), 1));
            }
            else
            {
                Get<TMP_Text>((int)Texts.Text_Stage + (i + 1)).text = "";
            }
        }
        seq.Play();
        SoundManager.Instance.PlaySFX("Lose", "UI");
    }

    void OnButtonClickedOut(PointerEventData evt)
    {
        SoundManager.Instance.PlaySFX("Click", "GameCommon");
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Out");
        // TODO : 로비로 이동하는 이벤트 + 점수 정산
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }

}
