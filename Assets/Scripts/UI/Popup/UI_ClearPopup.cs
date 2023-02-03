using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// NOTE : gamesystem�� score�� ���� ���� �ְ�, �� ���� �� �˾� ȣ���ؾߵ�.
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
        // TODO :���ھ� �� ��� �ִϸ��̼� �߰�
        Debug.Log("clear �ִϸ��̼� ����");
        int currentStage = GameSystem.Instance.CurrentStage;
        Get<TMP_Text>((int)Texts.Text_Stage).text = string.Format("Stage {0} : {1}", currentStage, GameSystem.Instance.ScoreArray[currentStage-1]);
    }

    void OnButtonClickedNextStage(PointerEventData evt)
    {
        // TODO : �ִϸ��̼ǵ� �־��ָ� ����.
        Debug.Log("Next Stage");
        // TODO : ���� �������� �Է� ���ִ°� ����. �������� ��ü�� ��������
        // TODO : GameSystem.Instance.CurrentStage Ȱ���ؾߵ�. 
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }


}
