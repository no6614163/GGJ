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
        // TODO : �ִϸ��̼ǵ� �־��ָ� ����.
        Debug.Log("Next Stage");
        // TODO : ���� �������� �Է� ���ִ°� ����. �������� ��ü�� ��������
        GameSystem.Instance.LoadScene((int)SceneType.Lobby);
    }

    public void SetScore(int stage, int score)
    {
        // NOTE : �̰� �ӽ�
        // TODO :�ִϸ��̼� text �־���ߵ�.
        Get<TMP_Text>((int)Texts.Text_Stage + stage).text = string.Format("Stage {0} : {1}", stage, score);
    }

}
