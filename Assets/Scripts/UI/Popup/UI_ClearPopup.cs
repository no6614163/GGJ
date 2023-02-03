using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
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
        Text_Stage1,
        Text_Stage2,
        Text_Stage3,
        Text_Stage4,
        Text_Stage5,

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
        // TODO : �ִϸ��̼� ����
        int stage = GameSystem.Instance.CurrentStage;
        // NOTE : ���ھ� ����� ����Ǿ����.
        Get<TMP_Text>((int)Texts.Text_Stage).text = string.Format("Stage {0} : {1}", stage, GameSystem.Instance.ScoreArray[stage-1]);
        //if (success)
        //    Get<TMP_Text>((int)Texts.Text_Stage + stage).text = string.Format("Stage {0} : {1}", stage, score);
        //else
        //    Get<TMP_Text>((int)Texts.Text_Stage + stage).text = string.Format("Stage {0} : Failed", stage, score);
    }

    void OnButtonClickedNextStage(PointerEventData evt)
    {
        // TODO : �ִϸ��̼ǵ� �־��ָ� ����.
        Debug.Log("Next Stage");
        // TODO : ���� �������� �Է� ���ִ°� ����. �������� ��ü�� ��������
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }

    public void SetScore(int stage, int score)
    {
        // NOTE : �̰� �ӽ�
        // TODO :�ִϸ��̼� text �־���ߵ�.
        
    }

}
