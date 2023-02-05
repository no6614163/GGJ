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
        // TODO : �ִϸ��̼� ����
        int stage = GameSystem.Instance.CurrentStage;
        // NOTE : ���ھ� ����� ����Ǿ����.
        Get<TMP_Text>((int)Texts.Text_Stage).DOText(string.Format("Stage {0} : {1}", stage, "Clear"), 1);
        SoundManager.Instance.PlaySFX("Win", "UI");
    }

    void OnButtonClickedNextStage(PointerEventData evt)
    {
        // TODO : �ִϸ��̼ǵ� �־��ָ� ����.
        Debug.Log("Next Stage");
        // TODO : ���� �������� �Է� ���ִ°� ����. �������� ��ü�� ��������
        // �� ȣ�� ���� gamesystem���� ���� �������� �ٲ��ֱ�
        GameSystem.Instance.SetNextStage();
        int rand = Random.Range(2, 8);
        GameSystem.Instance.LoadScene(rand);
    }

}
