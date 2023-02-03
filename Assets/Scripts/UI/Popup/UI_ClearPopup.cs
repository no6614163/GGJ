using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// NOTE : gamesystem에 score를 먼저 집어 넣고, 그 다음 이 팝업 호출해야됨.
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
        // TODO :스코어 등 출력 애니메이션 추가
        Debug.Log("clear 애니메이션 실행");
        int currentStage = GameSystem.Instance.CurrentStage;
        Get<TMP_Text>((int)Texts.Text_Stage).text = string.Format("Stage {0} : {1}", currentStage, GameSystem.Instance.ScoreArray[currentStage-1]);
    }

    void OnButtonClickedNextStage(PointerEventData evt)
    {
        // TODO : 애니메이션들 넣어주면 좋음.
        Debug.Log("Next Stage");
        // TODO : 다음 스테이지 입력 해주는게 맞음. 스테이지 자체는 랜덤으로
        // TODO : GameSystem.Instance.CurrentStage 활용해야됨. 
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }


}
