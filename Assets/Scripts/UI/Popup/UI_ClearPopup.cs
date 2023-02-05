using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
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
        PlayAnimation();
    }

    void PlayAnimation()
    {
        // TODO : 애니메이션 실행
        int stage = GameSystem.Instance.CurrentStage;
        // NOTE : 스코어 등록이 선행되어야함.
        Get<TMP_Text>((int)Texts.Text_Stage).DOText(string.Format("Stage {0} : {1}", stage, "Clear"), 1);
        SoundManager.Instance.PlaySFX("Win", "UI");
    }

    void OnButtonClickedNextStage(PointerEventData evt)
    {
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Next Stage");
        // TODO : 다음 스테이지 입력 해주는게 맞음. 스테이지 자체는 랜덤으로
        // 씬 호출 전에 gamesystem에서 현재 스테이지 바꿔주기
        GameSystem.Instance.SetNextStage();
        int rand = Random.Range(2, 8);
        GameSystem.Instance.LoadScene(rand);
    }

}
