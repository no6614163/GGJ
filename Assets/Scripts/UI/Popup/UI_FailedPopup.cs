using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
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
        Text_StageClear1,
        Text_StageClear2,
        Text_StageClear3,
        Text_StageClear4,
        Text_StageClear5,

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
        // TODO : �ִϸ��̼� ����
        int currentStage = GameSystem.Instance.CurrentStage;
        for (int i = 0; i < 5; i++)
        {
            if(i+1 > currentStage)
                Get<TMP_Text>((int)Texts.Text_Stage + (i+1)).text = string.Format("Stage {0} : Clear", (i+1));
            else
                Get<TMP_Text>((int)Texts.Text_Stage + (i + 1)).text = string.Format("Stage {0} : Failed", (i + 1));
        }
    }

    void OnButtonClickedOut(PointerEventData evt)
    {
        // TODO : �ִϸ��̼ǵ� �־��ָ� ����.
        Debug.Log("Out");
        // TODO : �κ�� �̵��ϴ� �̺�Ʈ + ���� ����
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }

}
